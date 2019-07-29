using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
	public GameObject mainMenuPage;
	public GameObject instructionsPage;
    public GameObject instructionsPage2;

	void Start ()
	{
		SetPage("Menu");
	}

	public void SetPage (string page)
	{
		switch(page){
			case "Menu":{
                mainMenuPage.SetActive(true);
				instructionsPage.SetActive(false);
                instructionsPage2.SetActive(false);
				break;
			}
			case "Instructions":{
				mainMenuPage.SetActive(false);
				instructionsPage.SetActive(true);
                instructionsPage2.SetActive(false);
				break;
			}
            case "Instructions2":{
                mainMenuPage.SetActive(false);
                instructionsPage.SetActive(false);
                instructionsPage2.SetActive(true);
                break;
            }
		}
	}

	public void PlayGame()
	{
        SceneManager.LoadScene("Pixel Cas A");
	}

	public void QuitGame ()
	{
		Application.Quit();
	}
}
