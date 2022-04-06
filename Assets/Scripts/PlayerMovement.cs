using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    private bool canRun = true;
    private bool canJump = true;
    private bool canCrouch = true;
    
    // déplacement
    private float speed;
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private Vector3 velocity;
    
    // sauts et gravité
    private float gravity = -9.81f * 2;
    private float groundDistance = 0.4f;
    private float jumpHeight = 3.5f;
    public void SetJumpHeight(float jumpHeight)
    {
        this.jumpHeight = jumpHeight;
    }
    private bool isGrounded;
    private Transform groundCheck;
    private LayerMask groundMask;
    private float slamForce = 50f;
    
    // accroupissement
    private float ceilDistance = 0.4f;
    private bool isCrouched;
    private Transform ceilCheck;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("Ground Check");
        ceilCheck = transform.Find("Ceil Check");
        groundMask = LayerMask.GetMask("Ground");

        if (!canRun) 
            speed = 3f;
        else 
            speed = 10f;
    }
    

    void Update()
    {
        HandleMovement();
    }
    

    void HandleMovement()
    {
        // === DÉPLACEMENTS ===
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * speed);
        
        // === SAUTS ===
        if(canJump)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            // si on est au sol et que le joueur ne bouge plus sur l'axe Y
            if (isGrounded && velocity.y < 0) velocity.y = -2f;

            // si on est au sol et que le joueur appuie sur le bouton de saut
            if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        // === GRAVITÉ ===
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        
        // === S'ACCROUPIR ===
        if (canCrouch)
        {
            isCrouched = Physics.CheckSphere(ceilCheck.position, ceilDistance, groundMask);

            if (Input.GetButton("Crouch"))
            {
                // si le joueur est en plein saut et qu'il appuie sur le bouton pour s'accroupir
                if (!isGrounded) velocity.y -= groundDistance * slamForce * (-gravity * 0.1f);

                // quand le player s'accroupit, sa hauteur est réduite de moitié
                // de même pour le rayon de collision du CharacterController
                transform.localScale = new Vector3(1f, 0.5f, 1f);
                controller.height = 0.9f;
            }
            else
            {
                // si le joueur est coincé entre le sol et le plafond, il reste accroupi
                switch (isCrouched)
                {
                    case true:
                        transform.localScale = new Vector3(1f, 0.5f, 1f);
                        controller.height = 0.9f;
                        break;

                    case false:
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        controller.height = 1.9f;
                        break;
                }
            }
        }
    }
}