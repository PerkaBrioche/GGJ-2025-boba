using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    [SerializeField] List<NetCorner> corners;

    public void Damage(NetCorner corner)
    {
        corners.Remove(corner);
        Destroy(corner.gameObject);
        if(corners.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
