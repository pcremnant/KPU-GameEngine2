using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyCollide Enemy;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Enemy = GetComponent<EnemyCollide>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("AI:" + Enemy.hp);
        if(Alive())
            navMeshAgent.destination = target.transform.position;
        else if(EnemyCollide.enemyHit)
            navMeshAgent.destination = gameObject.transform.position;
        else
        {
            navMeshAgent.destination = gameObject.transform.position;
        }
    }

    public bool Alive()
    {
        return Enemy.hp > 0;
    }
}