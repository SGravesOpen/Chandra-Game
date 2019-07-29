using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A script to find the camera used to track rays from a pointer,
 * and set that camera/pointer/ray to be the event activator for the canvas.
 * Should be more appropriately named to FindEventCamera, but its fine.
 * This is used on the main menu canvas, as it doesnt know
 * where/which event camera to use on the start of the scene. 
 */
public class FindUIInputModule : MonoBehaviour
{
    private Canvas canvas;
    private GameObject leftHand;
    private bool alreadySet = false;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        leftHand = GameObject.Find("Controller (Left)");

    }

    // Update is called once per frame
    void Update()
    {
        // checking to make sure right hand exists
        if (leftHand == null)
        {
            leftHand = GameObject.Find("Controller (Right)");
            return;
        }
        else
        {
            setCamera();
        }

    }

    // a function that sets the event camera if it has not already been done.
    private void setCamera()
    {
        if (!alreadySet)
        {
            Camera eventCamera = leftHand.GetComponentInChildren<Camera>();
            canvas.worldCamera = eventCamera;
            alreadySet = true;
        }
    }
}
