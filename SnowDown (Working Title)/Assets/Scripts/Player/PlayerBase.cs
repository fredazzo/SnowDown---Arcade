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

    public AudioClip hitClip1;
    public AudioClip hitClip2;
    public AudioClip moveClip1;
    public AudioClip moveClip2;
    public AudioClip shootClip1;
    public AudioClip shootClip2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }


}
