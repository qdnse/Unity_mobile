using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Characteristics")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Player Visualizer")]
    public HealthBar healthbar;
    public SpriteRenderer graphics;
    public Text _EnemyKilled;
    public Text _GemCount;
    public Text _CherryCount;
    public float InvincibilityTime = 3f;
    public float InvincibilityFlashDelay = 0.15f;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
        healthbar.setHealth(currentHealth);
        _EnemyKilled.text = Inventory.instance.EnemiesKilledCount.ToString();
        _GemCount.text = Inventory.instance.GemCount.ToString();
        _CherryCount.text = Inventory.instance.CherryCount.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthbar.setHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(InvincibilityTime);
        isInvincible = false;
    }
}
