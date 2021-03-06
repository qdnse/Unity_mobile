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
            GameManager.instance.Score(points);
            Destroy(transform.parent.transform.parent.gameObject);
        }
    }
}
