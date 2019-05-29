using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 5.0f;
    private void OnEnable()
    {
        UpdateHandler.UpdateOccured += ControlPlayerMovement;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccured -= ControlPlayerMovement;
    }

    void ControlPlayerMovement()
    {
        float deltaX = Input.GetAxis("Horizontal") * walkSpeed;
        float deltaY = Input.GetAxis("Vertical") * jumpSpeed;
    }
}
