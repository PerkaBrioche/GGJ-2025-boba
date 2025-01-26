using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] public List<Transform> ListOfButtonsPosition;
    private List<GameObject> ButtonSpawned;

    [SerializeField] private FanController FanController;

    public int buttonClicked;
    
    private void Start()
    {
        ButtonSpawned = new List<GameObject>();
        SpawnButton();
    }
    
    private void SpawnButton()
    {
        for (int i = 0; i < ListOfButtonsPosition.Count; i++)
        {
            print(ListOfButtonsPosition.Count);
            var button = Instantiate(ButtonPrefab, ListOfButtonsPosition[i].position, Quaternion.identity,ListOfButtonsPosition[i] );
            ButtonSpawned.Add(button);
            button.GetComponent<ButtonController>().ButtonManager = this;
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
        if(buttonClicked == ListOfButtonsPosition.Count)
        {
            return true;
        }
        return false;
    }
}
