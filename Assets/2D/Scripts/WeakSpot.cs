using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            Inventory.instance.Enemykilled(1);
            Destroy(transform.parent.transform.parent.gameObject);
        }
    }
}
