using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public GameObject exit;
	public GameObject loginButtonsObj;
	public GameObject playButton;
	public GameObject quitButton;
	public GameObject quitImage;
	public string gameLevel;

	public void No()
	{
		exit.SetActive(false);
		quitImage.SetActive(false);

		loginButtonsObj.SetActive(true);
		playButton.SetActive(true);
		quitButton.SetActive(true);
	}

	public void Play() 
	{
		SceneManager.LoadScene(gameLevel);
	}

	public void Quit() 
	{
		exit.SetActive(true);
		quitImage.SetActive(true);

		loginButtonsObj.SetActive(false);
		playButton.SetActive(false);
		quitButton.SetActive(false);
	}
		
	public void Yes()
	{
		Application.Quit();
	}
}
