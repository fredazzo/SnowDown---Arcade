using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rb;

    public int healthPoints;
    public int clipSize;
    public int reloadAmount;

    public float speed;
    public float rotateSpeed;
    protected float rotaionInRadians;

    public GameObject shot;
    public GameObject[] shotPool;

    public Transform shotSpawn;

    public AudioSource source;
    public AudioClip hitClip;
    public AudioClip moveClip;
    public AudioClip shootClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            healthPoints--;
            source.clip = hitClip;
            source.Play();
        }
    }

}
