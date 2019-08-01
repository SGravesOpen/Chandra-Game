using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//NOT APART OF OVR IMPORT
public class OVRInputMod : BaseInputModule
{

    public Camera m_Camera;
    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;
    private EventSystem es;

    // Start is called before the first frame update
    protected void Awake()
    {
        base.Awake();
        es = gameObject.GetComponent<EventSystem>();
        m_Data = new PointerEventData(es);
    }

    public PointerEventData GetData()
    {
       //Debug.Log("We got the data from the input module");
        return m_Data;
    }
    
    public override void Process()
    {   
        //Resey data, set Camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);
        
        //raycast 
        es.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;


        //clear
        m_RaycastResultCache.Clear();

        //Hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);
        //press
        if(OVRInput.GetDown(OVRInput.Button.Three))
        {
            ProcessPress(m_Data);
        }

        //release
        if(OVRInput.GetUp(OVRInput.Button.Three))
        {
            ProcessRelease(m_Data);
        }
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

    // Update is called once per frame
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
