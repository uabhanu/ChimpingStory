using CompleteProject;
//using GooglePlayGames;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	BhanusPurchaser bhanusPurchaseScript;
	bool chimpGrounded;
	bool chimpSuperMode;
	[SerializeField] GameObject selfieButton;

	public bool adWatched;
	public ChimpController chimpControlScript;
	public GameObject chimpCam;
	public GameObject adsMenu;
	public GameObject dollarButton;
	public GameObject iapMenu;
	public GameObject pauseButton;
	public GameObject pauseImage;
	public GameObject pauseMenu;
	public GameObject quitButton;
	public GameObject quitMenu;
	public GameObject restartButton;
	public ScoreManager scoreManagementScript;
	public string mainMenuLevel;

	void Start() 
	{
		bhanusPurchaseScript = GetComponent<BhanusPurchaser>();

		if(Advertisement.isSupported)
		{
			Advertisement.Initialize("rewardedVideo");
		}
			
		Time.timeScale = 1f;
	}
		
	public void AchievementsUI()
	{
		Social.ShowAchievementsUI();
	}

	public void AdsNo()
	{
		adsMenu.SetActive(false);
		adWatched = false;
		PlayerPrefs.DeleteKey("BananaScore");
        PlayerPrefs.DeleteKey("BananasLeft");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
		
	public void AdsYes()
	{
		Debug.Log("Ads Yes Button"); //Working
		adWatched = true;
		ShowRewardedAd();
	}

	public void Back()
	{
		dollarButton.SetActive(true);
		iapMenu.SetActive(false);
		pauseButton.SetActive(true);
		Time.timeScale = 1f;
	}

	void HandleShowResult(ShowResult result)
	{
		switch(result)
		{
			case ShowResult.Finished:
				Debug.Log ("The ad was successfully shown.");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			break;

			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
			break;

			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void IAP()
	{
		dollarButton.SetActive(false);
		iapMenu.SetActive(true);
		pauseButton.SetActive(false);
		Time.timeScale = 0f;
		//This method should launch IAP Panel you design later which will have a buyable item based on which BuyConsumable should be called
	}

//	public void LoadGame()
//	{
//		int slot = 0;
//		((PlayGamesPlatform)Social.Active).LoadState(slot , this);
//	}

	public void MonkeynutNo()
	{
		//This deactivates confirm panel and activates iapMenu
	}

	public void MonkeynutYes()
	{
		bhanusPurchaseScript.BuyOneMonkeynut();
		dollarButton.SetActive(true);
		iapMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void OneMonkeynut()
	{
		//This activates confirm panel once that's ready & deactivates iapMenu
		bhanusPurchaseScript.BuyOneMonkeynut();
		dollarButton.SetActive(true);
		iapMenu.SetActive(false);
		pauseButton.SetActive(true);
		Time.timeScale = 1f;
	}

	public void OnGameLoaded(bool success , int slot , byte[] data)
	{
		if(success) 
		{

		} 
		else 
		{

		}
	}

	public void OnGameSaved(bool success , int slot)
	{
		if(success)
		{

		}
		else
		{

		}
	}
		
	public void Pause()
	{
		Time.timeScale = 0f;
		dollarButton.SetActive(false);
		pauseButton.SetActive(false);
		pauseMenu.SetActive(true);
	}
		
	public void PlayVideo()
	{
		Debug.Log("Play Video");
		//Handheld.PlayFullScreenMovie(videoFile.ToString() , Color.black , FullScreenMovieControlMode.Full , FullScreenMovieScalingMode.AspectFill);
	}

	public void Quit() 
	{
		pauseMenu.SetActive(false);
		quitMenu.SetActive(true);
	}

	public void QuitNo()
	{
		quitMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}

	public void QuitYes()
	{
		PlayerPrefs.DeleteKey("BananaScore");
		SceneManager.LoadScene(mainMenuLevel);
	}

	public void Restart() 
	{
		PlayerPrefs.DeleteKey("BananaScore");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void RestartGame()
	{
		//Debug.Log("Restart Game"); Working
		adsMenu.SetActive(true);
		Time.timeScale = 0;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		dollarButton.SetActive(true);
		pauseButton.SetActive(true);
		pauseMenu.SetActive(false);
	}

	public void Selfie()
	{
		Debug.Log("Selfie");
		selfieButton.SetActive(false);
	}

//	public void SaveGame()
//	{
//		byte[] gamedata;
//		int slot = 0;
//
//		((PlayGamesPlatform) Social.Active).UpdateState(slot , gamedata , this);
//	}

	public void ShowRewardedAd()
	{
		if(Advertisement.IsReady("rewardedVideo"))
		{
			Debug.Log("Ads Yes Button showing Rewarded Ad");
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo" , options);
		}
	}

	public void TwoMonkeynuts()
	{
		//This activates confirm panel once that's ready & deactivates iapMenu
		bhanusPurchaseScript.BuyTwoMonkeynuts();
		dollarButton.SetActive(true);
		iapMenu.SetActive(false);
		pauseButton.SetActive(true);
		Time.timeScale = 1f;
	}

	public void LeaderboardUI()
	{
		Social.ShowLeaderboardUI();
	}
}
