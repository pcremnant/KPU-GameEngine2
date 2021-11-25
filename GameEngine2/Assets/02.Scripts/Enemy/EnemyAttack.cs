using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float attackSpeed;
    public BoxCollider attackRange;
    public TrailRenderer trailEffect;

    public void Attack()
    {
        
    }
    
    public enum State
    {
        Idle,
        Chasing,
        Attacking
    };

    private State currentState;

    private NavMeshAgent pathfinder;
    private Transform target;
    private Material skinMaterial;

    private Color originColor;

    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttacks = 1;

    private float nextAttackTime;
    private float myCollisionRadius;
    private float targetCollisionRadius;
    
    // Start is called before the first frame update
    public void Start()
    {
        // base.Start();
        // pathfinder = GetComponent();
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = 10;
        targetCollisionRadius = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
    }
}
