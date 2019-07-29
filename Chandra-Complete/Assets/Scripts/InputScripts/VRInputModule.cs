using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

/* refer to youtube video for deeper understanding / better explanation.
 * used for pointer interaction
 */ 
public class VRInputModule : BaseInputModule
{

    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;
    private EventSystem es;

    protected override void Awake()
    {
       // Debug.Log("VRInput Woke up");
        base.Awake();
        es = gameObject.GetComponent<EventSystem>();
        m_Data = new PointerEventData(es);
    }

    public override void Process()
    {
        // Reset data, set Camera

        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);
        
        // raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // Clear
        m_RaycastResultCache.Clear();

        // Hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);
        // Press
        //Debug.Log("In the process before we check buttons state: " + OVRInput.GetDown(OVRInput.RawButton.X));
        //Debug.Log("In the process before we check buttons state: " + OVRInput.GetDown(OVRInput.Button.One));
        if (m_ClickAction.GetStateDown(m_TargetSource))
        {
         
            ProcessPress(m_Data);
        }
        // Release
        if(m_ClickAction.GetStateDown(m_TargetSource))
        {
           // Debug.Log("Button state up: " + OVRInput.GetUp(OVRInput.Button.Two));
            ProcessRelease(m_Data);
        }
    }

    public PointerEventData GetData()
    {
       //Debug.Log("We got the data from the input module");
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        // Set RayCast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get the down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);
       // Debug.Log("Hello we got pressed");
        // if no down handler, try get click handler
        if(newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        // execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual 
        if(data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear the selected game object
        eventSystem.SetSelectedGameObject(null);

        // Reset Data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }


}
