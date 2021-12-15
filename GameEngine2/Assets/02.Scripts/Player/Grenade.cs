using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    //public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rigid;

    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);

        SoundMgr.instance.SFXPlay("Explosion", SoundMgr.instance.effectClip[1]);

        meshRenderer.enabled = false;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshRenderer.enabled = false;
        effectObj.SetActive(true);
        //meshObj.SetActive(false);

        RaycastHit[] raycastHits =  Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f);
        foreach (RaycastHit hit in raycastHits)
        {
            var target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                DamageMessage damageMessage;

                damageMessage.damager = gameObject;
                damageMessage.amount = 10;
                damageMessage.hitPoint = hit.point;
                damageMessage.hitNormal = hit.normal;

                // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지 주기
                target.ApplyDamage(damageMessage);
            }
        }
        Destroy(gameObject, 5); //이펙트 시간
    }
}
