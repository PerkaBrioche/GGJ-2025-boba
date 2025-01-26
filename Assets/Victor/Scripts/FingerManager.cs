using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerManager : MonoBehaviour
{
    [SerializeField] Transform model;
    [SerializeField] float backAmount = 20f;
    [SerializeField] float timeToGo = 5f;
    Vector3 playerPos;
    private void Start()
    {
        playerPos = transform.position;
        model.localEulerAngles = Vector3.up * Random.Range(0f, 360f);
        model.position = Vector3.back * backAmount;
        StartCoroutine(Animation());
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

        model.position = Vector3.back * backAmount;
        while (t < timeToGo)
        {
            yield return null;
            t += Time.deltaTime;
            model.position = Vector3.forward * backAmount * (t / timeToGo);
        }

        t = 0f;
        while (t < timeToGo / 4)
        {
            yield return null;
            t += Time.deltaTime;
            model.position = Vector3.forward * (backAmount - (backAmount * (t / timeToGo / 4)));
        }
        Destroy(gameObject);
    }
}
