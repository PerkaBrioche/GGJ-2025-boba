using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    [SerializeField] List<NetCorner> corners;
    [SerializeField] Animator animator;

    public void Damage(NetCorner corner)
    {
        corners.Remove(corner);
        Destroy(corner.gameObject);
        if(corners.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
    }
}
