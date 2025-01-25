using UnityEngine;

public class NetCorner : MonoBehaviour, IDamageable
{
    [SerializeField] NetManager netManager;

    public void Damage()
    {
        netManager.Damage(this);
    }
}
