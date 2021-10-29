using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMgr : MonoBehaviour
{
    public Transform[] sphereTransforms;
    public Transform centerTransform;
    public Transform playerTransform;
    public float width;
    public float height;

    void Start()
    {
        //Vector3 vCenterToPlayer = (centerTransform.position - playerTransform.position).normalized;
        //Vector3 vUp = new Vector3(0, 1, 0);


        float halfWidth = width * 0.4f;
        float halfHeight = height* 0.4f;

        for (int i = 0; i < sphereTransforms.Length; ++i)
        {
            Vector3 vRandomRight = centerTransform.right * Random.RandomRange(-halfWidth, halfWidth);
            Vector3 vRandomUp = new Vector3(0,1,0) * Random.RandomRange(-halfHeight, halfHeight);
            sphereTransforms[i].position += (vRandomRight + vRandomUp);
        }
    }

    void Update()
    {
        
    }
}
