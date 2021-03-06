﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class STATS
{
    public static int LIFE = 10;

}

public class PlayerGlobal : MonoBehaviour
{
    public bool playerOne;

    public string horizontalAxis;
    public string verticalAxis;
    public string altFireButton;
    public string altReloadButton;
    public KeyCode fireButton;
    public KeyCode reloadButton;
    //public string rotateAxis;

    Rigidbody2D rb;

    public int currentClipSize;
    public int reloadAmount;
    public int maxClipSize;
    private int currentHealthPoints;
    public int maxHealthPoints;

    public float fireRate;
    private float fireTimer = 0f;
    public float maxFireIdleTime;
    private float fireIdleTImer = 0f;
    public float maxSnowballMeltTime;
    private float snowballMeltTimer;
    public float speed;
    private float originalSpeed;
    public float rotateSpeed;
    private float rotaionInRadians;
    public float unlimAmmoDuration;
    private float powerUpTimer = 0f;
    private float rotationZ;

    public GameObject shot;
    public GameObject[] shotPool;

    public Transform shotSpawn;

    private bool moving;
    private bool hit;
    public bool unlimAmmo;
    public bool canShoot;
    public bool canDie;
    public bool melting;

    public SpriteRenderer body;
    public SpriteRenderer arms;
    public SpriteRenderer cannon;
    public SpriteRenderer aura;

    AudioSource moveSource;
    AudioSource shootSource;
    AudioSource hitSource;
    AudioSource deathSource;
    AudioSource reloadSource;

    Vector3 originalRotation;

    public Animator deathAnim;
    public Animator walkingAnim;
    public Animator cannonMoveAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hit = false;
        canShoot = false;
        canDie = true;
        currentClipSize = maxClipSize;
        currentHealthPoints = maxHealthPoints;
        originalSpeed = speed;
        snowballMeltTimer = maxSnowballMeltTime;
        for (int i = 0; i < shotPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(shot);
            shotPool[i] = obj;
            shotPool[i].SetActive(false);
        }

        if (playerOne)
        {
            moveSource = SoundManager.instance.p1MoveSource;
            shootSource = SoundManager.instance.p1ShootingSource;
            hitSource = SoundManager.instance.p1HitSource;
            deathSource = SoundManager.instance.p1DeathSource;
            reloadSource = SoundManager.instance.p1ReloadSource;
        }
        else
        {
            moveSource = SoundManager.instance.p2MoveSource;
            shootSource = SoundManager.instance.p2ShootingSource;
            hitSource = SoundManager.instance.p2HitSource;
            deathSource = SoundManager.instance.p2Deathsource;
            reloadSource = SoundManager.instance.p2ReloadSource;
        }

        originalRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        rotationZ = transform.eulerAngles.z;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis(horizontalAxis);
        float moveVertical = Input.GetAxis(verticalAxis);


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;


        rb.freezeRotation = true;

        //rotationZ += rotate * rotateSpeed;
        //rotationZ = Mathf.Clamp(rotationZ, originalRotation.z - 90, originalRotation.z + 90);

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -rotationZ);
        //transform.Rotate(0.0f, 0.0f, -rotate * rotateSpeed);


    }

    private void Update()
    {
        rotaionInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        if (currentHealthPoints > 0)
        {
            OnMovement(moveSource, horizontalAxis, verticalAxis);
        }

        if (unlimAmmo)
        {
            aura.enabled = true;
            UnlimAmmo();
            if (currentHealthPoints <= 0)
                aura.enabled = false;
        }
        else
            aura.enabled = false;

        fireTimer += Time.deltaTime;

        if (canShoot)
        {
            if (Input.GetKeyDown(fireButton) || Input.GetButtonDown(altFireButton))
            {
                resetFIreTimer();
                resetMeltTimer();
            }
            else
                tickFireTimer();
        }
        else
        {
            resetFIreTimer();
            resetMeltTimer();
        }


        if (fireIdleTImer > maxFireIdleTime)
        {
            melting = true;
            meltSnowball();
            if (currentClipSize <= 0)
            {
                melting = false;
            }
        }
        else
            melting = false;
        if (Input.GetKeyDown(fireButton) || Input.GetButtonDown(altFireButton))
        {
            if (currentClipSize > 0 && fireTimer > fireRate && canShoot)
            {
                fireTimer = 0f;
                currentClipSize--;


                for (int i = 0; i < shotPool.Length; i++)
                {
                    if (shotPool[i].activeInHierarchy == false)
                    {
                        shotPool[i].transform.position = shotSpawn.transform.position;
                        shotPool[i].transform.rotation = shotSpawn.transform.rotation;
                        shotPool[i].GetComponent<Snowball>().movement.x = Mathf.Cos(rotaionInRadians);
                        shotPool[i].GetComponent<Snowball>().movement.y = Mathf.Sin(rotaionInRadians);
                        //shotPool[i].GetComponent<Snowball>().ResetProperties();
                        SoundManager.instance.PlaySingle(shootSource);
                        if (playerOne)
                            shotPool[i].GetComponent<Snowball>().SetProjectileType(Snowball.Type.PLAYER_ONE);
                        else
                            shotPool[i].GetComponent<Snowball>().SetProjectileType(Snowball.Type.PLAYER_TWO);
                        shotPool[i].SetActive(true);

                        break;
                    }
                }
            }
        }

        if (Input.GetKeyUp(reloadButton) || Input.GetButtonDown(altReloadButton))
        {
            currentClipSize += reloadAmount;
            resetFIreTimer();
            resetMeltTimer();
            reloadSource.Play();
        }

        if (currentClipSize > maxClipSize)
        {
            currentClipSize = maxClipSize;
        }


        if (gameObject.activeSelf)
        {
            if (hit)
            {
                body.color = Color.red;
                arms.color = Color.red;
                StartCoroutine(whitecolor());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            // GameController: NotificationManager.Subscribe("PlayerDeath", PlayerDeath(Notification) );
            //
            // Player: NotificationManager.Post("PlayerDeath", PlayerID);

            hit = true;
            walkingAnim.SetBool("hit", true);

            if (playerOne)
            {
                if (other.gameObject.GetComponent<Snowball>().GetProjectileType() == Snowball.Type.PLAYER_TWO && canDie)
                {
                    ModifyHealth(-1, deathSource);
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Snowball>().GetProjectileType() == Snowball.Type.PLAYER_ONE && canDie)
                {
                    ModifyHealth(-1, deathSource);
                }
            }
            SoundManager.instance.PlaySingle(hitSource);
            CameraShake.instance.MinorShake(.05f);
            CameraP1.instance.MinorShake(.05f);
            CameraP2.instance.MinorShake(.05f);

        }
    }

    public void Reset(Vector3 startingPosition)
    {
        transform.position = startingPosition;
        currentHealthPoints = maxHealthPoints;
        currentClipSize = maxClipSize;
        unlimAmmo = false;
        arms.enabled = true;
        cannon.enabled = true;
        body.enabled = true;
        canDie = true;
        speed = originalSpeed;
        deathAnim.SetBool("dead", false);
        gameObject.SetActive(true);
    }

    public int GetHealth()
    {
        return currentHealthPoints;
    }

    public void ModifyHealth(int value, AudioSource deathSource)
    {
        currentHealthPoints += value;
        if (currentHealthPoints > maxHealthPoints)
            currentHealthPoints = maxHealthPoints;
        if (currentHealthPoints <= 0)
        {
            fireIdleTImer = 0f;

            DisableIdleSprites();
            walkingAnim.SetBool("moving", false);
            cannonMoveAnim.SetBool("moving", false);
            canShoot = false;
            SoundManager.instance.PlaySingle(deathSource);
            deathAnim.SetBool("dead", true);
            speed = 0;
        }

    }

    public void DisableIdleSprites()
    {
        body.enabled = false;
        arms.enabled = false;
        cannon.enabled = false;
    }

    public void EnableIdleSprites()
    {
        body.enabled = true;
        arms.enabled = true;
        cannon.enabled = true;

    }

    public void OnMovement(AudioSource source, string horizontalAxis, string verticalAxis)
    {
        if (Input.GetButton(horizontalAxis))
        {
            walkingAnim.SetBool("moving", true);
            cannonMoveAnim.SetBool("moving", true);
            DisableIdleSprites();
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        if (Input.GetButton(verticalAxis))
        {
            walkingAnim.SetBool("moving", true);
            cannonMoveAnim.SetBool("moving", true);
            DisableIdleSprites();
            if (!source.isPlaying)
            {
                source.Play();
            }
        }

        if (!Input.GetButton(horizontalAxis) && !Input.GetButton(verticalAxis))
        {
            walkingAnim.SetBool("moving", false);
            cannonMoveAnim.SetBool("moving", false);
            source.Pause();

            if (currentHealthPoints > 0)
                EnableIdleSprites();

        }
    }

    public IEnumerator whitecolor()
    {
        yield return new WaitForSeconds(0.02f);
        body.color = Color.white;
        arms.color = Color.white;
        walkingAnim.SetBool("hit", false);
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

    public float GetPercentageHP()
    {
        return (float)currentHealthPoints / maxHealthPoints;

    }

    public void resetFIreTimer()
    {
        fireIdleTImer = 0f;
    }

    public void tickFireTimer()
    {
        fireIdleTImer += Time.deltaTime;
    }

    public void resetMeltTimer()
    {
        snowballMeltTimer = maxSnowballMeltTime;
        melting = false;
    }

    public void meltSnowball()
    {
        snowballMeltTimer -= Time.deltaTime;
        if(snowballMeltTimer < 0)
        {
            currentClipSize -= 1;
            snowballMeltTimer = maxSnowballMeltTime;
            //fireIdleTImer = 0f;
        }
    }

    public float getMeltTimer()
    {
        return snowballMeltTimer / maxSnowballMeltTime;
    }


}
