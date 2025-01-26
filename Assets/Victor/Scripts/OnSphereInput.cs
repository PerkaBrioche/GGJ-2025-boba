using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnSphereInput : MonoBehaviour
{
    [SerializeField] InputAction directionalActions;
    [SerializeField] Transform sphere;
    [SerializeField] Transform directionTransform;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] Vector3 inputDirection;

    private void Awake()
    {
        directionalActions.Enable();
    }

    private void Update()
    {
        inputDirection = directionalActions.ReadValue<Vector2>().normalized;

        MoveAndProjectOnSphere();
    }

    void MoveAndProjectOnSphere()
    {
        Vector3 direction = sphere.position - directionTransform.position;

        direction.Normalize();
        // If there's a valid direction, calculate rotation
        if (direction != Vector3.zero)
        {
            // Determine the target rotation
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);

            // Smoothly rotate toward the target rotation
            directionTransform.rotation = targetRotation;
            Vector3 newDirection = sphere.position - directionTransform.position;
            Vector3 dir = (targetRotation * Vector3.forward * -inputDirection.y);
            Vector3 dir2 = (targetRotation * Vector3.right * inputDirection.x);
            Vector3 finalDir = (dir + dir2) * moveSpeed * Time.deltaTime;
            Vector3 positionOnSphere = directionTransform.position + finalDir;
            //Vector3 finalPos = sphere.position + (positionOnSphere - sphere.position) * 2.5f;

            directionTransform.position = positionOnSphere;
            transform.position = directionTransform.position + Vector3.up;
        }
    }

}


