using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rb;

    public int healthPoints;
    public int clipSize;
    public int reloadAmount;

    //protected float nextFire = 0.0f;
    //public float fireRate;
    public float speed;
    public float rotateSpeed;

    public GameObject shot;
    public GameObject[] shotPool;

    public Transform shotSpawn;

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
        }
    }

}
