using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphericalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f; // Speed of movement along the sphere
    public float rotationSpeed = 10f; // Speed for smoothly aligning with the movement direction

    [Header("Sphere Settings")]
    public Transform sphereCenter; // Center of the sphere
    public float sphereRadius = 5f; // Radius of the sphere

    private Vector2 movementInput; // Input from the Input System
    private Vector3 currentDirection; // Current movement direction
    [SerializeField] InputAction action;

    private void Start()
    {
        action.Enable();
        action.performed += OnMove;

    }
    private void Update()
    {
        if (movementInput.sqrMagnitude > 0.01f)
        {
            HandleMovement();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get input from the Input System
        Debug.Log("Test");
        movementInput = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        // Convert input to a world direction
        Vector3 inputDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;

        // Align input direction with the object's current position on the sphere
        Vector3 localForward = Vector3.Cross(transform.position - sphereCenter.position, Vector3.up).normalized;
        Vector3 localRight = Vector3.Cross(localForward, transform.position - sphereCenter.position).normalized;
        Vector3 worldDirection = (localRight * inputDirection.x + localForward * inputDirection.z).normalized;

        // Smoothly adjust current direction
        currentDirection = Vector3.Slerp(currentDirection, worldDirection, Time.deltaTime * rotationSpeed);

        // Calculate the new position on the sphere
        Vector3 offset = transform.position - sphereCenter.position;
        Vector3 newPosition = transform.position + currentDirection * (movementSpeed * Time.deltaTime);
        offset = newPosition - sphereCenter.position;

        // Project the position back onto the sphere's surface
        newPosition = sphereCenter.position + offset.normalized * sphereRadius;

        // Update position and rotation
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(currentDirection, transform.position - sphereCenter.position);
    }
}
