using System.Collections;
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
    [SerializeField] public float Damage = 15f;
    [SerializeField] public float Speed = 6f;
    //public int Ammunition = 15;

    [Header("Private Values")]
    [SerializeField] public bool Shield_isActive;

    [Header("Player Setup")]
    [SerializeField] public CharacterController controller;
    [SerializeField] public Transform objectif_position;
    [SerializeField] public GameObject target;

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
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            target.transform.position = hit.point;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(target, target.transform.position, Quaternion.identity);
    }

    //OBJECTIF
    public Vector3 Objectif_Position()
    {
        return objectif_position.position;
    }

    //DEATH
    public void Player_Death()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;
        Destroy(gameObject);
        ParticleSystem clone = Instantiate(particle, transform.position, Quaternion.identity);
        clone.GetComponentInChildren<Renderer>().material.color = color;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(CameraFollow.instance.CameraShaking.Shake(.15f, .4f));
    }

}
