using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] public List<Transform> ListOfButtonsPosition;
    private List<GameObject> ButtonSpawned;

    [SerializeField] private FanController FanController;
    
    private void Start()
    {
        SpawnButton();
    }
    
    private void SpawnButton()
    {
        foreach (var buttonPosition in ListOfButtonsPosition)
        {
            var but = Instantiate(ButtonPrefab, buttonPosition.position, buttonPosition.rotation, buttonPosition);
            
            ButtonSpawned.Add(but);
        }
    }

    private void Update()
    {
        if (IsEnded())
        {
            FanController.End();
        }

    }

    private bool IsEnded()
    {
        bool isEnded = true;
        foreach (var but in ButtonSpawned)
        {
            if(but != null)
            {
                isEnded = false;
            }
        }
        return isEnded;
    }
}
