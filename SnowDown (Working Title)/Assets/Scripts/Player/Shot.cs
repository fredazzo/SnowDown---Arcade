using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
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
        if (anim.GetBool("isFinished"))
            gameObject.SetActive(false);

        Debug.Log(anim.GetBool("isFinished"));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("collided");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
