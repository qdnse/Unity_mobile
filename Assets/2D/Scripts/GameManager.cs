using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public two_D twoD_game;
    [SerializeField] public three_D threeD_game;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        twoD_game = two_D.instance;
        threeD_game = three_D.instance;
    }
}
