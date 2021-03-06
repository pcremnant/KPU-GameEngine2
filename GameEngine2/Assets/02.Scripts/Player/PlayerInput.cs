using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal"; 
    public string moveVerticalAxisName = "Vertical"; 

    public string fireButtonName = "Fire1";
    public string jumpButtonName = "Jump"; 
    public string reloadButtonName = "Reload";
    public string throwButtonName = "Throw";


    public Vector3 moveInput { get; private set; } 
    public bool fire { get; private set; } 
    public bool reload { get; private set; }
    public bool jump { get; private set; }
    public bool throwG { get; private set; }

    public bool isMouseLock = false;

    private void Update()
    {
        //if (GameManager.Instance != null
        //    && GameManager.Instance.isGameover)
        //{
        //    moveInput = Vector2.zero;
        //    fire = false;
        //    reload = false;
        //    jump = false;

        //    return;
        //}

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (!isMouseLock)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;

            isMouseLock = !isMouseLock;
        }



        moveInput = new Vector2(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(moveVerticalAxisName));

        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        jump = Input.GetButtonDown(jumpButtonName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
        throwG= Input.GetButtonDown(throwButtonName);
    }
}
