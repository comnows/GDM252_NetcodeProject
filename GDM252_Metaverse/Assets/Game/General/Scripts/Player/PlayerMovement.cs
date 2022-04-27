using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;

    private float horizonInput;
    private float verticalInput;
    private bool isJump;

    private Vector3 moveDirection;

    public float moveSpeed = 12f;
    public float gravityForce = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;

    void Update()
    {
        if(IsClient && IsOwner)
        {
            GetInput();
            GetMoveDirection();
            Move();
            Jump();
            SetGravity();
        }
    }

    private void GetInput()
    {
        horizonInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isJump = Input.GetButtonDown("Jump");
    }

    private void GetMoveDirection()
    {
        moveDirection = transform.right * horizonInput + transform.forward * verticalInput;
    }

    private void Move()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if(IsGround() && isJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce); 
        }
    }

    private bool IsGround()
    {
        if(!Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            return false;
        }
        
        return true;
    }

    private void SetGravity()
    {
        if(IsGround() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravityForce * Time.deltaTime;
    
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
