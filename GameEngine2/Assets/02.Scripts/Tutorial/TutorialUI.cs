using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Transform TextTransform;
    public Text ScoreText;
    void Start()
    {
        //Transform UITransform = transform.GetChild(0).transform;
        //UITransform.GetComponent<Text>();
        //transform.rotation = Quaternion.Euler(0, 180, 0);
        TextTransform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetScoreText(int curScore, int maxScore)
    {
        if (curScore >= maxScore)
            ScoreText.text = "Clear!!";
        else
            ScoreText.text = curScore.ToString() + " / " + maxScore.ToString();
    }
}
