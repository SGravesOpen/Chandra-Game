using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class SpawnProjectilesScript : MonoBehaviour {

	public bool cameraShake;
	public Text effectName;
	public RotateToMouseScript rotateToMouse;
	public GameObject firePoint;
	//public GameObject cameras;
	public List<GameObject> VFXs = new List<GameObject> ();
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;
    private bool stateDown;
    public SteamVR_Action_Boolean m_Trigger;
    private int count = 0;
	private float timeToFire = 0f;
	private GameObject effectToSpawn;
	private List<Camera> camerasList = new List<Camera> ();
   // public GameObject Rig; 

	void Start () {
        //print("We start adding cameras");
        /**for (int i = 0; i < cameras.transform.childCount; i++) {
            Debug.Log(message: "Camera "+ cameras.transform.GetChild(i).gameObject.GetComponent<Camera>() + " was added");
            
			camerasList.Add (cameras.transform.GetChild(i).gameObject.GetComponent<Camera>());
		}
        **/
        stateDown = m_ClickAction.GetStateDown(m_TargetSource);
            //OVRInput.Get(OVRInput.Button.One);
        Application.targetFrameRate = 60;
		effectToSpawn = VFXs[0];
		if (effectName != null) effectName.text = effectToSpawn.name;

		//rotateToMouse.SetCamera (camerasList [2]);
		//rotateToMouse.StartUpdateRay (); 
        //Commented to test new rotatetoMouse script
	}


    /// <summary>
    /// setup a variable to store down state
    /// Check if the variable is true
    /// If so then call switchCam 
    /// But remeber to set the variable back to false
    /// </summary>
   

       
	void Update () {
        

        if (m_Trigger.GetStateDown(m_TargetSource) && Time.time >= timeToFire || Input.GetMouseButton (0) && Time.time >= timeToFire) {
			timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
			SpawnVFX ();	
		}

		if (Input.GetKeyDown (KeyCode.D)) Next ();
		if (Input.GetKeyDown (KeyCode.A)) Previous ();
        /**if (stateDown)
        {
            Debug.Log("We got the click for camera change");
            SwitchCamera();
            stateDown = false;
        }
        **/
		if (Input.GetKeyDown (KeyCode.X))
			ZoomIn ();
		if (Input.GetKeyDown (KeyCode.Z))
			ZoomOut ();
	}

	public void SpawnVFX () {
		GameObject vfx;

		/**if (cameraShake && cameras != null)
			cameras.GetComponent<CameraShakeSimpleScript> ().ShakeCamera ();
        **/
		if (firePoint != null) {
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);
			if(rotateToMouse != null){
				vfx.transform.localRotation = rotateToMouse.GetRotation ();
			} 
			else Debug.Log ("No RotateToMouseScript found on firePoint.");
		}
		else
			vfx = Instantiate (effectToSpawn);

		var ps = vfx.GetComponent<ParticleSystem> ();

		if (vfx.transform.childCount > 0) {
			ps = vfx.transform.GetChild (0).GetComponent<ParticleSystem> ();
		}
	}

	public void Next () {
		count++;

		if (count > VFXs.Count)
			count = 0;

		for(int i = 0; i < VFXs.Count; i++){
			if (count == i)	effectToSpawn = VFXs [i];
			if (effectName != null)	effectName.text = effectToSpawn.name;
		}
	}

	public void Previous () {
		count--;

		if (count < 0)
			count = VFXs.Count;

		for (int i = 0; i < VFXs.Count; i++) {
			if (count == i) effectToSpawn = VFXs [i];
			if (effectName != null)	effectName.text = effectToSpawn.name;
		}
	}

	public void CameraShake () {
		cameraShake = !cameraShake;
	}

	public void ZoomIn () {
		for (int i = 0; i < camerasList.Count; i++) {
			camerasList [i].fieldOfView += 5;
		}
	}

	public void ZoomOut () {
		for (int i = 0; i < camerasList.Count; i++) {
			camerasList [i].fieldOfView -= 5;
		}
	}

    //Store cameras positions and set the rigs transformation to the correct tranform when button pressed
    /**public void SwitchCamera()
    {
        for(int i = 0; i < camerasList.Count; i++)
        {
            if (camerasList[i].gameObject.activeSelf)
            {
                int next = i + 1;
                if(next == camerasList.Count)
                {
                    next = 0;
                }
                camerasList[i].gameObject.SetActive(false);
                camerasList[next].gameObject.SetActive(true);
            }
        }
        
        //Debug.Log("Camera: " + currCam.name + " has the postion: " + currCam.transform.position);
        //Debug.Log("Main Camera: " + mainCamera.name + " has the position: " + mainCamera.transform.position);

    }
    **/
}

/**
  print("random call");
        for (int i = 0; i < camerasList.Count; i++) {
            if (camerasList[i].gameObject.activeSelf)
            {
                camerasList[i].gameObject.SetActive(false);

                if ((i + 1) == camerasList.Count)
                {
                    camerasList[0].gameObject.SetActive(true);
                    // mainCamera.transform.position = camerasList[0].transform.position;
                    //rotateToMouse.SetCamera(camerasList [0]);
                    Debug.Log("Switch to first position of camera at spot [0]");
                    break;

                }
                break;
            }
            else
            {
                camerasList[i + 1].gameObject.SetActive(true);
                //rotateToMouse.SetCamera(camerasList[i + 1]);
                //mainCamera.transform.position = camerasList[i].transform.position;
                break;

            }
            
            
  **/
