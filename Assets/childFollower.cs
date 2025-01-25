using System;
using UnityEngine;

public class childFollower : MonoBehaviour
{
    [SerializeField] private Transform _bulle;
    public bool yaxis;
    
    private Vector3 originalPosition;
    
    private void Start()
    {
        originalPosition = transform.position;
    }


    public void ChangeAxisY()
    {
        yaxis = !yaxis;
    }
    private void Update()
    {
        if (yaxis)
        {
            transform.position = new Vector3(_bulle.position.x, _bulle.position.y, _bulle.position.z);
        }
        else
        {
            transform.position = new Vector3(_bulle.position.x, originalPosition.y, _bulle.position.z);
        }
    }
}
