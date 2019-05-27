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
    public Image[] playerOneWins;
    public Image[] playerTwoWins;
    public Image playerOneUnlimAmmo;
    public Image playerTwoUnlimAmmo;

    public Text playerOneAmmo;
    public Text playerTwoAmmo;
    public Text playerOneReload;
    public Text playerTwoReload;
    public Text leftCountdown;
    public Text rightCountdown;
    public Text roundTextLeft;
    public Text roundTextRight;
    public Text reloadIndicatorLeft;
    public Text reloadIndicatorRight;
    public Text leftLowHealth;
    public Text rightLowHealth;

    public Transform[] coverSpawnLocationsLeft;
    public Transform[] coverSpawnLocationsRight;
    public Transform[] powerUpSpawnsLeft;
    public Transform[] powerUpSpawnsRight;

    public Animator transitionAnim;
    public Animator p1HealthAnim;
    public Animator p2HealthAnim;

    public float coverSpawnWait;
    private float coverSpawnTimer;
    public float coverDuration;
    public float powerUpIdleDuration;
    public float powerUpCooldown;
    private float healthThreshold;

    private bool powerUpActivatedRight;
    private bool powerUpActivatedLeft;
    private bool gameActive;
    private bool p1Won;
    private bool p2Won;

    private int coverSpawnLocation;
    private int p1WinCount;
    private int p2WinCount;

    private Vector3 playerOneStartPos;
    private Vector3 playerTwoStartPos;

    public string winScene;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        coverSpawnLocation = 0;


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

        SoundManager.instance.musicSource.Play();

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

        if (_player1.GetHealth() <= 0 && _player2.GetHealth() > 0)
        {
            p2Won = true;
            roundTextRight.text = "Round Lost!";
            roundTextLeft.text = "Round Won!";
            gameActive = false;
            _player2.canDie = false;
        }

        if (_player2.GetHealth() <= 0 && _player1.GetHealth() > 0)
        {
            p1Won = true;
            roundTextRight.text = "Round Won!";
            roundTextLeft.text = "Round Lost!";
            gameActive = false;
            _player1.canDie = false;
        }
        if(_player1.GetHealth() <= 0 && _player2.GetHealth() <= 0)
        {
            roundTextRight.text = "Draw!";
            roundTextLeft.text = "Draw!";
            gameActive = false;
            StartCoroutine(RoundReset());
        }

        if (p1Won || p2Won)
        {
           StartCoroutine(RoundReset());
            p1Won = false;
            p2Won = false;
        }

        if(p1WinCount == 0)
        {
            playerOneWins[0].gameObject.SetActive(false);
            playerOneWins[1].gameObject.SetActive(false);
        }
        else if (p1WinCount == 1)
        {
            playerOneWins[0].gameObject.SetActive(true);
        }
        else if (p1WinCount == 2)
        {
            playerOneWins[0].gameObject.SetActive(true);
            playerOneWins[1].gameObject.SetActive(true);
        }
        if(p2WinCount == 0)
        {
            playerTwoWins[0].gameObject.SetActive(false);
            playerTwoWins[1].gameObject.SetActive(false);
        }
        else if (p2WinCount == 1)
        {
            playerTwoWins[0].gameObject.SetActive(true);
        }
        else if (p2WinCount == 2)
        {
            playerTwoWins[0].gameObject.SetActive(true);
            playerTwoWins[1].gameObject.SetActive(true);
        }




        playerOneHealth.fillAmount = _player1.GetPercentageHP();
        playerTwoHealth.fillAmount = _player2.GetPercentageHP();

        SetAmmoText(playerOneAmmo, playerOneReload, playerOneUnlimAmmo, reloadIndicatorRight, _player1);
        SetAmmoText(playerTwoAmmo, playerTwoReload, playerTwoUnlimAmmo, reloadIndicatorLeft, _player2);

        if (_player1.GetHealth() < healthThreshold)
        {
            p1HealthAnim.SetBool("low health", true);
            rightLowHealth.gameObject.SetActive(true);
            if (!powerUpActivatedRight)
            {
                SpawnPowerUp(powerUpSpawnsRight);
                powerUpActivatedRight = true;
            }
        }
        else
        {
            p1HealthAnim.SetBool("low health", false);
            rightLowHealth.gameObject.SetActive(false);
        }
        if (_player2.GetHealth() < healthThreshold)
        {
            p2HealthAnim.SetBool("low health", true);
            leftLowHealth.gameObject.SetActive(true);
            if (!powerUpActivatedLeft)
            {
                SpawnPowerUp(powerUpSpawnsLeft);
                powerUpActivatedLeft = true;
            }
        }
        else
        {
            p2HealthAnim.SetBool("low health", false);
            leftLowHealth.gameObject.SetActive(false);
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

    void SetAmmoText(Text ammoText, Text reloadText, Image unlimAmmo, Text relaodIndicator, PlayerGlobal player)
    {
        ammoText.text = player.currentClipSize.ToString();
        if (player.currentClipSize <= 0)
        {
            ammoText.text = " ";
            reloadText.text = "Reload!";
            relaodIndicator.gameObject.SetActive(true);
        }
        else
        {
            reloadText.text = " ";
            relaodIndicator.gameObject.SetActive(false);
        }
        if (player.unlimAmmo)
        {
            ammoText.text = " ";
            reloadText.text = " ";
            unlimAmmo.enabled = true;
        }
        else
        {
            unlimAmmo.enabled = false;
        }
    }


    IEnumerator StartUp(string roundNumber)
    {
        reloadIndicatorLeft.text = "Reload!";
        reloadIndicatorRight.text = "Reload!";
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
        resetActive = false;
    }

    private bool resetActive = false;
    private int roundCounter = 1;

    IEnumerator RoundReset()
    {

        if (resetActive)
            yield break;
        resetActive = true;
        roundCounter++;
        if (p1Won)
        {
            p1WinCount++;
        }
        if (p2Won)
        {
            p2WinCount++;

        }
        reloadIndicatorLeft.text = " ";
        reloadIndicatorRight.text = " ";
        yield return new WaitForSeconds(6.0f);

        if (p1WinCount == 2 || p2WinCount == 2)
        {
            if (p1WinCount == 2)
                PlayerPrefs.SetInt("Winner", 1);
            else
                PlayerPrefs.SetInt("Winner", 2);
            StartCoroutine(OnSceneLoad(winScene));
            yield break;
        }

        _player1.Reset(playerOneStartPos);
        _player2.Reset(playerTwoStartPos);
        _player1.canShoot = false;
        _player2.canShoot = false;
        powerUpActivatedRight = false;
        powerUpActivatedLeft = false;
        roundTextLeft.text = " ";
        roundTextRight.text = " ";
        string round = " ";
        if (roundCounter == 2)
            round = "Round 2";
        else if (roundCounter == 3)
        {
            if (p1WinCount < 1 || p2WinCount < 1)
                round = "Round 3";
            else
                round = "Final Round!";
        }
        else if (roundCounter == 4)
        {
            if (p1WinCount < 1 || p2WinCount < 1)
                round = "Round 4";
            else
                round = "Final Round!";

        }
        else if (roundCounter == 5)
        {
            if (p1WinCount < 1 || p2WinCount < 1)
                round = "Round 5";
            else
                round = "Final Round!";

        }
        else if (roundCounter == 6)
        {
            if (p1WinCount < 1 || p2WinCount < 1)
                round = "Round 6";
            else
                round = "Final Round!";

        }
        else if (roundCounter == 7)
            round = "Final Round!";
        StartCoroutine(StartUp(round));
    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
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