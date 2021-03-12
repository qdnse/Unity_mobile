using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public int cherry_points;
    public int gem_points;
    public bool cherry;
    public bool gem;

    public Color cherry_color;
    public Color gem_color;

    [SerializeField] public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player" && gem)
        {
            Inventory.instance.AddGem(1);
            AudioSystem.instance.AddAudio_Effects(AudioSystem.instance._pickup1);
            GameManager.instance.twoD_game.Score(gem_points);
            gem = false;
            gameObject.GetComponent<SpriteRenderer>().color = gem_color;
            gameObject.GetComponent<Animator>().SetBool("PickedUp", true);
            StartCoroutine(HandleDelay(1f));
        }
        
        if (collision.transform.gameObject.name == "Player" && cherry)
        {
            PlayerHealth playerhealth = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerhealth.currentHealth += 10;
            Inventory.instance.AddCherry(1);
            AudioSystem.instance.AddAudio_Effects(AudioSystem.instance.shoot);
            GameManager.instance.twoD_game.Score(cherry_points);
            cherry = false;
            gameObject.GetComponent<SpriteRenderer>().color = cherry_color;
            gameObject.GetComponent<Animator>().SetBool("PickedUp", true);
            StartCoroutine(HandleDelay(1f));
        }
        
    }
    
    public IEnumerator HandleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
}
