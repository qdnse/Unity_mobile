using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TD_Manager : MonoBehaviour
{
    [Header("TD Animation")]
    [SerializeField] public ParticleSystem particle;

    [Header("TD Characteristics")]
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentHealth = 0f;

    private bool isdead;

    public static TD_Manager instance;

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
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            isdead = true;
            TD_Death();
        }
    }

    //DEATH
    public void TD_Death()
    {
        if (isdead) {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.SetActive(false);
            PlayerManager.instance.gameObject.SetActive(false);
            ParticleSystem clone = Instantiate(particle, transform.position, Quaternion.identity);
            clone.GetComponentInChildren<Renderer>().material.color = color;
            UI_interaction.instance._Panel_Score_EnemyCount.SetActive(false);
            UI_interaction.instance._GameOver.SetActive(true);
            UI_interaction.instance._GameOver_Score.text = "Score: " + GameManager.instance.threeD_game._score.ToString(); 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Enemy(Clone)")
        {
            Destroy(other.gameObject);
            CurrentHealth -= 5;
        }
    }
}
