using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform gunMuzzle;

    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(playerInput.fire)
        {
            Shoot();
        }
    }

    public void Shoot()
    {

    }
}
