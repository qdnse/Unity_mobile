﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchGame()
    {
        SceneManager.LoadScene("2d game");
    }
    public void quitGame()
    {
        // Editor
        // UnityEditor.EditorApplication.isPlaying = false;
        // Playing
        Application.Quit();
    }
}
