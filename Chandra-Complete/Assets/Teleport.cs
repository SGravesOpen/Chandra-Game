using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MonoBehaviour
{

    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        //pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);
        //teleport
        if (m_TeleportAction.GetStateUp(m_Pose.inputSource))
        {
            TryTeleport();           
        }
    }

    //
    private void TryTeleport()
    {
        //Check for valid position, and if already teleporting
        if (!m_HasPosition || m_IsTeleporting)
            return;
        //get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        //figure out translation

        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;
        //move
        StartCoroutine(MoveRig(cameraRig, translateVector));
         
    }

    //Co routine for fading and moving the camera rig
    //Input: transform of the rig, vector3 of translation we want
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        //Flag
        m_IsTeleporting = true;

        //Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        //apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        //Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);
        //De-flag
        m_IsTeleporting = false;
    }

    //positions the pointer in the scene and pointer 
    private bool UpdatePointer()
    {
        // Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //If its a hit
        if (Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }    
        //if not a hit

        return false;
    }
}

