using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private int moveSpeed;
    private CharacterController characterController;
    private Vector3 moveVector;

    private float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float gravityVelocity;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * moveSpeed * Time.deltaTime / Screen.width;

        // when we drag knob in the y direction of the touch screen, character must move in the z direction in the 3d unity scene
        moveVector.z = moveVector.y;
        moveVector.y = 0; // character will not move in the y direction in the 3d unity scene

        playerAnimator.ManageAnimations(moveVector);
        ApplyGravity();
        characterController.Move(moveVector);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && gravityVelocity <= 0)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        moveVector.y = gravityVelocity;
    }
}
