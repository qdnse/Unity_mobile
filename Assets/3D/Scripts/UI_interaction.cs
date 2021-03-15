using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_interaction : MonoBehaviour
{
    [Header("Player Characteristics")]
    [SerializeField] public GameObject StatDisplayer;

    [Header("Player UI")]
    [SerializeField] public Slider ShieldBar;
    [SerializeField] public Slider HealthBar;

    public static UI_interaction instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player_Stat();
        Player_Shield();
        Player_Health();
    }

    public void Player_Stat()
    {
        GameObject clone = StatDisplayer.transform.GetChild(0).gameObject;
        clone.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.MaxHealth.ToString();
        clone.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.CurrentHealth.ToString();
        clone.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.Heal.ToString();
        clone.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.ShieldDuration.ToString();
        clone.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.Damage.ToString();
        clone.transform.GetChild(5).transform.GetChild(1).GetComponent<Text>().text = PlayerManager.instance.Speed.ToString();
    }
    public void Player_Health()
    {
        if (HealthBar.maxValue != PlayerManager.instance.MaxHealth)
            HealthBar.maxValue = PlayerManager.instance.MaxHealth;
        HealthBar.value = PlayerManager.instance.CurrentHealth;
        if (PlayerManager.instance.CurrentHealth <= 0)
        {
            PlayerManager.instance.Player_Death();
        }
    }

    public void Player_Shield()
    {
        if (PlayerManager.instance.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.E) && !PlayerManager.instance.Shield_isActive)
            {
                PlayerManager.instance.Shield_isActive = true;
            }
            else if (PlayerManager.instance.Shield_isActive)
            {
                ShieldBar.maxValue = PlayerManager.instance.ShieldDuration;
                ShieldBar.value -= 1 * Time.deltaTime;
                StartCoroutine(Shield_Activated());
                StartCoroutine(PlayerManager.instance.Shield_Duration());
            }
            else if (!PlayerManager.instance.Shield_isActive)
            {
                PlayerManager.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                ShieldBar.value += 1 * Time.deltaTime;
            }
        }
    }

    public IEnumerator Shield_Activated()
    {

        while (PlayerManager.instance.Shield_isActive)
        {
            PlayerManager.instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(PlayerManager.instance.ShieldDuration);
        }
    }

}
