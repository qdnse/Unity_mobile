using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public int damageOnCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            PlayerHealth playerhealth = collision.transform.GetComponent<PlayerHealth>();
            playerhealth.TakeDamage(damageOnCollision);
        }
    }
}
