using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* script to setup the camera Rig to always spawn in the wanted location
 * on entering a scene.
 */
public class StartingPosition : MonoBehaviour
{

    public GameObject prefab; // prefab of the camera rig for if the rig doesnt exist in the scene.

    // Start is called before the first frame update
    void Awake()
    {
        // creating MainRig if needed
        if (GameObject.Find("MainRig") == null)
        {
            GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
            obj.name = "MainRig";
        }

        Transform mainRig = null;

        while(mainRig == null)
        {
            Debug.Log("looking for main rig...");
            mainRig = GameObject.Find("MainRig").GetComponent<Transform>();
        }

        // moving camera rig to starting position
        mainRig.position = transform.position;
        mainRig.rotation = transform.rotation;
    }
}
