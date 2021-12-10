using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IDamageable
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance => instance;

    public Gun gun;
    public int hp = 100;
    public int maxHp = 100;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        hp -= (int)damageMessage.amount;
        if (hp < 0)
            hp = 0;
        return true;
    }
}
