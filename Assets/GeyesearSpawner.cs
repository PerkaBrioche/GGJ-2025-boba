using System;
using System.Collections;
using UnityEngine;

public class GeyesearSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _PREgeyesearPrefab;
    [SerializeField] private Transform _geyesearPrefab;
    private void Start()
    {
        var warning = Instantiate(_PREgeyesearPrefab, _geyesearPrefab.position, Quaternion.identity);
        Destroy(warning, 3);
        StartCoroutine(SpawnGeyesear());
    }
    
    private IEnumerator SpawnGeyesear()
    {
        yield return new WaitForSeconds(3);
        var g = Instantiate(_geyesearPrefab, _geyesearPrefab.position, Quaternion.identity);
        Destroy(g.gameObject, 3.5f);
    }
}
