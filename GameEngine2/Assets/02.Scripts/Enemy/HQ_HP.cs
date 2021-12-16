using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ_HP : MonoBehaviour
{
    public int maxHp;
    public int curHp;
    public Image HpBar;
    
    [SerializeField] private Transform enemy;
    [SerializeField] private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.rectTransform.localScale =
            new Vector3((float) curHp > 0 ? (float) curHp / (float) maxHp : 0, 1f, 1f);

        if (GetDistanceHQ())
        {
            curHp -= 10;
            Debug.Log(curHp);
        }
    }
    
    public bool GetDistanceHQ()
    {
        if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < distance)
        {
            Debug.Log("HQ Close!!!");
            return true;
        }
        else
        {
            return false;
        }
    }
}
