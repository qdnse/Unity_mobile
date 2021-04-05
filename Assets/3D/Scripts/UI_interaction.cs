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
    [SerializeField] public Slider ExpBar;
    [SerializeField] public GameObject _Panel_Score_EnemyCount;
    [SerializeField] public GameObject _Pause;
    [SerializeField] public GameObject _Options;
    [SerializeField] public GameObject _Shop;
    [SerializeField] public GameObject _GameOver;
    [SerializeField] public GameObject _JoySticks;
    [SerializeField] public Text _Money;
    [SerializeField] public Text _Score;
    [SerializeField] public Text _GameOver_Score;
    [SerializeField] public Slider TowerDefenseBar;

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
        _Options.transform.Find("Audio").Find("Effect").Find("Slider").GetComponent<Slider>().value = AudioSystem.instance.volume_effects;
        _Options.transform.Find("Audio").Find("Ambient").Find("Slider").GetComponent<Slider>().value = AudioSystem.instance.volume_ambient;
    }

    // Update is called once per frame
    void Update()
    {
        _Money.text = PlayerManager.instance.CurrentMoney.ToString();
        Tower_Defense_Health();
        Player_Stat();
        Player_Shield();
        Player_Health();
        Player_Exp();
        Shop();
        OptionsManager();
    }

    public void OptionsManager()
    {
        AudioSystem.instance.volume_effects = _Options.transform.Find("Audio").Find("Effect").Find("Slider").GetComponent<Slider>().value;
        AudioSystem.instance.volume_ambient = _Options.transform.Find("Audio").Find("Ambient").Find("Slider").GetComponent<Slider>().value;
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
        Player_Stat_TMP(_Shop.transform.GetChild(0).transform, PlayerManager.instance.MaxHealth.ToString());
        Player_Stat_TMP(_Shop.transform.GetChild(1).transform, PlayerManager.instance.CurrentHealth.ToString());
        Player_Stat_TMP(_Shop.transform.GetChild(2).transform, PlayerManager.instance.Heal.ToString());
        Player_Stat_TMP(_Shop.transform.GetChild(3).transform, PlayerManager.instance.ShieldDuration.ToString());
        Player_Stat_TMP(_Shop.transform.GetChild(4).transform, PlayerManager.instance.Damage.ToString());
        Player_Stat_TMP(_Shop.transform.GetChild(5).transform, PlayerManager.instance.Speed.ToString());
        Shop_BuyButton(_Shop.transform.GetChild(0).transform, 50);
        Shop_BuyButton(_Shop.transform.GetChild(1).transform, 100000);
        Shop_BuyButton(_Shop.transform.GetChild(2).transform, 100);
        Shop_BuyButton(_Shop.transform.GetChild(3).transform, 100);
        Shop_BuyButton(_Shop.transform.GetChild(4).transform, 50);
        Shop_BuyButton(_Shop.transform.GetChild(5).transform, 40);
    }

    public void Shop_BuyButton(Transform obj, int value)
    {
        if (value <= PlayerManager.instance.CurrentMoney) { 
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
    public void Tower_Defense_Health()
    {
        if (GameManager.instance.threeD_game.Tower_Defense) {
            if (TowerDefenseBar.maxValue != TD_Manager.instance.MaxHealth)
                TowerDefenseBar.maxValue = TD_Manager.instance.MaxHealth;
            TowerDefenseBar.value = TD_Manager.instance.CurrentHealth;
        }
    }

    public void Player_Exp()
    {
        if (ExpBar.maxValue != PlayerManager.instance.MaxExp)
            ExpBar.maxValue = PlayerManager.instance.MaxExp;
        ExpBar.value = PlayerManager.instance.CurrentExp;
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
    public void Player_Shield_Activated()
    {
        if (!PlayerManager.instance.Shield_isActive)
        {
            PlayerManager.instance.Shield_isActive = true;
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

    public void UpgradeMaxHealth() {
        if (PlayerManager.instance.CurrentMoney >= 50)
        {
            PlayerManager.instance.CurrentMoney -= 50;
            PlayerManager.instance.MaxHealth += 10;
            PlayerManager.instance.CurrentHealth += 10;
        }
    }

    public void UpgradeHealthRegen()
    {
        if (PlayerManager.instance.CurrentMoney >= 100)
        {
            PlayerManager.instance.CurrentMoney -= 100;
            PlayerManager.instance.Heal += 1;
        }
    }

    void UpgradeShootRate()
    {
        if (PlayerManager.instance.CurrentMoney >= 50)
        {
            PlayerManager.instance.Damage += 2;
        }
    }

    public void UpgradeDamage()
    {
        if (PlayerManager.instance.CurrentMoney >= 50)
        {
            PlayerManager.instance.CurrentMoney -= 50;
            PlayerManager.instance.Damage += 2;
        }
    }

    public void UpgradeSpeed()
    {
        if (PlayerManager.instance.CurrentMoney >= 40)
        {
            PlayerManager.instance.CurrentMoney -= 40;
            PlayerManager.instance.Speed += 0.5f;
        }
    }

    void UpgradeMoneyMultiplier()
    {
        PlayerManager.instance.MoneyMultiplier += +0.1f;
    }

    void UpgradeShieldRegen()
    {
        PlayerManager.instance.Damage -= (PlayerManager.instance.Damage / 20);
    }

    public void UpgradeShieldDuration()
    {
        if (PlayerManager.instance.CurrentMoney >= 100)
        {
            PlayerManager.instance.CurrentMoney -= 100;
            PlayerManager.instance.ShieldDuration += 0.1f;
        }
    }

    public void Endgame()
    {
        if (PlayerManager.instance.CurrentMoney >= 100000)
        {
            PlayerManager.instance.CurrentMoney -= 100000;
            GameManager.instance.threeD_game._score += 50000;
            GameManager.instance.threeD_game.Resume();
            PlayerManager.instance.Player_Death();
        }
    }
}
