using UnityEngine;

public class rotatecam : MonoBehaviour
{
    public float rotatespeed = 10f;
    private bool started = false;

    void Start()
    {
        started = true;
    }

    void Update()
    {
        if (started)
        {
            // Rotate in the y axis
            transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }
    }
}
