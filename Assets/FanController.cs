using System;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform _bulle;
    [SerializeField] private float PushForce = 5;


    private void FixedUpdate()
    {
        PushPlayer();
    }

    private void PushPlayer()
    {
        Vector3 direction = _bulle.position - transform.position;
        _bulle.GetComponent<Rigidbody>().AddForce(direction * PushForce, ForceMode.Impulse);
    }
}
