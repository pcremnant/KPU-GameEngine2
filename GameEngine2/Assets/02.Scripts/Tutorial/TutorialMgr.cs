using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMgr : MonoBehaviour
{
    public Transform[] sphereTransforms;
    public Transform centerTransform;
    public TutorialUI tutorialUI;
    public float width;
    public float height;
    public int maxScore;
    public int curScore;

    float halfWidth;
    float halfHeight;

    void Start()
    {
        halfWidth = width * 0.4f;
        halfHeight = height* 0.4f;

        for (int i = 0; i < sphereTransforms.Length; ++i)
        {
            RandomSetPosSphere(i);
        }

        tutorialUI.SetScoreText(curScore, maxScore);
    }

    void RandomSetPosSphere(int Index)
    {
        Vector3 vRandomRight = centerTransform.right * Random.RandomRange(-halfWidth, halfWidth);
        Vector3 vRandomUp = new Vector3(0, 1, 0) * Random.RandomRange(-halfHeight, halfHeight);
        sphereTransforms[Index].position = centerTransform.position + (vRandomRight + vRandomUp);
    }


    public void HitSphere(int Index)
    {
        RandomSetPosSphere(Index);

        tutorialUI.SetScoreText(++curScore, maxScore);
    }
}
