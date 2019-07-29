using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontChanger : MonoBehaviour
{

    private Text[] GetText;
    public Font newFont;
    public Color newColor;

    // Start is called before the first frame update
    void Start()
    {
        GetText = FindObjectsOfType<Text>();

        foreach (Text t in GetText)
        {
            t.font = newFont;
            t.color = newColor;
        }
    }

}
