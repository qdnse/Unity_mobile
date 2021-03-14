using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // 2D GAME
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            CameraFollow.instance.deathzone = true;
            PlayerHealth playerhealth = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerhealth.currentHealth = 0;
            playerhealth.Die();
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Animator>().SetBool("IsDead", true);

            if (collision.GetComponent<EnemyPatrol>() != null)
                collision.GetComponent<EnemyPatrol>().enabled = false;

            if (collision.transform.parent.gameObject.GetComponent<Rigidbody2D>() != null)
                collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; ;

            if (collision.GetComponent<EnemyJump>() != null)
                collision.GetComponent<EnemyJump>().enabled = false;

            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponentInChildren<BoxCollider2D>().enabled = false;
            StartCoroutine(HandleDelay(1f, collision.gameObject));
        }
    }
    public IEnumerator HandleDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj.transform.parent.gameObject);
    }

    // 3D GAME
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !PlayerManager.instance.Shield_isActive)
        {
            PlayerManager.instance.Player_Death();
        }
    }
}
