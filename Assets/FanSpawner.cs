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
    [SerializeField] private Transform boueTransform;
    

    
    private int spawnChanceMax = 10;
    private int spawnChance = 10;
    private bool _isCoolD;
    private bool _canSpawn;

    private void Start()
    {
        _canSpawn = true;
    }

    private void SpawnFan()
    {
        _canSpawn = false;
        var fan = Instantiate(_fanPrefab, transform.position, Quaternion.identity, boueTransform);
        fan.GetComponent<ButtonManager>().ListOfButtonsPosition = _Butositions;
        fan.GetComponent<FanController>()._bulle = _bulle;
        fan.GetComponent<FanController>().FanSpawner = this;
    }

    public void SetCanSpawn()
    {
        _canSpawn = true;
        _isCoolD = false;
    }
    
    private void Update()
    {
        if(!_canSpawn)
            return;
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
