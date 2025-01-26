using System;
using UnityEngine;

public class obstacleController : MonoBehaviour
{
    public bool oneTimeTouch = false;
    
    
    private bool alreadyHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(alreadyHit){return;}
            
            if (oneTimeTouch)
            {
                alreadyHit = true;
            }
            other.GetComponent<BubleMovement>().GetBounce(transform.position);
        }
    }
}
