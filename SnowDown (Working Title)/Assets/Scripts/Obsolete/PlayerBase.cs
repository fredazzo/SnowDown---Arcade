﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rb;

    public int currentClipSize;
    public int reloadAmount;
    public int maxClipSize;
    public int currentHealthPoints;
    public int maxHealthPoints;

    public float fireRate;
    protected float fireTimer;
    public float speed;
    public float rotateSpeed;
    protected float rotaionInRadians;
    public float unlimAmmoDuration;
    protected float powerUpTimer = 0f;

    public GameObject shot;
    public GameObject[] shotPool;

    public Transform shotSpawn;

    protected bool moving;
    protected bool hit;
    public bool unlimAmmo;

    public SpriteRenderer sprite;


    public void OnMovement(AudioSource source, string horizontalAxis, string verticalAxis)
    {
        if (Input.GetButton(horizontalAxis))
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        if (Input.GetButton(verticalAxis))
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }

        if (!Input.GetButton(horizontalAxis) && !Input.GetButton(verticalAxis))
        {
            source.Pause();
        }
    }

    public IEnumerator whitecolor()
    {
        yield return new WaitForSeconds(0.02f);
        sprite.color = Color.white;
        hit = false;
    }


    public void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;
    }

    public void UnlimAmmo()
    {
        powerUpTimer += Time.deltaTime;

        currentClipSize = maxClipSize;

        if (powerUpTimer > unlimAmmoDuration)
        {
            powerUpTimer = 0f;
            unlimAmmo = false;
        }
    }
}
