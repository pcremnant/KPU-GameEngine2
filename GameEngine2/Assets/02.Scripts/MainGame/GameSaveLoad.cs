using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad : MonoBehaviour
{
    public void Save()
    {
        PlayerPrefs.SetInt("Hp", PlayerInfo.Instance.hp);
        PlayerPrefs.SetInt("magAmmo", PlayerInfo.Instance.gun.magAmmo);
        PlayerPrefs.SetInt("ammoRemain", PlayerInfo.Instance.gun.ammoRemain);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Hp"))
        {
            PlayerInfo.Instance.hp = PlayerPrefs.GetInt("Hp"); 
            PlayerInfo.Instance.gun.magAmmo = PlayerPrefs.GetInt("magAmmo");
            PlayerInfo.Instance.gun.ammoRemain = PlayerPrefs.GetInt("ammoRemain");
        }
    }
}
