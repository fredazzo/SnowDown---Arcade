using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : PlayerBase
{


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hit = false;
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

        //transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Rotate_P1") * rotateSpeed);
       // transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Mouse Y") * rotateSpeed);

        rb.freezeRotation = true;

    }

    private void Update()
    {

        //faceMouse();

        rotaionInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        if (Input.GetButtonDown("Fire1") && clipSize > 0 )
        {
            clipSize--;

            for (int i = 0; i < shotPool.Length; i++)
            {
                if (shotPool[i].activeInHierarchy == false)
                {
                    shotPool[i].transform.position = shotSpawn.transform.position;
                    shotPool[i].transform.rotation = shotSpawn.transform.rotation;
                    shotPool[i].GetComponent<Shot>().movement.x = Mathf.Cos(rotaionInRadians);
                    shotPool[i].GetComponent<Shot>().movement.y = Mathf.Sin(rotaionInRadians);
                    SoundManager.instance.PlaySingle(SoundManager.instance.p1ShootingSource);
                    shotPool[i].SetActive(true);

                    break;
                }
            }
        }

        OnMovement(SoundManager.instance.p1MoveSource, "Horizontal_P1", "Vertical_P1");

        if(Input.GetButtonUp("Reload_P1"))
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

        if (gameObject.activeSelf)
        {
            if (hit)
            {
                sprite.color = Color.red;
                StartCoroutine(whitecolor());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            healthPoints--;
            SoundManager.instance.PlaySingle(SoundManager.instance.p1HitSource);
            hit = true;
            CameraShake.instance.MinorShake(.05f);
        }
    }


}


