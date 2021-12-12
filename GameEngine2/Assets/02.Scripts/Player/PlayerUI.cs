using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text HpText;
    public Text AmmoText;
    public Text GrenadeText;

    //public Gun gun;
    //public PlayerHp playerHp;
    void Start()
    {
        
    }

    void Update()
    {
        //HpText.text = "Hp: " + playerHp.hp.ToString();
        HpText.text = "Hp: " + PlayerInfo.Instance.hp.ToString();
        AmmoText.text = "Ammo:" + PlayerInfo.Instance.gun.magAmmo.ToString() + " / " + PlayerInfo.Instance.gun.ammoRemain.ToString();
        GrenadeText.text = "Grenade: " + PlayerInfo.Instance.grenade.ToString();
    }
}
