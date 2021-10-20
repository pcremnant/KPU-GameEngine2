using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    public int hp;
    private Ray ray;
    private RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
    }

    private void Update()
    {
        
    }
}
