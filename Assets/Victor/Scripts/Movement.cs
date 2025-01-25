using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction action;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = action.ReadValue<Vector2>().normalized * speed;
        rb.linearVelocity = new Vector3(input.x, rb.linearVelocity.y,input.y);
    }
}
