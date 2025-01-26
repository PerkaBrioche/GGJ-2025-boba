using UnityEngine;

public class ButtonController : MonoBehaviour, IDamageable
{
    public void Damage()
    {
        Destroy(gameObject);
    }
}