using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class three_D : MonoBehaviour
{
    public Text _Score;
    public Text _Time;

    private float currenttime;

    public static three_D instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
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

    public void Timedisplayer()
    {
        currenttime += 1 * Time.deltaTime;
        string _minutes;
        string _seconds;

        float minutes = Mathf.Floor(currenttime / 60);
        float seconds = Mathf.RoundToInt(currenttime % 60);

        if (minutes < 10)
        {
            _minutes = "0" + minutes.ToString();
        }
        else
            _minutes = minutes.ToString("0");

        if (seconds < 10)
        {
            _seconds = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
            _seconds = seconds.ToString("0");

        _Time.text = _minutes + ":" + _seconds;
    }

    public void Score(int points)
    {
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void launchMenu()
    {
        SceneManager.LoadScene("2DMenu");
    }
    public void quitGame()
    {
        //EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
