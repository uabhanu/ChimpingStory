using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
	Image m_adsAcceptButtonImage , m_adsCancelButtonImage , m_adsMenuImage , m_backToLandLoseMenuImage , m_backToLandWinMenuImage , m_backToLandWithSuperMenuImage , m_chimpionshipBeltImage , m_continueButtonImage;
    Image m_exitButtonImage , m_muteButtonImage , m_pauseButtonImage , m_pauseMenuImage , m_playButtonImage , m_quitButtonImage , m_quitAcceptButtonImage , m_quitCancelButtonImage , m_quitMenuImage;
    Image m_restartButtonImage , m_restartAcceptButtonImage , m_restartCancelButtonImage , m_restartMenuImage , m_resumeButtonImage , m_unmuteButtonImage;
    LandChimp m_landChimp;
	SoundManager m_soundManager;
    string m_applinkURL;
	Text m_adsText , m_backToLandLoseText , m_backToLandWinText , m_backToLandWithSuperText , m_highScoreDisplayText , m_highScoreValueText , m_noInternetText , m_quitText , m_restartText;

	[SerializeField] bool m_isFBShareTestMode , m_isLoggedIn , m_isMemoryLeakTestingMode , m_selfieFlashEnabled;
    [SerializeField] GameObject m_fbInviteSuccessMenuObj , m_fbShareMenuObj , m_fbShareSuccessMenuObj , m_fbShareTestMenuObj , m_loggedInObj , m_loggedOutObj;
    [SerializeField] Image m_facebookButtonImage , m_fallingLevelImage , m_fbInviteButtonImage , m_landLevelImage , m_profilePicImage , m_shareButtonImage , m_waterLevelImage;
    [SerializeField] Text m_memoryLeakTestText , m_usernameText;

    public static bool m_isTestingUnityEditor;
    public static Image m_selfieButtonImage , m_selfiePanelImage;
    public static int m_currentScene , m_playerMutedSounds;

    void Start()
	{
        BhanuFacebookInit();
        GetBhanuObjects();
    }
	
    public void Ads()
    {
        m_adsMenuImage.enabled = true;
        m_adsAcceptButtonImage.enabled = true;
        m_adsCancelButtonImage.enabled = true;
        m_adsText.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAccept()
    {
        m_adsMenuImage.enabled = false;
        m_adsAcceptButtonImage.enabled = false;
        m_adsCancelButtonImage.enabled = false;
        m_adsText.enabled = false;
        AdsShow();
    }

    public void AdsCancel()
    {
        BhanuPrefs.DeleteAll();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    void AdResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            //Debug.Log("Video completed - Offer a reward to the player");
            ScoreManager.m_scoreValue *= 0.25f;
		    ScoreManager.m_scoreValue = Mathf.Round(ScoreManager.m_scoreValue);
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(m_currentScene);
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            BhanuPrefs.DeleteAll();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            BhanuPrefs.DeleteAll();
        }
    }

    void AdsShow()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdResult;
        Advertisement.Show("rewardedVideo" , options);
    }

    public void BackToLandLoseMenu()
    {
        m_backToLandLoseMenuImage.enabled = true;
        m_backToLandLoseText.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
		m_continueButtonImage.enabled = true;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWinMenu()
    {
        m_backToLandWinMenuImage.enabled = true;
        m_backToLandWinText.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
		m_continueButtonImage.enabled = true;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWithSuperMenu()
    {
        m_backToLandWithSuperMenuImage.enabled = true;
        m_backToLandWithSuperText.enabled = true;
        m_continueButtonImage.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BhanuFacebookInit()
    {
        if(!FB.IsInitialized) 
		{
			FB.Init(FBSetInit , FBOnHideUnity);	
		}

        if(FB.IsLoggedIn && m_loggedInObj != null && m_loggedOutObj != null)
        {
            m_loggedInObj.SetActive(true);
            FBLoggedIn();
            m_loggedOutObj.SetActive(false);
        }
    }

    public void Continue()
    {
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
		Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("LandRunner");
    }

    void EndFlash()
	{
		m_selfiePanelImage.enabled = false;
	}

    public void ExitToMainMenu()
	{
        SceneManager.LoadScene("MainMenu");
    }

    void FBAppLinkURL(IAppLinkResult applinkResult) //Not sure how to use this yet so not using for now
    {
        if(!string.IsNullOrEmpty(applinkResult.Url))
        {
            m_applinkURL = "" + applinkResult.Url + "";
            Debug.Log(m_applinkURL);
        }
        else
        {
            m_applinkURL = "http://uabhanu.wixsite.com/portfolio";
        }
    }
    
    void FBAuthCallBack(IResult authResult)
	{
		if(authResult.Error != null) 
		{
			Debug.LogError("Sir Bhanu, there is an issue : " + authResult.Error);	
			m_noInternetText.enabled = true;
		} 

		else if(authResult.Error == null)
		{
			if(FB.IsLoggedIn) 
			{
				FBLoggedIn();
			} 
			else 
			{
				FBLoggedOut();
			}
		}
	}

    public void FBChallengePlayers()
    {
        FB.AppRequest
        (
            "Come and try to be the Chimpion :) ",
            null,
            new List<object>{"app_users"},
            null,
            null,
            null,
            null,
            FBChallengePlayersCallback
        );

        Screen.orientation = ScreenOrientation.Portrait;
    }

    void FBChallengePlayersCallback(IAppRequestResult appRequestResult)
    {
        if(appRequestResult.Cancelled) 
		{
			//Debug.LogWarning("Sir Bhanu, You have cancelled the invite");
            Screen.orientation = ScreenOrientation.Landscape;
		}
        
        else if(!string.IsNullOrEmpty(appRequestResult.Error))
        {
            //Debug.LogError("Sir Bhanu, There is a problem : " + appRequestResult.Error);
            Screen.orientation = ScreenOrientation.Landscape;
        }

		else if(!string.IsNullOrEmpty(appRequestResult.RawResult)) 
		{
            //Debug.LogWarning("Sir Bhanu, Your invitation : " + appRequestResult.RawResult);

            Screen.orientation = ScreenOrientation.Landscape;

            if(!m_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }
            
            m_fbInviteSuccessMenuObj.SetActive(true);
		}
    }

    public void FBInvite() //TODO When you figure out how to make this work, make Invite Button of LoggedinObj in the scene, Active
    {
        Screen.orientation = ScreenOrientation.Portrait;

        FB.Mobile.AppInvite
        (
            new System.Uri("http://uabhanu.wixsite.com/portfolio"), //TODO Game URL here when Live
            callback: FBInviteCallback
        );
    }

    public void FBInviteCallback(IResult inviteResult)
	{
		if(inviteResult.Cancelled) 
		{
			//Debug.LogWarning("Sir Bhanu, You have cancelled the invite");
            Screen.orientation = ScreenOrientation.Landscape;
		}
        
        else if(!string.IsNullOrEmpty(inviteResult.Error))
        {
            //Debug.LogError("Sir Bhanu, There is a problem : " + inviteResult.Error);
            Screen.orientation = ScreenOrientation.Landscape;
        }

		else if(!string.IsNullOrEmpty(inviteResult.RawResult)) 
		{
            //Debug.LogWarning("Sir Bhanu, Your invitation : " + inviteResult.RawResult);

            Screen.orientation = ScreenOrientation.Landscape;

            if(!m_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }
            
            m_fbInviteSuccessMenuObj.SetActive(true);
		} 
	}

    void FBLoggedIn()
	{
		m_isLoggedIn = true;

		FB.API("/me?fields=first_name" , HttpMethod.GET , FBUsernameDisplay);
		FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FBProfilePicDisplay);

		if(m_loggedInObj != null && m_loggedOutObj != null)
        {
            m_loggedInObj.SetActive(true);
		    m_loggedOutObj.SetActive(false);		
        }
        else
        {
            Debug.LogError("Sir Bhanu, Logged In & Out Objs are not assigned probably because you didn't start the game from Main Menu :)");
        }
	}

	void FBLoggedOut()
	{
		m_isLoggedIn = false;

		if(m_loggedInObj != null && m_loggedOutObj != null)
        {
            m_loggedInObj.SetActive(false);
		    m_loggedOutObj.SetActive(true);		
        }
        else
        {
            Debug.LogError("Sir Bhanu, Logged In & Out Objs are not assigned probably because you didn't start the game from Main Menu :)");
        }
	}

	public void FBLogin()
	{
        if(FB.IsInitialized)
        {
            m_noInternetText.enabled = false;
		    List<string> permissions = new List<string>();
		    permissions.Add("public_profile");
		    FB.LogInWithReadPermissions(permissions , FBAuthCallBack);
        }
	}

	void FBOnHideUnity(bool isGameShown)
	{
		if(!isGameShown) 
		{
			Time.timeScale = 0;
		} 
		else 
		{
			Time.timeScale = 1;	
		}
	}

	void FBProfilePicDisplay(IGraphResult graphicResult)
	{
		if(graphicResult.Texture != null && m_currentScene == 0)
		{
			m_profilePicImage.sprite = Sprite.Create(graphicResult.Texture , new Rect(0 , 0 , 480 , 480) , new Vector2());
		}
	}

	void FBSetInit()
	{
		if(FB.IsLoggedIn) 
		{
			FBLoggedIn();
		} 
		else 
		{
			FBLoggedOut();
		}
	}

	public void FBShare()
	{
		Screen.orientation = ScreenOrientation.Portrait;

		FB.ShareLink
		(
			contentTitle: "Fourth Lion Studios Message",
			contentURL: new System.Uri("http://uabhanu.wixsite.com/portfolio"), //TODO Game URL here when Live
			contentDescription: "We really hope you love the game", 
            callback: FBShareCallback
		);
	}

	void FBShareCallback(IShareResult shareResult)
	{
		if(shareResult.Cancelled || !string.IsNullOrEmpty (shareResult.Error)) 
		{
			//Debug.LogError("Sir Bhanu, there is an " + shareResult.Error);
            Screen.orientation = ScreenOrientation.Landscape;
		} 

		else if(!string.IsNullOrEmpty(shareResult.PostId)) 
		{
            Screen.orientation = ScreenOrientation.Landscape;
		} 

		else 
		{
			Screen.orientation = ScreenOrientation.Landscape;

            if(!m_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }

            m_fbShareSuccessMenuObj.SetActive(true);
        }
	}

	void FBUsernameDisplay(IResult result)
	{
		if(result.Error == null && m_currentScene == 0)
		{
			//Debug.Log(result.ResultDictionary["first_name"]);
			m_usernameText.text =  "Hi " + result.ResultDictionary["first_name"];
		}
	}

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;

        if(m_isFBShareTestMode)
        {
            m_fbShareTestMenuObj.SetActive(true);
        }

        m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();

        if(m_isMemoryLeakTestingMode)
        {
            if(m_currentScene == 1)
            {
                m_fallingLevelImage.enabled = true;
                m_memoryLeakTestText.enabled = true;
                m_waterLevelImage.enabled = true;
            }

            if(m_currentScene == 2)
            {
                m_fallingLevelImage.enabled = true;
                m_landLevelImage.enabled = true;
                m_memoryLeakTestText.enabled = true;
            }

            if(m_currentScene == 3)
            {
                m_landLevelImage.enabled = true;
                m_memoryLeakTestText.enabled = true;
                m_waterLevelImage.enabled = true;
            }
        }

        if(m_currentScene == 0)
        {
            m_noInternetText = GameObject.Find("NoInternetDisplay").GetComponent<Text>();
            m_playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            m_quitText = GameObject.Find("QuitText").GetComponent<Text>();
            m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            m_quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            m_quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            m_quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();
        }

        else if(m_currentScene == 1)
        {
            m_adsText = GameObject.Find("AdsText").GetComponent<Text>();
            m_adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            m_adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            m_adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            m_chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
            m_exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
			m_landChimp = FindObjectOfType<LandChimp>();
			m_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
            m_restartText = GameObject.Find("RestartText").GetComponent<Text>();
			m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
            m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
            m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
            m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
			m_selfieButtonImage = GameObject.Find("SelfieButton").GetComponent<Image>();
			m_selfiePanelImage = GameObject.Find("SelfiePanel").GetComponent<Image>();
            m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    m_soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    m_soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    m_soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    m_soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }
            }
        }

		else
		{
			m_backToLandLoseMenuImage = GameObject.Find("BackToLandLoseMenu").GetComponent<Image>();
			m_backToLandLoseText = GameObject.Find("BackToLandLose").GetComponent<Text>();
			m_backToLandWinMenuImage = GameObject.Find("BackToLandWinMenu").GetComponent<Image>();
			m_backToLandWinText = GameObject.Find("BackToLandWin").GetComponent<Text>();
            m_backToLandWithSuperMenuImage = GameObject.Find("BackToLandWithSuperMenu").GetComponent<Image>();
			m_backToLandWithSuperText = GameObject.Find("BackToLandWithSuper").GetComponent<Text>();
            m_chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
			m_continueButtonImage = GameObject.Find("ContinueButton").GetComponent<Image>();
			m_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
			m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    m_soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    m_soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    m_soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    m_soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }
            }
		}

        Time.timeScale = 1;
    }

    public void GoToFallingLevel()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("FallingDown");
    }

    public void GoToLandLevel()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("LandRunner");
    }

    public void GoToWaterLevel()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("WaterSwimmer");
    }

    public void MuteUnmute()
    {
        if(MusicManager.m_musicSource != null)
        {
            if(m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = false;
                MusicManager.m_musicSource.Pause();
                m_playerMutedSounds = 1;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                m_unmuteButtonImage.enabled = true;
                m_soundManager.m_soundsSource.enabled = false;
            }

            else if(!m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = true;
                MusicManager.m_musicSource.Play();
                m_playerMutedSounds = 0;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                m_unmuteButtonImage.enabled = false;
                m_soundManager.m_soundsSource.enabled = true;
            }
        }
    }

    public void OK()
    {
        SceneManager.LoadScene(m_currentScene);
    }

    public void Pause()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(MusicManager.m_musicSource.isPlaying)
            {
                MusicManager.m_musicSource.Pause();
                m_muteButtonImage.enabled = false;
            }
            else
            {
                m_unmuteButtonImage.enabled = false;
            }
        }

        m_chimpionshipBeltImage.enabled = false;
        
        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = true;
        }

		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        
        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = true;
        }
        
		m_pauseButtonImage.enabled = false;
		m_pauseMenuImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		if(m_selfiePanelImage != null)
		{
			m_selfieButtonImage.enabled = false;	
		}

		Time.timeScale = 0;
	}

	public void Play()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void Quit()
	{
        m_facebookButtonImage.enabled = false;
        m_fbInviteButtonImage.enabled = false;
        m_noInternetText.enabled = false;
        m_playButtonImage.enabled = false;
        m_profilePicImage.enabled = false;
		m_quitButtonImage.enabled = false;
        m_usernameText.enabled = false;

		m_quitMenuImage.enabled = true;
		m_quitAcceptButtonImage.enabled = true;
		m_quitCancelButtonImage.enabled = true;
		m_quitText.enabled = true;
	}

	public void QuitAccept()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancel()
	{
        m_facebookButtonImage.enabled = true;
        m_fbInviteButtonImage.enabled = true;
		m_playButtonImage.enabled = true;
        m_profilePicImage.enabled = true;
		m_quitButtonImage.enabled = true;
        m_usernameText.enabled = true;

		m_quitMenuImage.enabled = false;
		m_quitAcceptButtonImage.enabled = false;
		m_quitCancelButtonImage.enabled = false;
		m_quitText.enabled = false;
	}

	public void Restart()
	{
		m_exitButtonImage.enabled = false;
		m_pauseMenuImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;

		m_restartMenuImage.enabled = true;
		m_restartAcceptButtonImage.enabled = true;
		m_restartCancelButtonImage.enabled = true;
		m_restartText.enabled = true;
	}

	public void RestartAccept()
	{
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
        }
        
        BhanuPrefs.DeleteAll();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
		SceneManager.LoadScene(m_currentScene);
	}

    public void RestartCancel()
	{
		m_exitButtonImage.enabled = true;
		m_pauseMenuImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		m_restartMenuImage.enabled = false;
		m_restartAcceptButtonImage.enabled = false;
		m_restartCancelButtonImage.enabled = false;
		m_restartText.enabled = false;
	}

	public void Resume()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(m_playerMutedSounds == 0)
            {
                MusicManager.m_musicSource.Play();
                m_muteButtonImage.enabled = true;
            }

            else if(m_playerMutedSounds == 1)
            {
                m_unmuteButtonImage.enabled = true;
            }
        }

        m_chimpionshipBeltImage.enabled = true;

        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = false;
        }

		m_highScoreDisplayText.enabled = true;
		m_highScoreValueText.enabled = true;
		m_pauseButtonImage.enabled = true;
		m_pauseMenuImage.enabled = false;

        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = false;
        }

        m_resumeButtonImage.enabled = false;

		Time.timeScale = 1;
	}

	public void SelfieClicked()
	{
		m_soundManager.m_soundsSource.clip = m_soundManager.m_selfie;
		
        if(m_soundManager.m_soundsSource.enabled)
        {
            m_soundManager.m_soundsSource.Play();
        }

		m_selfieButtonImage.enabled = false;

		if(m_selfieFlashEnabled)
		{
			m_selfiePanelImage.enabled = true;
			Invoke("EndFlash" , 0.25f);
		}

		if(m_landChimp.m_isSlipping)
        {
            ScoreManager.m_scoreValue += 60;
        }

        else if(m_landChimp.m_isSuper) 
		{
			ScoreManager.m_scoreValue += 200;
		} 

		else 
		{
			ScoreManager.m_scoreValue += 20;
		}

		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
	}
}
