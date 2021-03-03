using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
