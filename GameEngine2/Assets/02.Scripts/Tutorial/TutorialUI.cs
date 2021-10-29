using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
        //transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
