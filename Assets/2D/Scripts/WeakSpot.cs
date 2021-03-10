using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            Inventory.instance.Enemykilled(1);
            GameManager.instance.twoD_game.Score(points);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            if (transform.parent.gameObject.GetComponent<EnemyPatrol>() != null)
                transform.parent.gameObject.GetComponent<EnemyPatrol>().enabled = false;
            if (transform.parent.gameObject.GetComponent<Rigidbody2D>() != null)
                transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; ;
            if (transform.parent.gameObject.GetComponent<EnemyJump>() != null)
                transform.parent.gameObject.GetComponent<EnemyJump>().enabled = false;
            transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(HandleDelay(1f));
        }
    }
    public IEnumerator HandleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
