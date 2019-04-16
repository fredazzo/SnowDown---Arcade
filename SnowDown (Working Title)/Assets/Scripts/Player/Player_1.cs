﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : PlayerBase
{

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shot.GetComponent<Shot>().movement.x = transform.rotation.x;
        shot.GetComponent<Shot>().movement.y = transform.rotation.y;

        clipSize = reloadAmount;

        for (int i = 0; i < shotPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(shot);
            shotPool[i] = obj;
            shotPool[i].SetActive(false);
        }
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_P1");
        float moveVertical = Input.GetAxis("Vertical_P1");


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Rotate_P1") * rotateSpeed);

        rb.freezeRotation = true;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && clipSize > 0 /*&& Time.time > nextFire*/)
        {
            // nextFire = Time.time + fireRate;
            clipSize--;

            for (int i = 0; i < shotPool.Length; i++)
            {
                if (shotPool[i].activeInHierarchy == false)
                {
                    shotPool[i].transform.position = shotSpawn.transform.position;
                    shotPool[i].transform.rotation = shotSpawn.transform.rotation;
                    shotPool[i].SetActive(true);

                    break;
                }
            }
        }

        if(Input.GetButtonDown("Reload_P1"))
        {
            clipSize++;
        }
        if(clipSize > 5)
        {
            clipSize = 5;
        }
        if (healthPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}


