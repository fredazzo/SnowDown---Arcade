using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public enum Type
    {
        PLAYER_ONE,
        PLAYER_TWO
    }

    Rigidbody2D rb;
    public float speed;
    private float originalSpeed;
    public int damage;
    
    public Vector2 movement;

    public Animator anim;

    private Type projectileType;

    private CircleCollider2D collision;

    public GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<CircleCollider2D>();
        originalSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void Update()
    {
        if (anim.GetBool("isFinished") && !anim.GetBool("collided"))
        {
            gameObject.SetActive(false);
            ResetProperties();
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cover")
        {
            splash.transform.position = this.transform.position;
            Instantiate(splash);
            anim.SetBool("collided", true);
            speed = 0;
            collision.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetProjectileType(Type p_type)
    {
        projectileType = p_type;
    }

    public Type GetProjectileType()
    {
        return projectileType;
    }

    public void ResetProperties()
    {
        speed = originalSpeed;
        collision.enabled = true;
    }
}
