using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private bool isColliding;
    public Animator animator;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColliding) {
            OpenChest();
        }
    }

    void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        //GetComponent<BoxCollider2D>().enabled = false;
        Inventory.instance.AddGem(5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            isColliding = false;
        }
    }
}
