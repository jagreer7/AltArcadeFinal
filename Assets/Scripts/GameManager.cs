using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        //Just for shutting down the game
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
}
