using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class OrbitMenu : MonoBehaviour
{
    public string loadingPath; //Inspector field for name of level

    public void LoadLevel()
    { 
        SteamVR_LoadLevel.Begin(loadingPath);
    }
    public void QuitGame()
    {
        Debug.Log("yer");
        Application.Quit();
    }


}
