using UnityEngine;
using UnityEngine.InputSystem;

public class OnSphereInput : MonoBehaviour
{
    [SerializeField] InputAction directionalActions;
    [SerializeField] Transform sphere;
    Vector3 testPos;
    Vector3 testPos2;
    [SerializeField] float angleX = 180;
    [SerializeField] float angleZ = 180;
    [SerializeField] float speedX = 5;
    [SerializeField] float speedZ = 5;

    private void Awake()
    {
        directionalActions.Enable();
    }

    private void Update()
    {
        // Handle input to move across the sphere
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        // Update angles based on input
        angleX += horizontal * speedX * Time.deltaTime; // Adjust longitude
        angleZ -= vertical * speedZ * Time.deltaTime;   // Adjust latitude

        // Clamp zAngle to prevent flipping at poles
        angleZ = Mathf.Clamp(angleZ, 0.01f, 179.99f);

        // Convert angles to radians
        float xRad = Mathf.Deg2Rad * angleX;
        float zRad = Mathf.Deg2Rad * angleZ;

        // Calculate the point on the sphere
        Vector3 point = new Vector3
        {
            x = (transform.localScale.x / 2f) * Mathf.Sin(zRad) * Mathf.Cos(xRad),
            y = (transform.localScale.x / 2f) * Mathf.Cos(zRad), // Height on the sphere
            z = (transform.localScale.x / 2f) * Mathf.Sin(zRad) * Mathf.Sin(xRad)
        };

        // Visualize the point (optional)
        Debug.DrawLine(Vector3.zero, point, Color.red);

        testPos2 = transform.position + point;


        //if (directionalActions.IsPressed())
        //{
        //    Vector2 dir = directionalActions.ReadValue<Vector2>().normalized;
        //    dir.x *= speedX;
        //    dir.y *= speedZ;


        //    if (angleX > 0 && angleX < 180)
        //    {
        //        angleZ += dir.y;
        //    }
        //    else
        //    {
        //        angleZ -= dir.y;
        //    }
        //    angleX += dir.x;

        //    if(angleX < 0)
        //    {
        //        angleX += 360;
        //    }
        //    if (angleX > 360)
        //    {
        //        angleX -= 360;
        //    }
            
        //}

        //testPos = new Vector3
        //{
        //    x = 2.5f * Mathf.Sin(angleZ * Mathf.Deg2Rad) * Mathf.Cos(angleX * Mathf.Deg2Rad),
        //    y = 2.5f * Mathf.Cos(angleZ * Mathf.Deg2Rad),
        //    z = 2.5f * Mathf.Sin(angleZ * Mathf.Deg2Rad) * Mathf.Sin(angleX * Mathf.Deg2Rad)
        //};
        //testPos2 = transform.position + testPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(testPos2, 0.5f);
    }
}


