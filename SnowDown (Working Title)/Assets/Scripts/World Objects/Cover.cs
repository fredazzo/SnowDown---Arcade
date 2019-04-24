using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public float timer = 0.0f;

    public AudioSource source;
    public AudioClip hitClip;
    public AudioClip spawnClip;

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            source.clip = hitClip;
            source.Play();
        }
    }


}
