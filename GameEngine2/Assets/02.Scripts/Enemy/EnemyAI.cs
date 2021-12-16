using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    private EnemyCollide Enemy;
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    private GameObject targetPlayer;
    [SerializeField] private Transform destination;
    [SerializeField] private float distance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Enemy = GetComponent<EnemyCollide>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        distance = 7.0f;

        targetPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("AI:" + Enemy.curHp);
        
        //else if(EnemyCollide.enemyHit)
        //   navMeshAgent.destination = gameObject.transform.position;

        if (Alive())
        {
            navMeshAgent.SetDestination(destination.transform.position);
            if (GetDistance())
                navMeshAgent.SetDestination(targetPlayer.transform.position);
        }
        else
        {
            //navMeshAgent.destination = gameObject.transform.position;
            navMeshAgent.SetDestination(gameObject.transform.position);
        }
    }

    public bool Alive()
    {
        return Enemy.curHp > 0;
    }

    public bool GetDistance()
    {
        if (Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position) < distance)
        {
            Debug.Log("Close!!!");
            return true;
        }
        else
        {
            return false;
        }
    }
}