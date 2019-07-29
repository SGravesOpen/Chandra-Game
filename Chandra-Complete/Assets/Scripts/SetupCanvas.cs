using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
/* a script used to set up the game canvas to be interactable with 
 * VR pointers, by adding buttons to them.
 * used on the main canvas in the Pixel CasA scene.
 */
public class SetupCanvas : MonoBehaviour
{
    // the canvas to set up.
    public GameObject uiCanvas;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;


    // Start is called before the first frame update
    // on start, making the needed parts of the canvas interactable.
    // may need changing if the updated scene from Elaine has renamed heirarchy,'
    // this was hardcoded.
    void Start()
    {
        //Debug.Log("We started");
        // going through each child in the canvas, and if the childs name is one that we need
        // to make interactable, we use the appropriate function.
        foreach (RectTransform child in uiCanvas.GetComponentsInChildren<RectTransform>())
        {
            if (child.name == "ColorSquares")              // if the squares to be colored in
                HandleColorSquares(child);
            else if (child.name == "Legend")               // if the squares are the legend
                HandleLegend(child);
           // else if(go.name == "ColorSquare")  // if the square is the current color square
            //    handleCCS(go);
        }
    }

    // funtion for the main squares to be colored, 
    // go is the transform of the parent object holding all 
    // of the squares.
    public void HandleColorSquares(RectTransform parent)
    {
        Debug.Log("Setting up the Color Squares!");
       // Debug.Log("The go: " + parent.name);
       // Debug.Log("The list of children: ");
        //int i = 0;

        // going through each child square, getting its colorSquare component,
        // which holds a function to handle being clicked on, and set a on click 
        // listener for the function in the colorSquare component.
        foreach(Button child in parent.GetComponentsInChildren<Button>())
        {
           
            var cs = child.GetComponent<ColorSquare>();

            ButtonTransitioner bs = child.gameObject.AddComponent(typeof(ButtonTransitioner)) as ButtonTransitioner;
            bs.enableChangeColor = false;
            
            if (child != null)
                child.onClick.AddListener(() => cs.OnMouseDown());
         
        }
    }

    // same logic for the handleColorSquares() logic, refer to that function for explanation.
    public void HandleLegend(Transform go)
    {
        Debug.Log("Setting up the legend!");
        foreach (Button child in go.GetComponentsInChildren<Button>())
        {
            //Debug.Log(child.name);
            var cs = child.GetComponent<ColorSquare>();

            ButtonTransitioner bs = child.gameObject.AddComponent(typeof(ButtonTransitioner)) as ButtonTransitioner;
            bs.enableChangeColor = false;

            if (child != null)
                child.onClick.AddListener(() => cs.OnMouseDown());
        }
    }

    // adding the button listener for the current color square.
    /**
    public void handleCCS(Transform go)
    {
        Debug.Log("Setting up the CSS!");
        Button child = go.GetComponentInChildren<Button>();
        Debug.Log(child.name);
        var cs = child.GetComponent<ColorSquare>();

        ButtonTransitioner bs = child.gameObject.AddComponent(typeof(ButtonTransitioner)) as ButtonTransitioner;
        bs.enableChangeColor = false;

        if (child != null)
            child.onClick.AddListener(() => cs.OnMouseDown());
    }

    **/


}
