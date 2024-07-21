using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [Header("Movement Info")]
    [SerializeField] private float speed;
    private Vector3 movementDirection;
    private float verticalVelocity;
    public Vector2 moveInput { get; private set; }

    bool isGravityOnLeft = false;
    bool isGravityOnRight = false;
    bool isGravityOnDown = false;
    bool isGravityOnUp = false;

    private void Awake()
    {
        controls = new PlayerControls();

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        AssignInputEvents();
    }

    private void Update()
    {

        ApplyMovement();
        SetGravityDirection();

    }

    private void SetGravityDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isGravityOnLeft = true;
            isGravityOnRight = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isGravityOnLeft = false;
            isGravityOnRight = true;

            transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Debug.Log(moveInput);

        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * Time.deltaTime * speed);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
            if(isGravityOnLeft)
                movementDirection.x = verticalVelocity;
            else if(isGravityOnRight)
                movementDirection.x = -verticalVelocity;
            else
                movementDirection.y = verticalVelocity;
        }
        else
            verticalVelocity = -0.5f;
    }

    private void AssignInputEvents()
    {

        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;


    }
}
