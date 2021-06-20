using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedI = 10f;
    public float speedF;
    public float gravity = 2f;
    public float jumpSpeed = 0.5f;
    public CharacterController controller;
    public  Vector3 moveDirection;
    private bool crouching;
    private bool running;
    private bool isJumping;

    void FixedUpdate()
    {
        MoveAndJump();
        Crouch();
        Run();

        if(crouching == false && running == false)
        {
            speedF = speedI;
        }
    }
    void MoveAndJump()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);
        Vector3 flatMovement = speedF * Time.deltaTime * transformDirection;
        moveDirection = new Vector3(flatMovement.x, moveDirection.y, flatMovement.z);
        if (Input.GetKeyDown(KeyCode.K) && controller.isGrounded)
        {   
            moveDirection.y = jumpSpeed;
            isJumping = true;
        }
        else if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            isJumping = false;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; 
        }
        controller.Move(moveDirection);

    }
    void Crouch()
    {
        if (Input.GetKey(KeyCode.L) && isJumping == false)
        { 
            speedF = speedI/2;
            controller.transform.localScale = new Vector3(2f, 1.5f, 2f);
            crouching = true;
        }
        else
        {
            controller.transform.localScale = new Vector3(2f, 2f, 2f);
            crouching = false;
        }
    }    

    void Run()
    {
        if(Input.GetKey(KeyCode.I) && crouching == false)
        {
            speedF = speedI * 1.5f;
            running = true;
        }
        else
        {
            running = false;
        }
    }
}
