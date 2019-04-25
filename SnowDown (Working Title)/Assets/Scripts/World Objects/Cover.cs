using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public float timer = 0.0f;

    public AudioClip hitClip;
    public AudioClip spawnClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void OnEnable()
    //{
    //    SoundManager.instance.PlaySingle(SoundManager.instance.coverSpawnSource);
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            SoundManager.instance.PlaySingle(SoundManager.instance.coverHitSource);
        }
    }


}
