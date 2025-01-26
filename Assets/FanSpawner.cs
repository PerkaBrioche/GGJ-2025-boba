using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _Butositions;
    [SerializeField] private GameObject _fanPrefab;
    [SerializeField] private Transform _bulle;

    
    private int spawnChanceMax = 10;
    private int spawnChance = 10;
    private bool _isCoolD;

    private void Start()
    {
        SpawnFan();
    }

    private void SpawnFan()
    {
        var fan = Instantiate(_fanPrefab, _Butositions[Random.Range(0, _Butositions.Count)].position, Quaternion.identity);
        fan.GetComponent<ButtonManager>().ListOfButtonsPosition = _Butositions;
        fan.GetComponent<FanController>()._bulle = _bulle;
        fan.GetComponent<FanController>().FanSpawner = this;
    }

    public void SetCanSpawn()
    {
        _isCoolD = false;
    }
    
    private void Update()
    {

        if (!_isCoolD)
        {
            _isCoolD = true;
            if (Random.Range(0, spawnChance) == 0)
            {
                SpawnFan();
                spawnChance = spawnChanceMax;
            }
            else
            {
                StartCoroutine(RetrySpawn());
                spawnChance --;
            }
        }

        
    }

    private IEnumerator RetrySpawn()
    {
        yield return new WaitForSeconds(2);
        _isCoolD = false;

    }
} 
