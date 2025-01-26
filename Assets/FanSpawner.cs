using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _Butositions;
    [SerializeField] private GameObject _fanPrefab;
    [SerializeField] private Transform _bulle;


    private void Start()
    {
        SpawnFan();
    }

    private void SpawnFan()
    {
        var fan = Instantiate(_fanPrefab, _Butositions[Random.Range(0, _Butositions.Count)].position, Quaternion.identity);
        fan.GetComponent<ButtonManager>().ListOfButtonsPosition = _Butositions;
        fan.GetComponent<FanController>()._bulle = _bulle;
    }
} 
