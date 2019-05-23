using System.Collections;
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
    public float speed;
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

    public SpriteRenderer body;

    AudioSource moveSource;
    AudioSource shootSource;
    AudioSource hitSource;

    Vector3 originalRotation;

    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hit = false;
        canShoot = false;
        currentClipSize = maxClipSize;
        currentHealthPoints = maxHealthPoints;

        for (int i = 0; i < shotPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(shot);
            shotPool[i] = obj;
            shotPool[i].SetActive(false);
        }

        if(playerOne)
        {
            moveSource = SoundManager.instance.p1MoveSource;
            shootSource = SoundManager.instance.p1ShootingSource;
            hitSource = SoundManager.instance.p1HitSource;
        }
        else
        {
            moveSource = SoundManager.instance.p2MoveSource;
            shootSource = SoundManager.instance.p2ShootingSource;
            hitSource = SoundManager.instance.p2HitSource;
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

        OnMovement(moveSource, horizontalAxis, verticalAxis);

        if (unlimAmmo)
        {
            UnlimAmmo();
        }

        fireTimer += Time.deltaTime;

        if (Input.GetKeyDown(fireButton) && currentClipSize > 0 && fireTimer > fireRate && canShoot)
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
                    SoundManager.instance.PlaySingle(shootSource);
                    if (playerOne)
                        shotPool[i].GetComponent<Snowball>().setType(Snowball.Type.PLAYER_ONE);
                    else
                        shotPool[i].GetComponent<Snowball>().setType(Snowball.Type.PLAYER_TWO);
                    shotPool[i].SetActive(true);

                    break;
                }
            }
        }



        if (Input.GetKeyUp(reloadButton))
        {
            currentClipSize += reloadAmount;
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
            if (playerOne)
            {
                if (other.gameObject.GetComponent<Snowball>().getType() == Snowball.Type.PLAYER_TWO)
                {
                    ModifyHealth(-1);
                    hit = true;
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Snowball>().getType() == Snowball.Type.PLAYER_ONE)
                {
                    ModifyHealth(-1);
                    hit = true;
                }
            }
            SoundManager.instance.PlaySingle(hitSource);
            CameraShake.instance.MinorShake(.05f);
        }
    }

    public void Reset(Vector3 startingPosition)
    {
        transform.position = startingPosition;
        currentHealthPoints = maxHealthPoints;
        currentClipSize = maxClipSize;
        unlimAmmo = false;
        gameObject.SetActive(true);
    }

    public int GetHealth()
    {
        return currentHealthPoints;
    }
    

    public void ModifyHealth(int value)
    {
        currentHealthPoints += value;
        if (currentHealthPoints > maxHealthPoints)
            currentHealthPoints = maxHealthPoints;
        if (currentHealthPoints <= 0)
            this.gameObject.SetActive(false);
        
    }

    public void OnMovement(AudioSource source, string horizontalAxis, string verticalAxis)
    {
        if (Input.GetButton(horizontalAxis))
        {
            anim.SetBool("moving", true);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        if (Input.GetButton(verticalAxis))
        {
            anim.SetBool("moving", true);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }

        if (!Input.GetButton(horizontalAxis) && !Input.GetButton(verticalAxis))
        {
            anim.SetBool("moving", false);
            source.Pause();
        }
    }

    public IEnumerator whitecolor()
    {
        yield return new WaitForSeconds(0.02f);
        body.color = Color.white;
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

}
