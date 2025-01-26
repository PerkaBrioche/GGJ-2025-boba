using System;
using System.Collections;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform pushParticule;
    [SerializeField] private Transform AttractionParticule;
    [SerializeField] public Transform _bulle;
    [SerializeField] private float PushForce = 5;
    [SerializeField] public FanSpawner FanSpawner;
    [SerializeField] public float rotationSpeed = 10;
    
    private bool isPusing = false;


    private void Start()
    {
        ChangeMode();
    }

    private void FixedUpdate()
    {
        PushPlayer();
        Vector3 targetDirection = _bulle.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void PushPlayer()
    {
        Vector3 direction = _bulle.position - transform.position;
        Debug.DrawLine(transform.position, direction, Color.red, 5f);
        _bulle.GetComponent<Rigidbody>().AddForce(direction * PushForce, ForceMode.Impulse);
    }

    private void ChangeMode()
    {
        isPusing = !isPusing;
        pushParticule.gameObject.SetActive(isPusing);
        AttractionParticule.gameObject.SetActive(!isPusing);
        if (isPusing)
        {
            PushForce = PushForce * 1;
        }
        else
        {
            PushForce = PushForce * -1;
        }
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
        Destroy(gameObject);
     //   StartCoroutine(LerpPosition());
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
