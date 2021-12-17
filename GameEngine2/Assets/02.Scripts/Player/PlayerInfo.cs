using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IDamageable
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance => instance;
    public MainGame mainGame;

    public Gun gun;
    public int hp = 100;
    public int maxHp = 100;
    public int money = 0;
    public int grenade = 5;

    public static int destroySpanwerCnt = 0;

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
        if (hp < 0)
        {
            hp = 0;
            mainGame.SetAdditionalHp((int)damageMessage.amount);
        }
        else
        {
            hp -= (int)damageMessage.amount;

        }
        return true;
    }

    public void SetDamage(int damage)
    {
        if (hp < 0)
        {
            hp = 0;
            mainGame.SetAdditionalHp(damage);
            //mainGame.AdditionalHp -= damage;
        }
        else
        {
            hp -= damage;
        }
    }


    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Hp", mainGame.AdditionalHp);
        PlayerPrefs.SetInt("gunLevel", mainGame.GunLevel);
        PlayerPrefs.SetInt("magAmmo", gun.magAmmo);
        //PlayerPrefs.SetInt("ammoRemain", gun.ammoRemain);
        PlayerPrefs.SetInt("money", mainGame.Money);
        PlayerPrefs.SetInt("stage", mainGame.MaxStage);
        // PlayerPrefs.SetInt("grenade", grenade);
        PlayerPrefs.SetInt("grenade", grenade);

    }

    public void LoadInfo()
    {
        if (PlayerPrefs.HasKey("Hp"))
        {
            int addHp = PlayerPrefs.GetInt("Hp");
            int money = PlayerPrefs.GetInt("money");
            int maxStage = PlayerPrefs.GetInt("stage");
            int gunLevel = PlayerPrefs.GetInt("gunLevel");
            
            // mainGame.LoadInfo(money, maxStage, gunLevel, maxStage);
            // gun.magAmmo = PlayerPrefs.GetInt("magAmmo");
            // grenade = PlayerPrefs.GetInt("grenade");

            // hp = PlayerPrefs.GetInt("Hp");
            //gun.ammoRemain = PlayerPrefs.GetInt("ammoRemain");
            // money = PlayerPrefs.GetInt("money");
            // grenade = PlayerPrefs.GetInt("grenade");
        }
    }
}
