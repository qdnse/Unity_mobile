using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject Environment;
    // Dropped Item
    public GameObject Item;
    // Over 100 of DropChances, a modulo of 100 is applied
    public int DropChances;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player" && DropChances >= 0)
        {
            if (Random.Range(0, 100) <= (DropChances % 100) || (DropChances % 100) == 0 ) {
                GameObject childObject = Instantiate(Item) as GameObject;
                childObject.transform.parent = Environment.transform;
                childObject.transform.position = collision.gameObject.transform.position;
            }
        }
    }
}
