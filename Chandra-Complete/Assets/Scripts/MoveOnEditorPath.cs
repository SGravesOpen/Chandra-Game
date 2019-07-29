using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOnEditorPath : MonoBehaviour {
    public EditorPath PathToFollow;

    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
    //public GameObject target;
   // private Transform targetTransform;
    private float timer = 0.0f;
    public string nextScene;
    public bool image;

    Vector3 last_position;
    Vector3 current_position;

	// Use this for initialization
	void Start () {
        print("Starting up: "+ this.name );
        last_position = transform.position;
       // targetTransform = target.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (image)
        {
            timer += Time.deltaTime;
            if (timer >= 9f)
            {
                if (CurrentWayPointID < PathToFollow.path_objs.Count)
                {
                    print("Keep moving " +this.name);
                    float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);
                    print("Distance: " + distance + " Reach Distance: " + reachDistance + " for "+ this.name);

                    if (distance <= reachDistance)
                    {
                        print("Current Way Point ID(before): " + CurrentWayPointID);
                        CurrentWayPointID++;
                        print("Current Way Point ID(after): " + CurrentWayPointID);
                    }

                }
                else
                {
                    GameObject img = GameObject.Find("CasA");
                    img.SetActive(false);
                }

            }

            //if (CurrentWayPointID >= PathToFollow.path_objs.Count)
            //{
            //    SceneManager.LoadScene(nextScene);
            //}
        }
        else {
            if (CurrentWayPointID < PathToFollow.path_objs.Count)
            {
                if (CurrentWayPointID == 3)
                {
                    timer += Time.deltaTime;
                    if (timer < 2f)
                    {
                        return;
                    }
                }
               
                print("Time: " + Time.deltaTime + " Speed: "+ speed + " --- " + transform.position);
                float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
                transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);
                print("No flag!!!" + this.name + " Path object position: " + PathToFollow.path_objs[CurrentWayPointID].position + " Current transform: " + transform.position);

                if (distance <= reachDistance)
                {
                    CurrentWayPointID++;
                    print("Current Way Point ID: "+ CurrentWayPointID);
                }
                print("Current Way Point ID***: " + CurrentWayPointID + " for: " + this.name);
                print("Distance: " + distance + " reach distance: " + reachDistance + " for " + this.name);
                print("Number of paths: " + PathToFollow.path_objs.Count);

                if (nextScene != "" && CurrentWayPointID >= PathToFollow.path_objs.Count)
                {
                    print("We should be moving to the next scene");
                    SceneManager.LoadScene(nextScene);
                }
            }
        }

	}

    //void IncrementTime(int id) {
    //    timer += Time.deltaTime;
    //    if (timer >= waitingTime[id])
    //    {
    //        timer = 0;
    //        CurrentWayPointID++;
    //        GameObject.Find("cap_obj (" + id + ")").GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 0);
    //    }
    //}
}
