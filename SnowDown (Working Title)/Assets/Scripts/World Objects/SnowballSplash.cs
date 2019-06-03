using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSplash : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("finished"))
            Destroy(gameObject);
    }
}
