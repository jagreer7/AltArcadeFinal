using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string KeyCode;
    [SerializeField] private GameObject Action;
    [SerializeField] private string ActionText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string GetKeyCode()
    {
        return KeyCode;
    }
    public string GetActionText()
    {
        return ActionText;
    }
    public GameObject GetAction()
    {
        return Action;
    }
}
