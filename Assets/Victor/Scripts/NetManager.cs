using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    [SerializeField] List<NetCorner> corners;
    [SerializeField] Animator animator;
    [SerializeField] Vector3 from;
    [SerializeField] Vector3 to;
    [SerializeField] float timeToGo = 5f;

    private void Start()
    {
        StartCoroutine(Animation());
    }

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

    IEnumerator Animation()
    {
        float t = 0f;
        Vector3 m_from = transform.position - from;
        Vector3 m_to = transform.position + to;
        while (t < timeToGo)
        {
            yield return null;
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(m_from, m_to, t / timeToGo);
        }
        transform.position = Vector3.Lerp(m_from, m_to, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position - from, 0.1f);
        Gizmos.DrawSphere(transform.position + to, 0.1f);
    }
}
