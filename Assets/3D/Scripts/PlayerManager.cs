﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Animation")]
    [SerializeField] public ParticleSystem particle;

    [Header("Player Characteristics")]
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentHealth = 0f;
    [SerializeField] public float Heal = 5f;
    [SerializeField] public float ShieldDuration = 2f;
    [SerializeField] public float Damage = 50f;
    [SerializeField] public float Speed = 6f;
    [SerializeField] public float Level = 0f;
    [SerializeField] public float CurrentExp = 0f;
    [SerializeField] public float MaxExp = 5f;
    [SerializeField] public float CurrentMoney = 0f;
    [SerializeField] public float MoneyMultiplier = 1f;



    
    [SerializeField] private bool _shooting = false;
    [SerializeField] private float _shootFrequency = 0.35f;
    //public int Ammunition = 15;

    [Header("Private Values")]
    [SerializeField] public bool Shield_isActive;

    [Header("Player Setup")]
    [SerializeField] public CharacterController controller;
    [SerializeField] public Transform objectif_position;
    [SerializeField] public GameObject target;
    [SerializeField] public GameObject bullet;

    public static PlayerManager instance;

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
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        TargetSystem();
    }

    //PLAYER
    public void Player_Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Speed * Time.deltaTime);
        CameraFollow.instance.getPosition_threeD(gameObject.transform.position);
    }

    public IEnumerator Shield_Duration()
    {
        yield return new WaitForSeconds(ShieldDuration);
        Shield_isActive = false;
    }

    //SHOOT
    public void TargetSystem()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        _shootFrequency -= 0.01f;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3 point = hit.point;
            point.y += 0.05f;
            target.transform.position = point;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _shooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _shooting = false;
        }
        if (_shooting && _shootFrequency <= 0) {
            Shoot();
            _shootFrequency = 0.5f;
        }
    }

    public void Shoot()
    {
        if (!GameManager.instance.threeD_game._isPaused) {
            AudioSystem.instance.AddAudio_Effects(AudioSystem.instance.shoot);
            Vector3 pos = gameObject.transform.position;
            Vector2 to = new Vector2(target.transform.position.x, target.transform.position.z);
            float angle = Mathf.Atan2(pos.z - to.y, pos.x - to.x) + Mathf.PI;
            float z = Mathf.Sin(angle);
            float x = Mathf.Cos(angle);
            Vector3 vector = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
            GameObject newBullet = Instantiate(bullet, pos + (vector * 1.05f), Quaternion.identity);
            newBullet.GetComponent<bulletCollider>().dir = vector / 10;
        }
    }

    //OBJECTIF
    public Vector3 Objectif_Position()
    {
        return objectif_position.position;
    }

    public void AddMoney(float Money) {
        CurrentMoney += (Money * MoneyMultiplier);
    }

    public void AddExp(float Exp) {
        CurrentExp += Exp;
        if (CurrentExp >= MaxExp) {
            CurrentExp -= MaxExp;
            Damage += 1;
            Level += 1;
            MaxExp += 5;
        }
    }

    //DEATH
    public void Player_Death()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;
        gameObject.SetActive(false);
        ParticleSystem clone = Instantiate(particle, transform.position, Quaternion.identity);
        clone.GetComponentInChildren<Renderer>().material.color = color;
        UI_interaction.instance._Panel_Score_EnemyCount.SetActive(false);
        UI_interaction.instance._GameOver.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(CameraFollow.instance.CameraShaking.Shake(.15f, .4f));
    }

}
