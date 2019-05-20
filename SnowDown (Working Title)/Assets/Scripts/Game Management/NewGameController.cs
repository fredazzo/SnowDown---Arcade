using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour
{
    public GameObject playerOne;
    private PlayerGlobal _player1;
    public GameObject playerTwo;
    private PlayerGlobal _player2;
    public GameObject cover;
    public GameObject powerUp;
    public GameObject[] coverPool;
    public GameObject[] powerUpPool;

    public Image playerOneHealth;
    public Image playerTwoHealth;

    public Text playerOneAmmo;
    public Text playerTwoAmmo;
    public Text playerOneReload;
    public Text playerTwoReload;
    public Text playerOneUnlimAmmo;
    public Text playerTwoUnlimAmmo;
    public Text leftCountdown;
    public Text rightCountdown;
    public Text roundTextLeft;
    public Text roundTextRight;

    public Transform[] coverSpawnLocationsLeft;
    public Transform[] coverSpawnLocationsRight;
    public Transform[] powerUpSpawnsLeft;
    public Transform[] powerUpSpawnsRight;

    public Animator transitionAnim;

    public float coverSpawnWait;
    private float coverSpawnTimer;
    public float coverDuration;
    public float powerUpIdleDuration;
    public float powerUpCooldown;
    private float healthThreshold;

    bool powerUpActivatedRight;
    bool powerUpActivatedLeft;
    public bool gameActive;
    bool p1Won;
    bool p2Won;

    int coverSpawnLocation;
    int p1WinCount;
    int p2WinCount;

    Vector3 playerOneStartPos;
    Vector3 playerTwoStartPos;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        coverSpawnLocation = 0;

        Debug.Log("displays connected: " + Display.displays.Length);

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();

        for (int i = 0; i < coverPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(cover);
            coverPool[i] = obj;
            coverPool[i].SetActive(false);
        }

        for (int i = 0; i < powerUpPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(powerUp);
            powerUpPool[i] = obj;
            powerUpPool[i].SetActive(false);
        }
        _player1 = playerOne.GetComponent<PlayerGlobal>();
        _player2 = playerTwo.GetComponent<PlayerGlobal>();

        playerOneStartPos = playerOne.transform.position;
        playerTwoStartPos = playerTwo.transform.position;

        healthThreshold = _player1.maxHealthPoints / 2;

        playerOneAmmo.text = _player1.currentClipSize.ToString();
        playerTwoAmmo.text = _player2.currentClipSize.ToString();
        playerOneReload.text = " ";
        playerTwoReload.text = " ";
        leftCountdown.text = " ";
        rightCountdown.text = " ";
        roundTextLeft.text = " ";
        roundTextRight.text = " ";
        StartCoroutine(StartUp("Round 1"));

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Delete))
            Application.Quit();

        if (_player1.currentHealthPoints <= 0)
        {
            p2Won = true;
            p2WinCount++;
            StartCoroutine(RoundReset());
        }

        if (_player2.currentHealthPoints <= 0)
        {
            p1Won = true;
            p1WinCount++;
            StartCoroutine(RoundReset());
        }

        playerOneHealth.fillAmount = _player1.GetPercentageHP();
        playerTwoHealth.fillAmount = _player2.GetPercentageHP();

        SetAmmoText(playerOneAmmo, playerOneReload, playerOneUnlimAmmo, _player1);
        SetAmmoText(playerTwoAmmo, playerTwoReload, playerTwoUnlimAmmo, _player2);

        if (_player1.currentHealthPoints < healthThreshold && !powerUpActivatedRight)
        {
            SpawnPowerUp(powerUpSpawnsRight);
            powerUpActivatedRight = true;
        }
        if (_player2.currentHealthPoints < healthThreshold && !powerUpActivatedLeft)
        {
            SpawnPowerUp(powerUpSpawnsLeft);
            powerUpActivatedLeft = true;
        }

        if (gameActive)
        {
            coverSpawnTimer += Time.deltaTime;

            if (coverSpawnTimer > coverSpawnWait)
            {

                SpawnCover(coverSpawnLocation);
                coverSpawnLocation++;

            }
        }

        if (coverSpawnLocation > 2)
            coverSpawnLocation = 0;

        for (int i = 0; i < coverPool.Length; i++)
        {
            if (coverPool[i].activeInHierarchy == true)
            {
                coverPool[i].GetComponent<Cover>().timer += Time.deltaTime;
                if (coverPool[i].GetComponent<Cover>().timer > coverDuration)
                {
                    StartCoroutine(DespawnCover(i));
                }
            }
        }

        for (int i = 0; i < powerUpPool.Length; i++)
        {
            if (powerUpPool[i].activeInHierarchy == true)
            {
                powerUpPool[i].GetComponent<UnlimAmmo>().timer += Time.deltaTime;
                if (powerUpPool[i].GetComponent<UnlimAmmo>().timer > powerUpIdleDuration)
                {
                    powerUpPool[i].SetActive(false);
                    powerUpPool[i].GetComponent<UnlimAmmo>().timer = 0.0f;

                }
            }
        }



    }

    void SpawnCover(int coverSpawnLocation)
    {
        coverSpawnTimer = 0f;
        for (int i = 0; i < coverPool.Length; i++)
        {

            if (coverPool[i].activeInHierarchy == false)
            {
                // int spawnSelection = Random.Range(0, coverSpawnLocations.Length - 1);

                coverPool[i].transform.position = coverSpawnLocationsLeft[coverSpawnLocation].position;
                coverPool[i].transform.rotation = coverSpawnLocationsLeft[coverSpawnLocation].rotation;
                SoundManager.instance.PlaySingle(SoundManager.instance.coverSpawnSource);
                coverPool[i].SetActive(true);
                i++;
                coverPool[i].transform.position = coverSpawnLocationsRight[coverSpawnLocation].position;
                coverPool[i].transform.rotation = coverSpawnLocationsRight[coverSpawnLocation].rotation;
                SoundManager.instance.PlaySingle(SoundManager.instance.coverSpawnSource);
                coverPool[i].SetActive(true);

                break;
            }

        }

    }

    IEnumerator DespawnCover(int index)
    {
        coverPool[index].GetComponent<Cover>().timer = 0.0f;
        coverPool[index].GetComponent<Cover>().anim.SetTrigger("Despawn");
        coverPool[index].GetComponent<Cover>().anim.SetBool("Exit", true);
        yield return new WaitForSeconds(1.5f);
        coverPool[index].SetActive(false);
    }

    void SpawnPowerUp(Transform[] locations)
    {
        int spawnChance = Random.Range(0, 4);
        int randomLocation = Random.Range(0, locations.Length - 1);

        for (int i = 0; i < powerUpPool.Length; i++)
        {
            if (powerUpPool[i].activeInHierarchy == false)
            {
                powerUpPool[i].transform.position = locations[randomLocation].position;
                powerUpPool[i].transform.rotation = locations[randomLocation].rotation;
                powerUpPool[i].SetActive(true);

                break;
            }
            
        }

    }

    void SetAmmoText(Text ammoText, Text reloadText, Text unlimAmmoText, PlayerGlobal player)
    {
        ammoText.text = player.currentClipSize.ToString();
        if (player.currentClipSize <= 0)
        {
            ammoText.text = " ";
            reloadText.text = "Reload!";
        }
        else
        {
            reloadText.text = " ";
        }
        if(player.unlimAmmo)
        {
            ammoText.text = " ";
            reloadText.text = " ";
            unlimAmmoText.text = "\x221E";
        }
        else
        {
            unlimAmmoText.text = " ";
        }
    }


    IEnumerator StartUp(string roundNumber)
    {
        roundTextLeft.text = roundNumber;
        roundTextRight.text = roundNumber;
        leftCountdown.text = "3...";
        rightCountdown.text = "3...";
        yield return new WaitForSeconds(1.0f);
        leftCountdown.text = "2...";
        rightCountdown.text = "2...";
        yield return new WaitForSeconds(1.0f);
        leftCountdown.text = "1...";
        rightCountdown.text = "1...";
        yield return new WaitForSeconds(1.0f);
        leftCountdown.text = "Start!";
        rightCountdown.text = "Start!";
        gameActive = true;
        _player1.canShoot = true;
        _player2.canShoot = true;
        yield return new WaitForSeconds(1.0f);
        roundTextLeft.text = " ";
        roundTextRight.text = " ";
        leftCountdown.text = " ";
        rightCountdown.text = " ";
           
    }
    //roundTextRight.text = "Round Won!";
    //roundTextLeft.text = "Round Lost!";

    //roundTextRight.text = "Round Lost!";
    //roundTextLeft.text = "Round Won!";
    IEnumerator RoundReset()
    {
        yield return new WaitForSeconds(5.0f);

        gameActive = false;
        playerOne.transform.position = playerOneStartPos;
        _player1.currentHealthPoints = _player1.maxHealthPoints;
        _player1.currentClipSize = _player1.maxClipSize;
        playerTwo.transform.position = playerTwoStartPos;
        _player2.currentHealthPoints = _player2.maxHealthPoints;
        _player2.currentClipSize = _player2.maxClipSize;
        playerOne.gameObject.SetActive(true);
        playerTwo.gameObject.SetActive(true);
        p1Won = false;
        p2Won = false;
    }


    //void PowerUpActivity(GameObject player, bool powerUpActivity, Transform[] spawns)
    //{
    //    if (player.GetComponent<PlayerGlobal>().currentHealthPoints < healthThreshold && !powerUpActivity)
    //    {
    //        SpawnPowerUp(spawns);
    //        powerUpActivity = true;
    //    }
    //}

}

// infinity symbol: \x221E