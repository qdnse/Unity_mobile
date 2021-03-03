using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text _EnemyKilled;
    public Text _GemCount;
    public Text _CherryCount;

    // Start is called before the first frame update
    void Start()
    {
        AudioSystem.instance.AddAudio_Ambient(AudioSystem.instance.ambient);
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();

        _EnemyKilled.text = Inventory.instance.EnemiesKilledCount.ToString();
        _GemCount.text = Inventory.instance.GemCount.ToString();
        _CherryCount.text = Inventory.instance.CherryCount.ToString();
    }

    void InputManager()
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
