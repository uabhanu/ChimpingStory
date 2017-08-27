//using CompleteProject;
//using GooglePlayGames;
using System.Collections;
using System.IO;
using UnityEngine;
//using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	//BhanusPurchaser bhanusPurchaseScript;
	bool m_chimpGrounded;
	bool m_chimpSuperMode;

    [SerializeField] bool m_adWatched;

    [SerializeField] Button m_selfieButton;

    [SerializeField] ChimpController m_chimpControlScript;

    [SerializeField] GameObject /*m_adsMenuObj , m_dollarButtonObj , m_iapMenuObj ,*/ m_pauseButtonObj , m_pauseMenuObj , m_quitButtonObj , m_quitMenuObj , m_restartButtonObj;

    [SerializeField] GameObject m_selfieButtonObj;

    [SerializeField] GameObject[] m_infoObjs;

    [SerializeField] ParticleSystem m_selfieButtonParticleSystem;

    [SerializeField] ScoreManager m_scoreManagementScript;

    [SerializeField] string m_level;

	//public GameObject m_chimpCam;

	void Start() 
	{
		//bhanusPurchaseScript = GetComponent<BhanusPurchaser>();

        //if (Advertisement.isSupported)
        //{
        //    Advertisement.Initialize("rewardedVideo");
        //}

        Time.timeScale = 1f;
	}

    IEnumerator ButtonInteraction()
    {
        yield return new WaitForSeconds(0.7f);
        m_selfieButton.interactable = true;
    }
	
	public void AchievementsUI()
	{
		Social.ShowAchievementsUI();
	}

	public void AdsNo()
	{
		//m_adsMenuObj.SetActive(false);
		m_adWatched = false;
		PlayerPrefs.DeleteKey("BananaScore");
        PlayerPrefs.DeleteKey("BananasLeft");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
		
	public void AdsYes()
	{
		Debug.Log("Ads Yes Button"); //Working
		m_adWatched = true;
		//ShowRewardedAd();
	}

	public void Back()
	{
		//dollarButton.SetActive(true);
		//m_iapMenuObj.SetActive(false);
		m_pauseButtonObj.SetActive(true);
		Time.timeScale = 1f;
	}

	//void HandleShowResult(ShowResult result)
	//{
	//	switch(result)
	//	{
	//		case ShowResult.Finished:
	//			Debug.Log ("The ad was successfully shown.");
	//			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	//		break;

	//		case ShowResult.Skipped:
	//			Debug.Log("The ad was skipped before reaching the end.");
	//		break;

	//		case ShowResult.Failed:
	//			Debug.LogError("The ad failed to be shown.");
	//		break;
	//	}
	//}

	public void IAP()
	{
		//dollarButton.SetActive(false);
		//m_iapMenuObj.SetActive(true);
		m_pauseButtonObj.SetActive(false);
		Time.timeScale = 0f;
		//This method should launch IAP Panel you design later which will have a buyable item based on which BuyConsumable should be called
	}

    public void LeaderboardUI()
    {
        Social.ShowLeaderboardUI();
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
		//bhanusPurchaseScript.BuyOneMonkeynut();
		//dollarButton.SetActive(true);
		//m_iapMenuObj.SetActive(false);
		Time.timeScale = 1f;
	}

	public void OneMonkeynut()
	{
		//This activates confirm panel once that's ready & deactivates iapMenu
		//bhanusPurchaseScript.BuyOneMonkeynut();
		//dollarButton.SetActive(true);
		//m_iapMenuObj.SetActive(false);
		m_pauseButtonObj.SetActive(true);
		Time.timeScale = 1f;
	}
	
	public void Pause()
	{
		Time.timeScale = 0f;
		//dollarButton.SetActive(false);
		m_pauseButtonObj.SetActive(false);
		m_pauseMenuObj.SetActive(true);

        for(int i = 0; i < m_infoObjs.Length; i++)
        {
            m_infoObjs[i].SetActive(false);
        }
	}
		
	public void PlayVideo()
	{
		Debug.Log("Play Video");
		//Handheld.PlayFullScreenMovie(videoFile.ToString() , Color.black , FullScreenMovieControlMode.Full , FullScreenMovieScalingMode.AspectFill);
	}

	public void Quit() 
	{
		m_pauseMenuObj.SetActive(false);
		m_quitMenuObj.SetActive(true);
	}

	public void QuitNo()
	{
		m_quitMenuObj.SetActive(false);
		m_pauseMenuObj.SetActive(true);
	}

	public void QuitYes()
	{
		PlayerPrefs.DeleteKey("BananaScore");
		SceneManager.LoadScene(m_level);
	}

	public void Restart() 
	{
		PlayerPrefs.DeleteKey("BananaScore");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void RestartGame()
	{
		//Debug.Log("Restart Game"); Working
		//m_adsMenuObj.SetActive(true);
		Time.timeScale = 0;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		//dollarButton.SetActive(true);
		m_pauseButtonObj.SetActive(true);
		m_pauseMenuObj.SetActive(false);

        for(int i = 0; i < m_infoObjs.Length; i++)
        {
            m_infoObjs[i].SetActive(true);
        }
	}

	public void Selfie()
	{
		Debug.Log("Selfie");
        m_selfieButton.interactable = false;
        m_selfieButtonObj.SetActive(false);
        m_selfieButtonParticleSystem.Play();
        StartCoroutine("ButtonInteraction");
	}

	public void TwoMonkeynuts()
	{
		//This activates confirm panel once that's ready & deactivates iapMenu
		//bhanusPurchaseScript.BuyTwoMonkeynuts();
		//dollarButton.SetActive(true);
		//m_iapMenuObj.SetActive(false);
		m_pauseButtonObj.SetActive(true);
		Time.timeScale = 1f;
	}

    public void WinCard()
    {
        Debug.Log("Victory is Mine");
        //Show animated count down scores Win Card 
        return;  
    }
}
