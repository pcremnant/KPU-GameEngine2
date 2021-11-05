using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSphere : MonoBehaviour, IDamageable
{
    public TutorialMgr tutorialMgr;
    public int Index;
    void Start()
    {

    }

    void Update()
    {

    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        tutorialMgr.HitSphere(Index);
        return false;
    }
}
