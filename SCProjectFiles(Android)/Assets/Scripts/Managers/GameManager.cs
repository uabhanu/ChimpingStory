using Facebook.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource _musicSource;
    bool _profilePicEnabled = false;
    Dictionary<string , object> _highScoresData;
    Dictionary<string , string> _scores = null;
	Image _adsAcceptButtonImage , _adsCancelButtonImage , _adsMenuImage , _backToLandLoseMenuImage , _backToLandWinMenuImage , _backToLandWithSuperMenuImage , _chimpionshipBeltImage , _continueButtonImage;
    Image _exitButtonImage , _muteButtonImage , _pauseButtonImage , _pauseMenuImage , _playButtonImage , _quitButtonImage , _quitAcceptButtonImage , _quitCancelButtonImage , _quitMenuImage;
    Image _restartButtonImage , _restartAcceptButtonImage , _restartCancelButtonImage , _restartMenuImage , _resumeButtonImage , _unmuteButtonImage;
    LandChimp _landChimp;
    List<object> _highScoresList = null;
	SoundManager _soundManager;
    string _applinkURL;
	Text _adsText , _backToLandLoseText , _backToLandWinText , _backToLandWithSuperText , _highScoreDisplayText , _highScoreValueText , _noInternetText , _quitText , _restartText;

	[SerializeField] bool _isFBShareTestMode , _selfieFlashEnabled;
    [SerializeField] GameObject _fbInviteSuccessMenuObj , _fbShareMenuObj , _fbShareSuccessMenuObj , _fbShareTestMenuObj , _loggedInObj , _loggedOutObj;
    [SerializeField] Image _facebookButtonImage , _fallingLevelImage , _fbInviteButtonImage , _landLevelImage , _profilePicImage , _shareButtonImage , _waterLevelImage;
    [SerializeField] Text _fbScoreText , _memoryLeakTestText , _usernameText;

    public static bool m_isMemoryLeakTestingMode , m_isTestingUnityEditor;
    public static Image m_selfieButtonImage , m_selfiePanelImage;
    public static int m_currentScene , m_playerMutedSounds;

    void Start()
	{
        FBInit();
        m_isMemoryLeakTestingMode = true; //TODO Remove this for Live Version
        Invoke("FBLogInCheck" , 0.2f);
        GetBhanuObjects();
    }

    public void Ads()
    {
        _adsMenuImage.enabled = true;
        _adsAcceptButtonImage.enabled = true;
        _adsCancelButtonImage.enabled = true;
        _adsText.enabled = true;
        _chimpionshipBeltImage.enabled = false;
        _muteButtonImage.enabled = false;
        _pauseButtonImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        _unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAccept()
    {
        _adsMenuImage.enabled = false;
        _adsAcceptButtonImage.enabled = false;
        _adsCancelButtonImage.enabled = false;
        _adsText.enabled = false;
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
        _backToLandLoseMenuImage.enabled = true;
        _backToLandLoseText.enabled = true;
        _chimpionshipBeltImage.enabled = false;
		_continueButtonImage.enabled = true;
		_highScoreDisplayText.enabled = false;
		_highScoreValueText.enabled = false;
        _muteButtonImage.enabled = false;
        _pauseButtonImage.enabled = false;
        _unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWinMenu()
    {
        _backToLandWinMenuImage.enabled = true;
        _backToLandWinText.enabled = true;
        _chimpionshipBeltImage.enabled = false;
		_continueButtonImage.enabled = true;
		_highScoreDisplayText.enabled = false;
		_highScoreValueText.enabled = false;
        _muteButtonImage.enabled = false;
        _pauseButtonImage.enabled = false;
        _unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWithSuperMenu()
    {
        _backToLandWithSuperMenuImage.enabled = true;
        _backToLandWithSuperText.enabled = true;
        _continueButtonImage.enabled = true;
        _chimpionshipBeltImage.enabled = false;
		_highScoreDisplayText.enabled = false;
		_highScoreValueText.enabled = false;
        _muteButtonImage.enabled = false;
        _pauseButtonImage.enabled = false;
        _unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
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
            _applinkURL = "" + applinkResult.Url + "";
            Debug.Log(_applinkURL);
        }
        else
        {
            _applinkURL = "http://uabhanu.wixsite.com/portfolio";
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

            if(!_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }
            
            _fbInviteSuccessMenuObj.SetActive(true);
		}
    }

    void FBHighScoreGet()
    {
       FB.API("/app/scores?fields=score,user.limit(30)" , HttpMethod.GET , FBHighScoreGetCallback);
    }

    void FBHighScoreGetCallback(IGraphResult getScoreResult)
    {
        _highScoresList = getScoreResult.ResultDictionary["data"] as List<object>;
        _highScoresData = _highScoresList[0] as Dictionary<string , object>;
        long highScore = (long)_highScoresData["score"];
        //_fbScoreText.text = "Score : " + highScore.ToString();
    }

    void FBHighScoreSet()
    {
        float highScore = BhanuPrefs.GetHighScore();
        //_fbScoreText.text = "Score : " + highScore.ToString();
        _scores = new Dictionary<string , string>(){ {"score" , highScore.ToString()} };
        //_fbScoreText.text = "Score : " + _scores;
        FB.API("/me/scores" , HttpMethod.POST , FBHighScoreSetCallback , _scores);
    }

    void FBHighScoreSetCallback(IGraphResult setScoreResult)
    {
        Debug.Log(setScoreResult.RawResult); //Interestingly, this is not working
        _fbScoreText.text = setScoreResult.RawResult; //TODO This is failing with extended permissions requirement error code 200
    }

    void FBInit()
    {
        if(!FB.IsInitialized) 
		{
            HideUnityDelegate FBOnHideUnity = null;
            InitDelegate FBSetInit = null;
            FB.Init(FBSetInit , FBOnHideUnity);	
		}
    }

    public void FBInvite() //TODO When you figure out how to make this work, make Invite Button of LoggedinObj in the scene, Active
    {
        Screen.orientation = ScreenOrientation.Portrait; //TODO Do this if player has the relevant permission

        FB.Mobile.AppInvite
        (
            new Uri("http://uabhanu.wixsite.com/portfolio"), //TODO Game URL here when Live
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

            if(!_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }
            
            _fbInviteSuccessMenuObj.SetActive(true);
		} 
	}

    void FBLoggedIn()
	{
        FB.API("/me?fields=first_name" , HttpMethod.GET , FBUsernameDisplay);
        FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FBProfilePicDisplay);

        if(_loggedInObj != null && _loggedOutObj != null)
        {
            _loggedInObj.SetActive(true);
		    _loggedOutObj.SetActive(false);		
        }
        else
        {
            Debug.LogError("Sir Bhanu, Logged In & Out Objs are not assigned probably because you didn't start the game from Main Menu :)");
        }

        Screen.orientation = ScreenOrientation.Landscape;
	}

	void FBLoggedOut()
	{
		if(_loggedInObj != null && _loggedOutObj != null)
        {
            _loggedInObj.SetActive(false);
		    _loggedOutObj.SetActive(true);		
        }
        else
        {
            Debug.LogError("Sir Bhanu, Logged In & Out Objs are not assigned this is not Main Menu :)");
        }

        Screen.orientation = ScreenOrientation.Landscape;
	}

    void FBLogIn()
    {
        if(FB.IsInitialized)
        {
            _noInternetText.enabled = false;
		    List<string> permissions = new List<string>();
		    //permissions.Add("public_profile"); //TODO This may not be needed because this is one of the default permissions allowed
            //permissions.Add("publish_actions"); TODO This will work only after Facebook approval
            FB.LogInWithReadPermissions(permissions , FBLogInCallBack);
            //FB.LogInWithPublishPermissions(permissions , FBLogInCallBack); //TODO Use this when player tries to publish something
        }
    }

    void FBLogInCallBack(IResult logInResult)
	{
        if(logInResult.Cancelled) 
		{
			Debug.LogWarning("Sir Bhanu, You have cancelled the LogIn");
            FBLoggedOut();
		}
        
        else if(!string.IsNullOrEmpty(logInResult.Error))
        {
            Debug.LogWarning("Sir Bhanu, You have pressed Error Button");
            FBLoggedOut();
        }

		else if(!string.IsNullOrEmpty(logInResult.RawResult)) 
		{
            Debug.LogWarning("Sir Bhanu, Your LogIn : " + logInResult.RawResult);
            FBLoggedIn();
		}
	}

	void FBLoginButton()
	{
        Screen.orientation = ScreenOrientation.Portrait;
        FBLogIn();
        Invoke("FBLoggedIn" , 0.2f);
	}

    void FBLogInCheck()
    {
        if(FB.IsLoggedIn)
        {
            FBLoggedIn();
        }
        else
        {
            FBLoggedOut();
            Invoke("FBLogInCheck" , 0.2f);
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
        try
        {
            if(graphicResult.Texture != null && graphicResult.Error == null)
		    {
			    _profilePicImage.sprite = Sprite.Create(graphicResult.Texture , new Rect(0 , 0 , graphicResult.Texture.width , graphicResult.Texture.height) , new Vector2());
                _profilePicEnabled = true; //This is used to check if sprite created properly and display only if it is, or else, _profilePicImage won't be enabled
                _profilePicImage.enabled = true;
		    }
        }
        catch(Exception e)
        {
            Debug.Log(e);
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

	void FBShare()
	{
        FBHighScoreSet();
        FBHighScoreGet(); //TODO This is ok for now for testing but should happen only once upon game installation
        
        Screen.orientation = ScreenOrientation.Portrait;

		FB.ShareLink //TODO Do this if player has the relevant permission
		(
			contentTitle: "Fourth Lion Studios Message",
			contentURL: new Uri("http://uabhanu.wixsite.com/portfolio"), //TODO Game URL here when Live
			contentDescription: "We really hope you love the game", 
            callback: FBShareCallback
		);
	}

	void FBShareCallback(IShareResult shareResult)
	{
		if(shareResult.Cancelled) 
		{
			Debug.LogWarning("Sir Bhanu, you have cancelled the Share :)" );
            Screen.orientation = ScreenOrientation.Landscape;
		} 

        else if(!string.IsNullOrEmpty(shareResult.Error))
        {
            Debug.LogError("Sir Bhanu, You have pressed error button");
        }

		else
		{
            Debug.LogWarning("Sir Bhanu, Your Share is a success :)");
			Screen.orientation = ScreenOrientation.Landscape;

            if(!_isFBShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }

            _fbShareSuccessMenuObj.SetActive(true);
		} 
	}

	void FBUsernameDisplay(IResult usernameResult)
	{
		if(usernameResult.Error == null && m_currentScene == 0)
		{
			_usernameText.text =  "Hi " + usernameResult.ResultDictionary["first_name"];
		}
        else
        {
            FBLoggedOut();
        }
	}

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;

        if(_isFBShareTestMode)
        {
            _fbShareTestMenuObj.SetActive(true);
        }

        m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();

        if(m_isMemoryLeakTestingMode)
        {
            if(m_currentScene == 1)
            {
                _fallingLevelImage.enabled = true;
                _memoryLeakTestText.enabled = true;
                _waterLevelImage.enabled = true;
            }

            if(m_currentScene == 2)
            {
                _fallingLevelImage.enabled = true;
                _landLevelImage.enabled = true;
                _memoryLeakTestText.enabled = true;
            }

            if(m_currentScene == 3)
            {
                _landLevelImage.enabled = true;
                _memoryLeakTestText.enabled = true;
                _waterLevelImage.enabled = true;
            }
        }

        if(m_currentScene == 0)
        {
            _noInternetText = GameObject.Find("NoInternetDisplay").GetComponent<Text>();
            _playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            _quitText = GameObject.Find("QuitText").GetComponent<Text>();
            _quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            _quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            _quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            _quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();
        }

        else if(m_currentScene == 1)
        {
            _adsText = GameObject.Find("AdsText").GetComponent<Text>();
            _adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            _adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            _adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            _chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
            _exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
			_landChimp = FindObjectOfType<LandChimp>();
			_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            _muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
            _restartText = GameObject.Find("RestartText").GetComponent<Text>();
			_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            _restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
            _restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
            _restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
            _restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
			m_selfieButtonImage = GameObject.Find("SelfieButton").GetComponent<Image>();
			m_selfiePanelImage = GameObject.Find("SelfiePanel").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            _unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    _muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    _muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    _unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;
                    _unmuteButtonImage.enabled = true;
                }
            }
        }

		else
		{
			_backToLandLoseMenuImage = GameObject.Find("BackToLandLoseMenu").GetComponent<Image>();
			_backToLandLoseText = GameObject.Find("BackToLandLose").GetComponent<Text>();
			_backToLandWinMenuImage = GameObject.Find("BackToLandWinMenu").GetComponent<Image>();
			_backToLandWinText = GameObject.Find("BackToLandWin").GetComponent<Text>();
            _backToLandWithSuperMenuImage = GameObject.Find("BackToLandWithSuperMenu").GetComponent<Image>();
			_backToLandWithSuperText = GameObject.Find("BackToLandWithSuper").GetComponent<Text>();
            _chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
			_continueButtonImage = GameObject.Find("ContinueButton").GetComponent<Image>();
			_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            _muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
			_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            _unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    _muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    _muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    _unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;
                    _unmuteButtonImage.enabled = true;
                }
            }
		}

        Time.timeScale = 1;
    }

    void GoToFallingLevel()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("FallingDown");
    }

    void GoToLandLevel()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("LandRunner");
    }

    void GoToWaterLevel()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("WaterSwimmer");
    }

    void MuteUnmute()
    {
        if(MusicManager.m_musicSource != null)
        {
            if(_muteButtonImage.enabled)
            {
                _muteButtonImage.enabled = false;
                MusicManager.m_musicSource.Pause();
                m_playerMutedSounds = 1;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                _unmuteButtonImage.enabled = true;
                _soundManager.m_soundsSource.enabled = false;
            }

            else if(!_muteButtonImage.enabled)
            {
                _muteButtonImage.enabled = true;
                MusicManager.m_musicSource.Play();
                m_playerMutedSounds = 0;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                _unmuteButtonImage.enabled = false;
                _soundManager.m_soundsSource.enabled = true;
            }
        }
    }

    void OK()
    {
        _fbShareSuccessMenuObj.SetActive(false);
    }

    void Pause()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(MusicManager.m_musicSource.isPlaying)
            {
                MusicManager.m_musicSource.Pause();
                _muteButtonImage.enabled = false;
            }
            else
            {
                _unmuteButtonImage.enabled = false;
            }
        }

        _chimpionshipBeltImage.enabled = false;
        
        if(_exitButtonImage != null)
        {
            _exitButtonImage.enabled = true;
        }

		_highScoreDisplayText.enabled = false;
		_highScoreValueText.enabled = false;
        
        if(_restartButtonImage != null)
        {
            _restartButtonImage.enabled = true;
        }
        
		_pauseButtonImage.enabled = false;
		_pauseMenuImage.enabled = true;
		_resumeButtonImage.enabled = true;

		if(m_selfiePanelImage != null)
		{
			m_selfieButtonImage.enabled = false;	
		}

		Time.timeScale = 0;
	}

	void Play()
	{
		SceneManager.LoadScene("LandRunner");
	}

	void Quit()
	{
        _facebookButtonImage.enabled = false;
        _fbInviteButtonImage.enabled = false;
        _noInternetText.enabled = false;
        _playButtonImage.enabled = false;
        _profilePicImage.enabled = false;
		_quitButtonImage.enabled = false;
        _usernameText.enabled = false;

		_quitMenuImage.enabled = true;
		_quitAcceptButtonImage.enabled = true;
		_quitCancelButtonImage.enabled = true;
		_quitText.enabled = true;
	}

	void QuitAccept()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	void QuitCancel()
	{
        _facebookButtonImage.enabled = true;
        _fbInviteButtonImage.enabled = true;
		_playButtonImage.enabled = true;

        if(_profilePicEnabled)
        {
            _profilePicImage.enabled = true;
        }
        
		_quitButtonImage.enabled = true;
        _usernameText.enabled = true;

		_quitMenuImage.enabled = false;
		_quitAcceptButtonImage.enabled = false;
		_quitCancelButtonImage.enabled = false;
		_quitText.enabled = false;
	}

	void Restart()
	{
		_exitButtonImage.enabled = false;
		_pauseMenuImage.enabled = false;
		_restartButtonImage.enabled = false;
		_resumeButtonImage.enabled = false;

		_restartMenuImage.enabled = true;
		_restartAcceptButtonImage.enabled = true;
		_restartCancelButtonImage.enabled = true;
		_restartText.enabled = true;
	}

	void RestartAccept()
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

    void RestartCancel()
	{
		_exitButtonImage.enabled = true;
		_pauseMenuImage.enabled = true;
		_restartButtonImage.enabled = true;
		_resumeButtonImage.enabled = true;

		_restartMenuImage.enabled = false;
		_restartAcceptButtonImage.enabled = false;
		_restartCancelButtonImage.enabled = false;
		_restartText.enabled = false;
	}

	void Resume()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(m_playerMutedSounds == 0)
            {
                MusicManager.m_musicSource.Play();
                _muteButtonImage.enabled = true;
            }

            else if(m_playerMutedSounds == 1)
            {
                _unmuteButtonImage.enabled = true;
            }
        }

        _chimpionshipBeltImage.enabled = true;

        if(_exitButtonImage != null)
        {
            _exitButtonImage.enabled = false;
        }

		_highScoreDisplayText.enabled = true;
		_highScoreValueText.enabled = true;
		_pauseButtonImage.enabled = true;
		_pauseMenuImage.enabled = false;

        if(_restartButtonImage != null)
        {
            _restartButtonImage.enabled = false;
        }

        _resumeButtonImage.enabled = false;

		Time.timeScale = 1;
	}

	void SelfieClicked()
	{
		_soundManager.m_soundsSource.clip = _soundManager.m_selfie;
		
        if(_soundManager.m_soundsSource.enabled)
        {
            _soundManager.m_soundsSource.Play();
        }

		m_selfieButtonImage.enabled = false;

		if(_selfieFlashEnabled)
		{
			m_selfiePanelImage.enabled = true;
			Invoke("EndFlash" , 0.25f);
		}

		if(_landChimp.m_isSlipping)
        {
            ScoreManager.m_scoreValue += 60;
        }

        else if(_landChimp.m_isSuper) 
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
