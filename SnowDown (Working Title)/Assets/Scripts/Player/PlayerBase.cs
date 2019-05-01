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

    protected bool moving;
    protected bool hit;

    public SpriteRenderer sprite;


    //public AudioClip hitClip;
    //public AudioClip moveClip;
    //public AudioClip shootClip;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnMovement(AudioSource source, string horizontalAxis, string verticalAxis)
    {
        if (Input.GetButton(horizontalAxis))
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        if (Input.GetButton(verticalAxis))
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }

        if (!Input.GetButton(horizontalAxis) && !Input.GetButton(verticalAxis))
        {
            source.Pause();
        }
    }

    public IEnumerator whitecolor()
    {
        yield return new WaitForSeconds(0.02f);
        sprite.color = Color.white;
        hit = false;
    }
}
