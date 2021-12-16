using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyCollide : MonoBehaviour, IDamageable
{
    public enum MonsterState
    {
        Idle = 0,
        Walk = 1,
        Attack = 2,
        Death = 3
    }

    public int maxHp;
    public int curHp;
    public Image HpBar;
    
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
    
    //[SerializeField] private NavMeshAgent navMeshAgent;
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

        HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        curHp = maxHp;
    }

    private void Update()
    {
        if (enemyDeath)
        {
            deathTime -= Time.deltaTime;
            //Debug.Log("Time: " + deathTime);
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
        HpBar.rectTransform.localScale =
            new Vector3((float) curHp / (float) maxHp > 0 ? (float) curHp / (float) maxHp : 0, 1f, 1f);
    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        curHp -= (int)damageMessage.amount;
        Debug.Log("Attacked!!!: " + curHp);
        
        if (curHp <= 0)
        {
            animator.SetBool("Death", true);
            enemyDeath = true;
        }
        
        enemyHit = true;
        //monsterColor.color = Color.red;

        return false;
    }
}
