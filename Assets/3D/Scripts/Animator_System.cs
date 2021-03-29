using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Animator_System : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Bt_Menu_Game()
    {
        animator.enabled = true;
        animator.SetBool("Menu_Game", true);
        HandleDelay(2f);
    }

    public void Bt_Menu_Options()
    {
        animator.enabled = true;
        animator.SetBool("ToOption", true); // ("Menu_Options", true);
        HandleDelay(1f);
    }
    public void Bt_Options_Menu()
    {
        animator.enabled = true;
        animator.SetBool("ToOption", false); // ("Menu_Options", true);
        HandleDelay(1f);
    }

    public void PlayScene() {
        SceneManager.LoadScene("Survival");
    }

    public void QuitGame() {
        // Editor
        // UnityEditor.EditorApplication.isPlaying = false;
        // Build
        Application.Quit();
    }

    public IEnumerator HandleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = false;
    }

    public IEnumerator HandleDelayMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = true;
        animator.SetInteger("Menu_Options 0", 0);
        animator.enabled = false;
    }
}
