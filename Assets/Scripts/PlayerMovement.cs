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
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void Update()
    {

        ApplyMovement();
        ApplyGravity();
        SetHologramDirection();

    }

    private void SetHologramDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipDirection(-Vector3.right);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipDirection(Vector3.right);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            FlipDirection(Vector3.up);
        }


    }

    private void FlipDirection(Vector3 newDirection)
    {
        Quaternion rotationDifference = Quaternion.FromToRotation(newDirection, Vector3.up);
        transform.rotation = rotationDifference * transform.rotation;
        //transform.Rotate((rotationDifference * transform.rotation).ToEulerAngles(), 1f, Space.World);
        //movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void GetMovementDirection()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        movementDirection.y = verticalVelocity;
        if (transform.localRotation.eulerAngles.z == 90f)
        {
            movementDirection = new Vector3(0, moveInput.x, moveInput.y);
            movementDirection.x = -verticalVelocity;
        }
        else if(transform.localRotation.eulerAngles.z == 180f)
        {
            movementDirection = new Vector3(-moveInput.x, 0, moveInput.y);
            movementDirection.y = verticalVelocity;
        }
        else if (transform.localRotation.eulerAngles.z == 270f)
        {
            movementDirection = new Vector3(0, -moveInput.x, moveInput.y);
            movementDirection.x = verticalVelocity;
        }

        if (transform.localRotation.eulerAngles.x == 90f)
        {
            movementDirection = new Vector3(moveInput.x,moveInput.y, 0);
        }
        else if (transform.localRotation.eulerAngles.x == 180f)
        {
            movementDirection = new Vector3(-moveInput.x, 0, moveInput.y);
        }
        else if (transform.localRotation.eulerAngles.x == 270f)
        {
            movementDirection = new Vector3(-moveInput.x, 0, moveInput.y);
        }


    }



    private void ApplyMovement()
    {
        GetMovementDirection();
        //movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
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
