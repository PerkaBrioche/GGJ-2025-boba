using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerManager : MonoBehaviour
{
    [SerializeField] List<NetCorner> corners;
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float timeToGo = 5f;

    private void Start()
    {
        transform.localEulerAngles = Vector3.up * Random.Range(0, 360);
        StartCoroutine(Animation());
    }

    public void Damage(NetCorner corner)
    {
        corners.Remove(corner);
        Destroy(corner.gameObject);
        if (corners.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
    }

    IEnumerator Animation()
    {
        float t = 0f;
        Vector3 m_from = transform.forward * from;
        Vector3 m_to = (-transform.forward) * to;
        transform.LookAt(m_to, Vector3.up);
        while (t < timeToGo)
        {
            yield return null;
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(m_from, m_to, t / timeToGo);
        }
        transform.position = Vector3.Lerp(m_from, m_to, 1f);
        Destroy(gameObject);
    }
}
