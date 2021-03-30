using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Animation")]
    [SerializeField] public ParticleSystem particle;

    [Header("Enemy Characteristics")]
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentHealth = 0f;
    [SerializeField] public float Damage;
    [SerializeField] public float Speed = 6f;
    [SerializeField] public int value = 5;

    [Header("Enemy UI")]
    [SerializeField] public Slider HealthBar;
    [SerializeField] public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = MaxHealth;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_Health();
    }

    public void Enemy_Health()
    {
        canvas.transform.forward = Camera.main.transform.forward;
        if (HealthBar.maxValue != MaxHealth)
            HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Enemy_Death();
        }
    }

    //DEATH
    public void Enemy_Death()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;
        Destroy(gameObject);
        ParticleSystem clone = Instantiate(particle, transform.position, Quaternion.identity);
        clone.GetComponentInChildren<Renderer>().material.color  = color;
        PlayerManager.instance.AddExp(5f);
        PlayerManager.instance.AddMoney(10f);
        GameManager.instance.threeD_game._score += value;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && !PlayerManager.instance.Shield_isActive)
        {
            PlayerManager.instance.TakeDamage(Damage);
        }
    }

}
