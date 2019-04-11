﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject _player1;
    public GameObject _player2;
    public GameObject cover;
    public GameObject[] coverPool;

    public Image[] player1Health;
    public Image[] player2Health;
    public Image[] player1Ammo;
    public Image[] player2Ammo;

    public Transform[] coverSpawnLocations;

    public float coverSpawnTimer;
    private float coverSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < coverPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(cover);
            coverPool[i] = obj;
            coverPool[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {


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

            if (_player1.GetComponent<Player_1>().clipSize <= 4)
            {
                player1Ammo[4].enabled = false;
            }
            else
            {
                player1Ammo[4].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().clipSize <= 3)
            {
                player1Ammo[3].enabled = false;
            }
            else
            {
                player1Ammo[3].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().clipSize <= 2)
            {
                player1Ammo[2].enabled = false;
            }
            else
            {
                player1Ammo[2].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().clipSize <= 1)
            {
                player1Ammo[1].enabled = false;
            }
            else
            {
                player1Ammo[1].enabled = true;
            }
            if (_player1.GetComponent<Player_1>().clipSize <= 0)
            {
                player1Ammo[0].enabled = false;
            }
            else
            {
                player1Ammo[0].enabled = true;
            }
        }


        // Player health and ammo checks
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

        if (_player2.GetComponent<Player_2>().clipSize <= 4)
        {
            player2Ammo[4].enabled = false;
        }
        else
        {
            player2Ammo[4].enabled = true;
        }
        if (_player2.GetComponent<Player_2>().clipSize <= 3)
        {
            player2Ammo[3].enabled = false;
        }
        else
        {
            player2Ammo[3].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().clipSize <= 2)
        {
            player2Ammo[2].enabled = false;
        }
        else
        {
            player2Ammo[2].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().clipSize <= 1)
        {
            player2Ammo[1].enabled = false;
        }
        else
        {
            player2Ammo[1].enabled = true;

        }
        if (_player2.GetComponent<Player_2>().clipSize <= 0)
        {
            player2Ammo[0].enabled = false;
        }
        else
        {
            player2Ammo[0].enabled = true;

        }
    }

        coverSpawnTime += Time.deltaTime;

        if(coverSpawnTime > coverSpawnTimer)
        {
            coverSpawnTime = 0f;
            for (int i = 0; i < coverPool.Length; i++)
            {
                if (coverPool[i].activeInHierarchy == false)
                {
                    int spawnSelection = Random.Range(0, 3);

                    coverPool[i].transform.position = coverSpawnLocations[spawnSelection].position;
                    coverPool[i].SetActive(true);

                    break;
                }
            }
        }
    }
}
