using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class three_D : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public Text _Score;
    [SerializeField] public Text _EnemyCount;
    [SerializeField] public Text _Time;

    [Header("GameObjects")]
    [SerializeField] public GameObject _SpawnPointList;
    [SerializeField] public GameObject _Enemy;
    [SerializeField] public GameObject _Shooter;
    [SerializeField] public GameObject _Tank;
    [SerializeField] public GameObject _Fast;
    [SerializeField] public GameObject _EnemyList;


    [SerializeField] public bool _isPaused = false;

    [SerializeField] private float currenttime;

    [Header("Tower Defense Mod")]
    [SerializeField] public bool Tower_Defense;

    public int _score = 0;
    private int _maxEnemy = 6;
    private int _spawnLeft = 0;
    private int spawnRate = 200;

    public bool mobile;

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
        AudioSystem.instance.AddAudio_Ambient(AudioSystem.instance.ambient);
        if (Application.platform == RuntimePlatform.Android)
        {
            mobile = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        SpawnManager();
        Display_EnemyCount();
        _Score.text = _score.ToString();
    }

    void Display_EnemyCount()
    {
        _EnemyCount.text = _EnemyList.transform.childCount.ToString() + " / " + (_spawnLeft + _EnemyList.transform.childCount).ToString();
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            UI_interaction.instance.StatDisplayer.SetActive(true);
        } 
        else 
        {
            UI_interaction.instance.StatDisplayer.SetActive(false);
        }

        //if (Input.GetButtonDown("Pause"))
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause_Game();
        }
        Timedisplayer();
    }

    public void Pause_Game()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            UI_interaction.instance._Pause.SetActive(false);
            UI_interaction.instance._Options.SetActive(false);
            if (mobile)
                UI_interaction.instance._JoySticks.SetActive(true);
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            UI_interaction.instance._Pause.SetActive(true);
            if (mobile)
                UI_interaction.instance._JoySticks.SetActive(false);
            _isPaused = true;
        }
    }

    void SpawnManager()
    {
        if (_spawnLeft == 0 &&  _EnemyList.transform.childCount == 0)
        {
            _maxEnemy = (int)Mathf.Floor(_maxEnemy * 1.4f);
            _spawnLeft = _maxEnemy;
        }
        if (spawnRate >= 200 && _spawnLeft > 0 && _EnemyList.transform.childCount < 12)
        {
            SpawnEnemy();
            spawnRate = 0;
            _spawnLeft -= 1;
        }
        if (!_isPaused)
            spawnRate += 1;
    }

    void SpawnEnemy()
    {
        var enemies = new List<GameObject>() { _Enemy, _Shooter, _Tank, _Fast };
        if (!_isPaused) {
            Transform spawn = _SpawnPointList.transform.GetChild(Random.Range(0, _SpawnPointList.transform.childCount));
            GameObject clone = Instantiate(enemies[Random.Range(0, 4)], spawn.position, Quaternion.identity);
            clone.transform.SetParent(_EnemyList.transform);
        }
    }

    public void Timedisplayer()
    {
        currenttime += 1 * Time.deltaTime;
        string _minutes;
        string _seconds;

        float minutes = Mathf.Floor(currenttime / 60);
        float seconds = Mathf.RoundToInt(currenttime % 60);

        _minutes = (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString("0"));
        _seconds = (seconds < 10 ? "0" + Mathf.RoundToInt(seconds).ToString() : seconds.ToString("0"));

        if (PlayerManager.instance.gameObject.activeSelf && !_isPaused)
            _Time.text = _minutes + ":" + _seconds;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        UI_interaction.instance._Pause.SetActive(false);
        if (mobile)
            UI_interaction.instance._JoySticks.SetActive(true);
        _isPaused = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void launchMenu()
    {
        SceneManager.LoadScene("3DMenu");
    }
    public void quitGame()
    {
        // if (Application.isEditor)
            // UnityEditor.EditorApplication.isPlaying = false;
        if (Application.isPlaying)
            Application.Quit();
    }
}
