﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDisplays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
        if (Display.displays.Length > 3)
            Display.displays[3].Activate();
        Debug.Log("displays connected: " + Display.displays.Length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
