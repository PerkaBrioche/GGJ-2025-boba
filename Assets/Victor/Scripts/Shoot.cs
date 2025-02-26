using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private LayerMask layer;
    
    [SerializeField] InputAction mouseCord;
    [SerializeField] InputAction mouseClick;
    Transform crosshair;
    [SerializeField] GameObject crosshairPrefab;
    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Transform player;
    [SerializeField] float maxDistance = 4f;
    [SerializeField] float radius = 1.5f;
    List<Vector3> pos = new List<Vector3>();

    private void Awake()
    {
        crosshair = Instantiate(crosshairPrefab).transform;
        EnableInputs();
        mouseClick.performed += OnMouseClick;
        mouseClick.canceled += OnMouseStopClicking;
    }

    private void OnMouseStopClicking(InputAction.CallbackContext context)
    {
        if(waterParticle.isPlaying)
        {
            waterParticle.Stop();
        }
    }

    private void Start()
    {
        GetComponentInChildren<particleAttractorLinear>().target = crosshair;
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        Collider[] colliders = Physics.OverlapSphere(crosshair.transform.position, radius);
        if (colliders.Length > 0)
        {
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage();
                }
            }
        }
        waterParticle.Play();
        SoundManagerScript.instance.ActivateSound(8, "OneShot");
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,1000f ,layer))
        {
            if(Vector2.Distance(new Vector2(player.position.x,player.position.z), new Vector2(hit.point.x,hit.point.z)) > maxDistance)
            {
                Vector2 dir = (new Vector2(hit.point.x, hit.point.z)) - (new Vector2(player.position.x, player.position.z));
                dir = dir.normalized * maxDistance;
                crosshair.transform.position = new Vector3(player.position.x + dir.x, hit.point.y + 0.01f, player.position.z + dir.y);
            }
            else
            {
                crosshair.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
            }
            
        }
        
        Vector3 targetDirection = crosshair.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        
    }

    void EnableInputs()
    {
        mouseCord.Enable();
        mouseClick.Enable();
    }

    void DisableInputs()
    {
        mouseCord.Disable();
        mouseClick.Disable();
    }

    private void OnDrawGizmos()
    {
        foreach (var item in pos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(item, 0.1f);
        }
    }
}
