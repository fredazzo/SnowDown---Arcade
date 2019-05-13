using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public int damage;

    public Vector2 movement;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void Update()
    {
        if (anim.GetBool("isFinished") && !anim.GetBool("collided"))
            gameObject.SetActive(false);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cover")
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("collided", true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
