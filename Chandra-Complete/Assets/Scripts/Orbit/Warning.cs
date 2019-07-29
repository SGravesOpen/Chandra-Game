using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warning : MonoBehaviour
{

    public GameObject ship;
    public Canvas canvas;
    public GameObject TMP;
    private TextMeshProUGUI text;




    // Start is called before the first frame update
    void Start()
    {
        text = TMP.GetComponent<TextMeshProUGUI>();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            float distance = Vector3.Magnitude(transform.position - ship.transform.position);

            if (distance <= 60f)
            {
                canvas.gameObject.SetActive(true);

            } else if(distance >= 246f) {
                text.text = "OH NO! COME BACK!";
                canvas.gameObject.SetActive(true);
                // access text mesh and update the stuff
            } else
            {
                canvas.gameObject.SetActive(false);
            }

        } else {
            text.text = "FAILURE";
            canvas.gameObject.SetActive(true);
            // access text mesh and update the stuff
        }
    }
}
