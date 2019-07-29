using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouseScript : MonoBehaviour {

    public GameObject pointer_cam; //Pointer camera
	public float maximumLength;
    private Ray rayMouse;
	private Vector3 pos;
	private Vector3 direction;
	private Quaternion rotation;
	public Camera event_cam; //event camera 
	private WaitForSeconds updateTime = new WaitForSeconds (0.01f);

    
  
   	public void StartUpdateRay (){
            StartCoroutine (UpdateRay());
            }
        
	IEnumerator UpdateRay (){
		if (event_cam != null) {
            Ray ray_p = new Ray (pointer_cam.transform.position,pointer_cam.transform.forward);
            RaycastHit hit;
           // Debug.Log("After Ray is made and should be about set the postition");
			rayMouse = ray_p;
			if (Physics.Raycast (rayMouse.origin, rayMouse.direction, out hit, maximumLength)) {
				RotateToMouse (gameObject, hit.point);
			} else {	
				var pos = rayMouse.GetPoint (maximumLength);
				RotateToMouse (gameObject, pos);
			}
			yield return updateTime;
			StartCoroutine (UpdateRay ());
		} else {
			Debug.Log ("Camera not set");
		}
	}
    

    void Update()
    {
        StartUpdateRay();
        this.rotation = pointer_cam.transform.rotation;
    }
    
	void RotateToMouse (GameObject obj, Vector3 destination ) {
		direction = destination - obj.transform.position;
		rotation = Quaternion.LookRotation (direction);
		obj.transform.localRotation = Quaternion.Lerp (obj.transform.rotation, rotation, 60);
	}
    
	public void SetCamera (Camera camera){

        event_cam = camera;
	}

	public Vector3 GetDirection () {
		return direction;
	}

	public Quaternion GetRotation () {
		return pointer_cam.transform.rotation;
	}
}
