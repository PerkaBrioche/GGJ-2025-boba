using UnityEngine;

public class NetCorner : MonoBehaviour, IDamageable
{
    [SerializeField] NetManager netManager;
    [SerializeField] Transform model;

    public void Damage()
    {
        netManager.Damage(this);
        if(model != null)
        {
            Destroy(model.gameObject);
        }
    }
}
