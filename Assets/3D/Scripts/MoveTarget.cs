using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTarget : MonoBehaviour
{
    [SerializeField] public NavMeshAgent nmAgent;
    [SerializeField] public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        nmAgent.speed = gameObject.GetComponent<EnemyManager>().Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.objectif_position != null)
            nmAgent.SetDestination(PlayerManager.instance.Objectif_Position());
    }
}
