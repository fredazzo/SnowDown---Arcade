 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject _player1;
    public GameObject _player2;
    public GameObject cover;
    public GameObject powerUp;
    public GameObject[] coverPool;
    public GameObject[] powerUpPool;

    public Image[] player1Health;
    public Image[] player2Health;
    public Image[] player1Ammo;
    public Image[] player2Ammo;

    public Transform[] coverSpawnLocationsLeft;
    public Transform[] coverSpawnLocationsRight;
    public Transform[] powerUpSpawnsLeft;
    public Transform[] powerUpSpawnsRight;

    public float coverSpawnWait;
    private float coverSpawnTimer;
    public float coverDuration;
    public float powerUpIdleDuration;
    public float powerUpCooldown;

    bool powerUpActivatedRight;
    bool powerUpActivatedLeft;

    int coverSpawnIndex;
    int healthThreshold;

    // Start is called before the first frame update
    void Start()
    {
        coverSpawnIndex = 0;
        healthThreshold = _player1.GetComponent<PlayerBase>().healthPoints / 2;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Delete))
            Application.Quit();


        // Player1 health and ammo checks
        {
            if (_player1.GetComponent<Player_1>().healthPoints == 9)
            {
                player1Health[9].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 8)
            {
                player1Health[8].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 7)
            {
                player1Health[7].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 6)
            {
                player1Health[6].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 5)
            {
                player1Health[5].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 4)
            {
                player1Health[4].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 3)
            {
                player1Health[3].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 2)
            {
                player1Health[2].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 1)
            {
                player1Health[1].enabled = false;
            }
            if (_player1.GetComponent<Player_1>().healthPoints == 0)
            {
                player1Health[0].enabled = false;
            }

            if (_player1.GetComponent<Player_1>().currentClipSize <= 4)
            {
                player1Ammo[4].enabled = false;
            }
            else
            {
                player1Ammo[4].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().currentClipSize <= 3)
            {
                player1Ammo[3].enabled = false;
            }
            else
            {
                player1Ammo[3].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().currentClipSize <= 2)
            {
                player1Ammo[2].enabled = false;
            }
            else
            {
                player1Ammo[2].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().currentClipSize <= 1)
            {
                player1Ammo[1].enabled = false;
            }
            else
            {
                player1Ammo[1].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().currentClipSize <= 0)
            {
                player1Ammo[0].enabled = false;
            }
            else
            {
                player1Ammo[0].enabled = true;
            }
        }


        // Player2 health and ammo checks
        { 
        if (_player2.GetComponent<Player_2>().healthPoints == 9)
        {
            player2Health[9].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 8)
        {
            player2Health[8].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 7)
        {
            player2Health[7].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 6)
        {
            player2Health[6].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 5)
        {
            player2Health[5].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 4)
        {
            player2Health[4].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 3)
        {
            player2Health[3].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 2)
        {
            player2Health[2].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 1)
        {
            player2Health[1].enabled = false;
        }
        if (_player2.GetComponent<Player_2>().healthPoints == 0)
        {
            player2Health[0].enabled = false;
        }

        if (_player2.GetComponent<Player_2>().currentClipSize <= 4)
        {
            player2Ammo[4].enabled = false;
        }
        else
        {
            player2Ammo[4].enabled = true;
        }
        if (_player2.GetComponent<Player_2>().currentClipSize <= 3)
        {
            player2Ammo[3].enabled = false;
        }
        else
        {
            player2Ammo[3].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().currentClipSize <= 2)
        {
            player2Ammo[2].enabled = false;
        }
        else
        {
            player2Ammo[2].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().currentClipSize <= 1)
        {
            player2Ammo[1].enabled = false;
        }
        else
        {
            player2Ammo[1].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().currentClipSize <= 0)
        {
            player2Ammo[0].enabled = false;
        }
        else
        {
            player2Ammo[0].enabled = true;

        }
    }

        coverSpawnTimer += Time.deltaTime;

  
        if (coverSpawnTimer > coverSpawnWait)
        {

            SpawnCover(coverSpawnIndex);
            coverSpawnIndex++;

        }
        Debug.Log(coverSpawnIndex);

        if (coverSpawnIndex > 2)
            coverSpawnIndex = 0;

        if (_player1.GetComponent<Player_1>().healthPoints < healthThreshold && !powerUpActivatedRight)
        {
            SpawnPowerUp(powerUpSpawnsRight);
            powerUpActivatedRight = true;
        }

        if (_player2.GetComponent<Player_2>().healthPoints < healthThreshold && !powerUpActivatedLeft)
        {
            SpawnPowerUp(powerUpSpawnsLeft);
            powerUpActivatedLeft = true;
        }




        for (int i = 0; i < coverPool.Length; i++)
        {
           if(coverPool[i].activeInHierarchy == true)
            {
                coverPool[i].GetComponent<Cover>().timer += Time.deltaTime;
                if(coverPool[i].GetComponent<Cover>().timer > coverDuration)
                {
                    coverPool[i].SetActive(false);
                    coverPool[i].GetComponent<Cover>().timer = 0.0f;
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

    void SpawnCover(int coverSpawnIndex)
    {
        coverSpawnTimer = 0f;
        for (int i = 0; i < coverPool.Length; i++)
        {
            
            if (coverPool[i].activeInHierarchy == false)
            {
               // int spawnSelection = Random.Range(0, coverSpawnLocations.Length - 1);

                coverPool[i].transform.position = coverSpawnLocationsLeft[coverSpawnIndex].position;
                coverPool[i].transform.rotation = coverSpawnLocationsLeft[coverSpawnIndex].rotation;
                SoundManager.instance.PlaySingle(SoundManager.instance.coverSpawnSource);
                coverPool[i].SetActive(true);
                i++;
                coverPool[i].transform.position = coverSpawnLocationsRight[coverSpawnIndex].position;
                coverPool[i].transform.rotation = coverSpawnLocationsRight[coverSpawnIndex].rotation;
                SoundManager.instance.PlaySingle(SoundManager.instance.coverSpawnSource);
                coverPool[i].SetActive(true);

                break;
            }

        }

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
}
