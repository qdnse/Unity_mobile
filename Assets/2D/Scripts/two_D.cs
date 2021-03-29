using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class two_D : MonoBehaviour
{
    public Text _EnemyKilled;
    public Text _GemCount;
    public Text _CherryCount;

    public Text _Score;
    public Text _Time;

    public bool isGameOver;
    public int _score;

    private float currenttime;

    public static two_D instance;

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
        isGameOver = false;
        AudioSystem.instance.AddAudio_Ambient(AudioSystem.instance.ambient);
        currenttime = 0f;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();

        if (!isGameOver)
        {
            _EnemyKilled.text = Inventory.instance.EnemiesKilledCount.ToString();
            _GemCount.text = Inventory.instance.GemCount.ToString();
            _CherryCount.text = Inventory.instance.CherryCount.ToString();
            _Score.text = "Score : " + _score.ToString();
            Timedisplayer();
        }
    }

    void InputManager()
    {

        if (Input.GetButtonDown("Escape"))
        {
            // Editor
            // UnityEditor.EditorApplication.isPlaying = false;
            // Build
            Application.Quit();
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
        _score += points;
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
        // Editor
        // UnityEditor.EditorApplication.isPlaying = false;
        // Build
        Application.Quit();
    }
}