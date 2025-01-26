using System;
using System.Collections;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] public Transform _bulle;
    [SerializeField] private float PushForce = 5;
    [SerializeField] public FanSpawner FanSpawner;


    private void Start()
    {
        ChangeMode();
    }

    private void FixedUpdate()
    {
        PushPlayer();
        transform.LookAt(_bulle, Vector3.up);
    }

    private void PushPlayer()
    {
        Vector3 direction = _bulle.position - transform.position;
        Debug.DrawLine(transform.position, direction, Color.red, 5f);
        _bulle.GetComponent<Rigidbody>().AddForce(direction * PushForce, ForceMode.Impulse);
    }

    private void ChangeMode()
    {
        PushForce = -PushForce;
        StartCoroutine(changeModeCoroutine());
    }
    
    private IEnumerator changeModeCoroutine()
    {
        yield return new WaitForSeconds(5);
        ChangeMode();
    }

    public void End()
    {
        FanSpawner.SetCanSpawn();
        StartCoroutine(LerpPosition());
    }
    
    private IEnumerator LerpPosition()
    {
        float alpha = 0;
        Vector3 myPos = transform.position;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            // GO DOWN
            transform.position = Vector3.Lerp(myPos, myPos + Vector3.down * 8, alpha);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
