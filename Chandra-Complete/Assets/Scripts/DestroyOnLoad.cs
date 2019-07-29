using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneUnloaded += DestroyOnSceneChange;
    }

    void DestroyOnSceneChange(Scene current)
    {
        Destroy(GameObject.Find("MainRig"));
    }
}
