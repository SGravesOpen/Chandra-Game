using UnityEngine;
using System;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Game : MonoBehaviour 
{

	public GameObject[] colorSquares;	//Array holding all the color squares on the board.
    public GameObject[] legendSquares;  // Array holding all of the squares in the legend. 
    public GameObject currentColorSquare;
    public Dictionary<Color, int[]> legendDict;
    public Text feedback;
    public HashSet<int> blankSquareIndices;
    public GameObject[] squares;
    public Texture2D image;
	public GameMode gameMode;           //The game mode that the player selected on the main menu.
    public RawImage casA;
    public String nextScene;
  
    private Color[] pix;
    private int done;
    private Texture2D zoomedImage;
    private float timer;
    private int currDim;
    private int currX;
    private int currY;

    void Start()
    {
        blankSquareIndices = new HashSet<int> {14, 16, 17, 18, 24, 29, 30, 33, 38, 39, 40, 52, 
            56, 57, 58, 60, 62, 64, 67, 69, 70, 75, 78, 81, 82, 84, 96, 101, 102, 106};
        pix = image.GetPixels(396, 314, 11, 11);
        Array.Reverse(pix);
        done = 0;

        for (int i = 0; i < squares.Length; i++)
        {
            if(!blankSquareIndices.Contains(i))
            {
                squares[i].GetComponent<Image>().color = pix[i];
            }
        }
        zoomedImage = new Texture2D(11, 11);
        timer = 0.0f;
        currDim = 11;
        currX = 396;
        currY = 314;
    }

    void Awake()
    {
        
        // Hard code legend dictionary to add color ranges 
        legendDict = new Dictionary<Color, int[]>
        {
            { legendSquares[0].GetComponent<Image>().color, new int[] { 0, 39 } },
            { legendSquares[1].GetComponent<Image>().color, new int[] { 40, 80 } },
            { legendSquares[2].GetComponent<Image>().color, new int[] { 81, 120 } },
            { legendSquares[3].GetComponent<Image>().color, new int[] { 121, 160 } },
            { legendSquares[4].GetComponent<Image>().color, new int[] { 161, 250 } }
        };

        //printDict(legendDict);
        gameMode = (GameMode)PlayerPrefs.GetInt("GameMode");								//The gamemode gets set as the int identifier get loaded from the player prefs and converted to the enum element.
	}

	void Update ()
    {
        if (currDim == 758)
        {
            timer += Time.deltaTime;

            if (timer > .5f)
            {
                timer = 0;
                ZoomOut(82, 0, 758);
                currDim += 1;
                done = 3;
            }
        }

        if (done == 1)
        {
            GameObject grid = GameObject.Find("ColorSquares");
            GameObject legend = GameObject.Find("Legend");
            GameObject legendText = GameObject.Find("LegendText");
            GameObject menu = GameObject.Find("MenuButton");
            GameObject selected = GameObject.Find("Selected Color");
            GameObject currentColor = GameObject.Find("Current Color Square");
            GameObject feed = GameObject.Find("Feedback");
            GameObject pixelCas = GameObject.Find("Cas. A");
            grid.SetActive(false);
            legend.SetActive(false);
            legendText.SetActive(false);
            menu.SetActive(false);
            selected.SetActive(false);
            currentColor.SetActive(false);
            feed.SetActive(false);
            pixelCas.SetActive(false);
            casA.color = Color.white;
            ZoomOut(396, 314, 11);
            done = 2;
        }

        if (done == 0)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                if (blankSquareIndices.Contains(i) && squares[i].GetComponent<Image>().color != pix[i])
                {
                    return;
                }
            }
            done = 1;
        }

        if (done == 2 && currDim != 758)
        {
            ZoomOut(currX, currY, currDim);
            currDim += 1;
            if (currDim < 400 && currDim % 2 == 0)
            {
                currX -= 1;
                currY -= 1;
            }
            else if(currDim > 400 && currDim % 3 == 0)
            {
                currX -= 1;
                currY -= 1;
            }
        }

        if (done == 3)
        {
            timer += Time.deltaTime;

            if (timer > 4f)
            {
                SceneManager.LoadScene(nextScene);
            }
            float step = 80 * Time.deltaTime;
            GameObject target = GameObject.Find("EarthHigh");
            GameObject binary = GameObject.Find("Binary");
            float targetScale = 0.01f;
            float shrinkSpeed = 15f;
            casA.transform.localScale = Vector3.Lerp(casA.transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);

            if (timer > 1f)
            {
                targetScale = 0.1f;
                shrinkSpeed = 10f;
                binary.GetComponent<TextMeshPro>().alpha = 255;
                binary.transform.position = Vector3.MoveTowards(binary.transform.position, target.transform.position, step);

                binary.transform.localScale = Vector3.Lerp(binary.transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);
            }

        }
    }

    void ZoomOut(int x, int y, int dim) 
    {
        zoomedImage = new Texture2D(dim, dim);
        pix = image.GetPixels(x, y, dim, dim);
        zoomedImage.SetPixels(pix);
        zoomedImage.Apply();
        casA.texture = zoomedImage;
        casA.GraphicUpdateComplete();
    }


	void FailGame ()
	{
		LoadMenu();																			//Then we load the menu using the LoadMenu function.
	}

	public void CheckSquare (GameObject square)												//When a square gets clicked on this public function gets called. Takes in the clicked color square.
	{
        for (int i = 0; i < legendSquares.Length; i++)
        {
            if (legendSquares[i] == square)
            {
                Color pickedColor = legendSquares[i].GetComponent<Image>().color;
                if(pickedColor != currentColorSquare.GetComponent<Image>().color) 
                {
                    Debug.Log(message: pickedColor + "Color you just chose");
                    currentColorSquare.GetComponent<Image>().color = pickedColor;                
                }
                return;
            }
        }
       
        for (int i = 0; i < colorSquares.Length; i++)
        {
            if (colorSquares[i] == square)
            {
                print("Check square number: " + square.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
                if(colorSquares[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text != "") 
                {
                    print("Checked to see if square had text");
                    int value = int.Parse(colorSquares[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
                    //  Debug.Log(currentColorSquare.GetComponent<Image>().color);
                    Debug.Log( legendDict[currentColorSquare.GetComponentInChildren<Image>().color]);
     
                    int[] range = legendDict[currentColorSquare.GetComponentInChildren<Image>().color];
                    print(range[0] + " - " + range[1]);
                    print("made the value and range variables: Value = " + value + " " + "Range = " + range[0] + "-" + range[1]);

                    if (value >= range[0] && value < range[1]) 
                    {
                        colorSquares[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText("");
                        string squareName = colorSquares[i].gameObject.name;
                        string id = squareName.Substring(13, squareName.Length - 14);
                        colorSquares[i].GetComponent<Image>().color = pix[int.Parse(id)];
                        feedback.text = "Correct!";
                    } else {
                        feedback.text = "Sorry, wrong color :(";
                    }
                    return;
                }
            }
        }
    }	

	public void LoadMenu ()																	//This function loads the menu. Its used when the player fails the game or when the menu button gets pressed.
	{	
        SceneManager.LoadScene("Menu");			
	}
}

public enum GameMode {NORMAL = 0, TIME_RUSH = 1}											//The GameMode enumurator stores the two gamemodes. Normal and Time Rush.
