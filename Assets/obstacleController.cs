using System;
using UnityEngine;

public class obstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(PlayerManager.instance.IsInvicible()){return;}
            other.GetComponent<BubleMovement>().GetBounce(transform.position);
        }
    }
}
