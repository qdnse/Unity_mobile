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
    public GameObject GameOver;
    public HealthBar healthbar;
    public SpriteRenderer graphics;
    public float InvincibilityTime = 3f;
    public float InvincibilityFlashDelay = 0.15f;
    public bool isInvincible = false;

    private Vector3 position_Player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(7f, 7f, 1f);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(80);
        }
        healthbar.setHealth(currentHealth);

        if (currentHealth > 0 && !CameraFollow.instance.deathzone)
            CameraFollow.instance.getPosition_twoD(gameObject.transform.position);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthbar.setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        GameManager.instance.twoD_game.isGameOver = true;
        PlayerMovement.instance.enabled = false;
        gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        PlayerMovement.instance.animator.SetTrigger("Die");
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(HandleInvincibilityDelay());
        GameOver.SetActive(true);
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
