using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public int cherry_points;
    public int gem_points;
    public bool cherry;
    public bool gem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player" && gem)
        {
            Inventory.instance.AddGem(1);
            AudioSystem.instance.AddAudio_Effects(AudioSystem.instance.pickup);
            GameManager.instance.Score(gem_points);
            gem = false;
            Destroy(gameObject);
        }
        
        if (collision.transform.gameObject.name == "Player" && cherry)
        {
            PlayerHealth playerhealth = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerhealth.currentHealth += 10;
            Inventory.instance.AddCherry(1);
            AudioSystem.instance.AddAudio_Effects(AudioSystem.instance.shoot);
            GameManager.instance.Score(cherry_points);
            cherry = false;
            Destroy(gameObject);
        }
        
    }
}
