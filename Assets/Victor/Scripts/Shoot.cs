using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{
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
        mouseClick.canceled += OnMouseTopClicking;
    }

    private void OnMouseTopClicking(InputAction.CallbackContext context)
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
                if (hit.GetComponent<Collider>().gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.Damage();
                }
            }
        }
        waterParticle.Play();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mouseCord.ReadValue<Vector2>()), out hit))
        {

            if(Vector2.Distance(new Vector2(player.position.x,player.position.z), new Vector2(hit.point.x,hit.point.z)) > maxDistance)
            {
                Vector3 dir = hit.point - player.position;
                dir = dir.normalized * maxDistance;
                crosshair.transform.position = new Vector3(player.position.x + dir.x, crosshair.transform.position.y, player.position.z + dir.z);
            }
            else
            {
                crosshair.transform.position = new Vector3(hit.point.x, crosshair.transform.position.y, hit.point.z);
            }
            
        }
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
