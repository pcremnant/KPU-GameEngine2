using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollide : MonoBehaviour, IDamageable
{
    public enum MonsterState
    {
        Idle = 0,
        Walk = 1,
        Attack = 2,
        Death = 3
    }

    public int hp;
    public Animator animator;
    private bool enemyDeath;
    private float deathTime;
    
    public Material monsterColor;
    private Material originalColor;
    public static bool enemyHit;
    private float colorTime;
    private Renderer myColor;

    private float delayTime;
    private float originalSpeed;

    private Ray ray;
    private RaycastHit hit;

    private Spawner spawner;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spawner = GetComponent<Spawner>();

        animator.SetBool("Death", false);
        enemyDeath = false;
        deathTime = 3;
        
        enemyHit = false;
        colorTime = 1;

        delayTime = 1;
        originalSpeed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    private void Update()
    {
        if (enemyDeath)
        {
            deathTime -= Time.deltaTime;
            Debug.Log("Time: " + deathTime);
            if (deathTime <= 0.0)
                GameObject.Destroy(gameObject);
        }

        //if (enemyHit)
        {
            //colorTime -= Time.deltaTime;
            //delayTime -= Time.deltaTime;
            
            //Debug.Log("Color Time: " + colorTime);
            //if (colorTime <= 0.0)
            {
                //monsterColor.color = originalColor.color;
                //Debug.Log("Color Time:  Done!!!");
                //colorTime = 10;
                //enemyHit = false;
            }

            //if (delayTime <= 0.0)
            {
                //deathTime = 10;
                //navMeshAgent.destination = target.transform.position;
            }
        }
    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        hp -= (int)damageMessage.amount;
        Debug.Log(hp);
        
        if (hp <= 0)
        {
            animator.SetBool("Death", true);
            enemyDeath = true;
        }
        
        enemyHit = true;
        //monsterColor.color = Color.red;

        return false;
    }
}
