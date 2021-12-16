using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBillboard : MonoBehaviour
{
    public Transform view;
    
    // Start is called before the first frame update
    void Start()
    {
        view = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + view.rotation * Vector3.forward, view.rotation * Vector3.up);
    }
}