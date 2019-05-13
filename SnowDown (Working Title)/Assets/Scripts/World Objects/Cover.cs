using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public float timer = 0.0f;
    public Animator anim;
    BoxCollider2D collision;

    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Solid"))
            collision.enabled = true;
        else
            collision.enabled = false;
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            SoundManager.instance.PlaySingle(SoundManager.instance.coverHitSource);
        }
    }


}
