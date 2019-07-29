using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraMoveScript : MonoBehaviour
{
 
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;
    public SteamVR_Action_Boolean m_ButtonAction;
   
    public EditorPath PathToFollow; //Path for tour
    private List<Transform>[] paths = new List<Transform>[10];
    public GameObject[] Text; //list of text to show at event 

    private int counter = -1;
    private int CurrentPoint = 0;
    private float ReachDistance = 0.001f;
    public int speed = 20;

    //private bool isPlaying;
    private bool isMoving = false;
   
    private Vector3 StartP;
   
  /**  void Awake()
    {
        float theta = 0;
        for (int i = 0; i < 9; i++)
        {
            if (theta < 361)
            { 
            float r = Vector3.Distance(transform.position, Ship.transform.position); //radius of rotation
            Vector3 NextStop = new Vector3(r * Mathf.Cos(theta), 0, r * Mathf.Sin(theta));
            ShipCircle[i] = NextStop;
            theta += 45.0f;
            }             
        }
    }
        **/
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
         
            Text[i].SetActive(false);
            List<Transform> point = new List<Transform> { PathToFollow.path_objs[i] };
            Debug.Log(point[0]);
            paths[i] = point;
        }
        Debug.Log(paths.Length);
        Text[0].SetActive(true);
        //Text[0].GetComponent<AudioSource>().Play();
        
        //Getting paths from PathToFollow given in inspector
       

    }
    List<Transform> NextPath;
    Vector3 NextPoint;

    // Update is called once per frame
    void Update()
    {
        print(isMoving);
        if (isMoving)
        {
            //print(counter);
            //NextPath = paths[counter];
            
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, Time.deltaTime * speed);
            PlayAudio(counter);
            //Text[counter].SetActive(true);
           
            float distance = Vector3.Distance(transform.position, NextPoint);

            if(distance <= ReachDistance)
            {
                if(CurrentPoint == NextPath.Count-1)
                {
                    isMoving = false;
                    CurrentPoint = 0;
                    
                }
                else
                {
                    //Text[counter].SetActive(false);
                    CurrentPoint++;
                    NextPoint = NextPath[CurrentPoint].position;
                    
                }

               
            }                    
        }
        else if ((!isMoving) && m_ClickAction.GetStateDown(m_TargetSource))
        {
            isMoving = true;
            if (counter < paths.Length - 1)
            {

                
                counter++;
                NextPath = paths[counter];
                NextPoint = NextPath[CurrentPoint].position;
               // Text[counter].SetActive(true);
                PlayAudio(counter);
            }
            else
            {
                counter = -1;
            }
            print(counter);
            
        }
        
        /**
        if (m_ClickAction.GetStateDown(m_TargetSource))            
        {
            counter++;
            print("We hit the button");
          
            /*check if moving (make flag)
             *  yes - Move towards
             *  check if we are close to the next point is there a next point (nextpoint needs to be variable)
             *      yes - set flag false
             * 
             * */
        /**
       Vector3 NextPath;
       //
       
       
           if (counter > paths.Capacity - 1)
           {

               Text[counter-1].SetActive(false);
               Text[counter-1].GetComponent<AudioSource>().Stop();
               counter = 0;
               NextPath = paths[counter].position;
               Text[counter].SetActive(true);
               transform.position = NextPath;
               PlayAudio(counter);

           }
            //Executing tour movement
           else
           {
               
               Text[counter-1].GetComponent<AudioSource>().Stop();
               Text[counter-1].SetActive(false);
         
               NextPath = paths[counter].position;
               Text[counter].SetActive(true);
               print("We should have changed spots");              
               transform.position = NextPath;
               PlayAudio(counter);
               print(counter);
           }
       /**
       //Checking for index in bounds
       if (counter >= paths.Capacity - 1)
       {
           
          
           //Disable the current text and audio for that text
           if (isPlaying)
           {
               
               isPlaying = false;
           }

           //Reseting path
           Text[counter - 1].GetComponent<AudioSource>().Stop();
           Text[counter - 1].SetActive(false);
           NextPath = paths[0].position;
           print("Next Path set to: " + NextPath);
           Text[counter].SetActive(true);
          // Text[counter].GetComponent<AudioSource>().Play();
           PlayAudio(counter);
           transform.position = NextPath;
           counter = 0;
           // transform.position = Vector3.MoveTowards(transform.position, NextPath, Time.deltaTime * speed);
           //   transform.LookAt(Text[0].transform.position);
       }
       //Executing tour movement
       else
       {
           if (isPlaying)
           {
               Text[counter].GetComponent<AudioSource>().Stop();
               Text[counter].SetActive(false);
           }

           NextPath = paths[counter].position;

           transform.position = NextPath;
         //  transform.LookAt(Text[counter].transform.position);
           //Vector3.MoveTowards(transform.position, NextPath, Time.deltaTime * speed);
           //transform.position = Transform.LookAt(Text[counter].transform);

           counter++;
           Text[counter].SetActive(true);
           PlayAudio(counter);

           isPlaying = true;
           isMoving = true;
       }
       **/

        /**
        else if (m_ButtonAction.GetStateDown(m_TargetSource))
        {
            Text[counter].GetComponent<AudioSource>().Stop();
            Text[counter].SetActive(false);
            transform.position = paths[0][0].position;
            counter = 0;
        }
        **/
    }

    private bool Click()
    {
        return m_ClickAction.GetStateDown(m_TargetSource);

    }


    //Function that moves the player through the tour 
    /**
    public void Tour(int index)
    {

        Vector3 NextPath; 

        //Check if we are at the last track and need to restart   
        if (index >= PathToFollow.path_objs.Capacity-1)
        {
            NextPath = paths[0].position;
            print("Next Path set to: " + NextPath);
            Text[0].SetActive(true);
            Text[0].GetComponent<AudioSource>().Play();
            index = 0;
        } 
        else
        {
            NextPath = paths[index].position;
            print("Next Path set to: " + NextPath);
            Text[index].SetActive(true);
            Text[index].GetComponent<AudioSource>().Play();
        }

        // Debug.Log("Name of selected assest: " + selectedAsset.name);
        //print("Before I moved I was at: " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, NextPath, Time.deltaTime * 20f);

       // NextPath;
        

        print("We should be moving to : " + NextPath);
        isPlaying = true;
    }
    **/

    //Function that calls play on the audio sources for the text
    public void PlayAudio(int index)
    {
       if(index > 0)
        {
            Text[index-1].SetActive(false);
            Text[index -1].GetComponent<AudioSource>().Stop();
        } else if (index >= Text.Length || index == -1)
        {

            index = 0;
            Text[9].SetActive(false);
        }
        print(index);
        Text[index].GetComponent<AudioSource>().Play();
        Text[index].SetActive(true);
    }

   
}
