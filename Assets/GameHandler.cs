﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
        }
        /*
        if (Input.GetButtonDown("Pause"))
        {
            //Pause
        }
        if (Input.GetButtonDown("Menu"))
        {
            //Menu
        }
        */
    }
}
