using UnityEngine;

public class ButtonController : MonoBehaviour, IDamageable
{
    public ButtonManager ButtonManager;
    public void Damage()
    {
        ButtonManager.buttonClicked++;
        Destroy(gameObject);
    }
}