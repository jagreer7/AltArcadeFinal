using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NewSceneMover : MonoBehaviour
{
    StirControls controls;
    private bool UpBool;
    private bool DownBool;
    private bool LeftBool;
    private bool RightBool;

    private bool UpBoolChecker = true;
    private bool DownBoolChecker = true;
    private bool LeftBoolChecker = true;
    private bool RightBoolChecker = true;

    [SerializeField] private string newScene;

    [SerializeField] private int stirCount;
    [SerializeField] private int stirLimit = 8;

    private void Awake()
    {
        controls = new StirControls();

        controls.Stir.Up.performed += ctx => UpBool = true;
        controls.Stir.Up.canceled += ctx => UpBool = false;

        controls.Stir.Down.performed += ctx => DownBool = true;
        controls.Stir.Down.canceled += ctx => DownBool = false;

        controls.Stir.Left.performed += ctx => LeftBool = true;
        controls.Stir.Left.canceled += ctx => LeftBool = false;

        controls.Stir.Right.performed += ctx => RightBool = true;
        controls.Stir.Right.canceled += ctx => RightBool = false;

        controls.Stir.Enable();
    }
    void Update()
    {
        if (UpBool == true)
        {
            Debug.Log("up");
            if (UpBoolChecker == true)
            {
                Debug.Log("upcheck");
                UpBoolChecker = false;
                DownBoolChecker = true;
                stirCount++;
            }
        }
        if (DownBool == true)
        {
            Debug.Log("down");
            if (DownBoolChecker == true)
            {
                Debug.Log("downcheck");
                UpBoolChecker = true;
                DownBoolChecker = false;
                stirCount++;
            }
        }
        if (LeftBool == true)
        {
            Debug.Log("left");
            if (LeftBoolChecker == true)
            {
                LeftBoolChecker = false;
                RightBoolChecker = true;
                stirCount++;
            }
        }
        if (RightBool == true)
        {
            Debug.Log("right");
            if (RightBoolChecker == true)
            {
                RightBoolChecker = false;
                LeftBoolChecker = true;
                stirCount++;
            }
        }
        if (stirCount == stirLimit)
        {
            Debug.Log("Stirred");
            SceneManager.LoadScene(newScene);
        }
    }
}
