using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

/* a component to disable the pointer on the right hand, where
 * it is only active on the pixel Cas A scene where it is needed.
 */
public class ActivePointer : MonoBehaviour
{
    
    private OculusInput oi;                 // the Oculus Input scripted component
    private Transform pointer;              // the transform of the pointer, to turn the gameobject on and off
    private bool needPixelACanvas = false;  // boolean to tell whether the canvas needs to be shown.
  
    // Start is called before the first frame update
    // find all of the objects in the scene.
    void Start()
    {
        // finding the objects and disabling them.

        oi = gameObject.GetComponent<OculusInput>();
        pointer = transform.Find("PR_Pointer");
        if (SceneManager.GetActiveScene().name == "Pixel Cas A")
        {
            pointer.gameObject.SetActive(false);
            oi.enabled = true;
        }
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    // function to control when a scene changes, the logic for turning on the pointer if needed.
    public void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log("scene changed");
        string currentName = current.name;
        string nextName = next.name;


        if(nextName == "Pixel Cas A")   // if pixel Scene, turn on pointer
        {
            Debug.Log("in pixel scene");
            pointer.gameObject.SetActive(true);
            oi.enabled = true;
            needPixelACanvas = true;
            setCanvas();
        } else                          // turn the pointer off
        {
            pointer.gameObject.SetActive(false);
            oi.enabled = false;
            needPixelACanvas = false;
        }
    }

    // a helper to handle logic of setting the canvas for the oculus input script.
    // refer to the OculusInput.cs script to see what the canvas is for.
    private void setCanvas()
    {
        GameObject c = null;
        while(c == null)
        {
            c = GameObject.Find("Canvas");
        }

        if(needPixelACanvas)
        {
            oi.canvas = c;
            needPixelACanvas =  false;

        }


    }




}
