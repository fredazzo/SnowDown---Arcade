﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimAmmo : MonoBehaviour
{
    public float timer = 0f;

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
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBase>().unlimAmmo = true;
            gameObject.SetActive(false);
        }

    }
}
