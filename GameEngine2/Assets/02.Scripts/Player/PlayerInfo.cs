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
    public int money = 0;
    public int grenade = 5;


    void Awake()
    {
        instance = this;
        grenade = 5;
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


    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Hp", hp);
        PlayerPrefs.SetInt("magAmmo", gun.magAmmo);
        //PlayerPrefs.SetInt("ammoRemain", gun.ammoRemain);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("grenade", grenade);

    }

    public void LoadInfo()
    {
        if (PlayerPrefs.HasKey("Hp"))
        {
            hp = PlayerPrefs.GetInt("Hp");
            gun.magAmmo = PlayerPrefs.GetInt("magAmmo");
            //gun.ammoRemain = PlayerPrefs.GetInt("ammoRemain");
            money = PlayerPrefs.GetInt("money");
            grenade = PlayerPrefs.GetInt("grenade");
        }
    }
}
