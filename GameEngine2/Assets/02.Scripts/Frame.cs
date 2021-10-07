using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    float deltaTime = 0f;
    [SerializeField, Range(1, 100)]
    int size = 10;

    [SerializeField]
    Color color = Color.green;

    public bool isShow = true;

    private void Awake()
    {
        //Application.targetFrameRate = 144;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.F1))
            isShow = !isShow;
    }

    private void OnGUI()
    {
        if(isShow)
        {
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(30, 30, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;

            float ms = deltaTime * 1000f;
            float fps = 1f / deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

            GUI.Label(rect, text, style);
        }
    }
}
