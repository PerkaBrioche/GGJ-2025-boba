using UnityEngine;

public class DamageShoot: MonoBehaviour
{
    [SerializeField] float radius = 5;
    public void Shoot()
    {
        RaycastHit[] hits =  Physics.SphereCastAll(transform.position, radius, Vector3.zero);
        if(hits.Length > 0 )
        {
            foreach(RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.Damage();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
