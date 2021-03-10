﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController controller;
    public ParticleSystem particle;

    public float speed = 12f;

    public static PlayerManager instance;

    public Transform objectif_position;

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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        CameraFollow.instance.getPosition_threeD(gameObject.transform.position);
    }

    public void Player_Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    public Vector3 Objectif_Position()
    {
        return objectif_position.position;
    }
}