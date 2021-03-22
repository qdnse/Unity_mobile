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
    [SerializeField] public GameObject _Panel_Score_EnemyCount;
    [SerializeField] public GameObject _Pause;
    [SerializeField] public GameObject _Shop;
    [SerializeField] public GameObject _GameOver;
    
    [Header("Shop")]
    [SerializeField] public Color[] button_color;

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
        Shop();
    }

    public void Player_Stat()
    {
        GameObject clone = StatDisplayer.transform.GetChild(0).gameObject;
        Player_Stat_TMP(clone.transform.GetChild(0).transform, PlayerManager.instance.MaxHealth.ToString());
        Player_Stat_TMP(clone.transform.GetChild(1).transform, PlayerManager.instance.CurrentHealth.ToString());
        Player_Stat_TMP(clone.transform.GetChild(2).transform, PlayerManager.instance.Heal.ToString());
        Player_Stat_TMP(clone.transform.GetChild(3).transform, PlayerManager.instance.ShieldDuration.ToString());
        Player_Stat_TMP(clone.transform.GetChild(4).transform, PlayerManager.instance.Damage.ToString());
        Player_Stat_TMP(clone.transform.GetChild(5).transform, PlayerManager.instance.Speed.ToString());
    }

    public void Player_Stat_TMP(Transform obj, string value)
    {
        obj.GetChild(1).GetComponent<Text>().text = value;
    }

    public void Shop()
    {
        Shop_BuyButton(_Shop.transform.GetChild(0).transform, 50);
        Shop_BuyButton(_Shop.transform.GetChild(1).transform, 1);
        Shop_BuyButton(_Shop.transform.GetChild(2).transform, 50);
        Shop_BuyButton(_Shop.transform.GetChild(3).transform, 2);
        Shop_BuyButton(_Shop.transform.GetChild(4).transform, 5);
        Shop_BuyButton(_Shop.transform.GetChild(5).transform, 50);
    }
    public void Shop_BuyButton(Transform obj, int value)
    {
        if (value == 50) { 
            obj.GetChild(4).GetComponent<Image>().color = button_color[1];
        }
        else
        {
            obj.GetChild(4).GetComponent<Image>().color = button_color[0];
        }
    }

    public void Player_Health()
    {
        if (HealthBar.maxValue != PlayerManager.instance.MaxHealth)
            HealthBar.maxValue = PlayerManager.instance.MaxHealth;
        HealthBar.value = PlayerManager.instance.CurrentHealth;
        if (PlayerManager.instance.CurrentHealth <= 0 && PlayerManager.instance.gameObject.activeSelf)
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
