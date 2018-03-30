using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource _musicSource;
    const float _chimpionshipBeltInvokeTime = 0.5f;
	Image _adsAcceptButtonImage , _adsCancelButtonImage , _backToLandLoseMenuImage , _backToLandWinMenuImage , _backToLandWithSuperMenuImage , _chimpionshipBeltImage , _continueButtonImage , _exitButtonImage;
    Image _muteButtonImage , _pauseMenuImage , _playButtonImage , _quitButtonImage , _quitAcceptButtonImage , _quitCancelButtonImage , _quitMenuImage , _restartButtonImage , _restartAcceptButtonImage;
    Image _restartCancelButtonImage , _restartMenuImage , _resumeButtonImage , _unmuteButtonImage;
    LandChimp _landChimp;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;
	Text _adsText , _backToLandLoseText , _backToLandWinText , _backToLandWithSuperText , _highScoreDisplayText , _highScoreValueText , _quitText , _restartText;

	[SerializeField] bool _selfieFlashEnabled;
    [SerializeField] Image _fallingLevelImage , _landLevelImage , _waterLevelImage;
    [SerializeField] Sprite[] _chimpionshipBeltSprites;
    [SerializeField] Text _memoryLeakTestText;

    public static bool m_isMemoryLeakTestingMode , m_isTestingUnityEditor , m_quitButtonTapped;
    public static Image _adsMenuImage , _pauseButtonImage , m_selfieButtonImage , m_selfiePanelImage;
    public static int m_currentScene , m_playerMutedSounds;

    void Start()
	{
        //m_isMemoryLeakTestingMode = true; //TODO Remove this for Live Version
        GetBhanuObjects();
    }

    public void Ads()
    {
        _adsMenuImage.enabled = true;
        _adsAcceptButtonImage.enabled = true;
        _adsCancelButtonImage.enabled = true;
        _adsText.enabled = true;
        _chimpionshipBeltImage.enabled = false;
        
        if(SocialmediaManager.m_isGooglePlayGamesLeaderboardTestMode)
        {
            SocialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
        }

        _muteButtonImage.enabled = false;
        _pauseButtonImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        _unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAcceptButton()
    {
        _adsMenuImage.enabled = false;
        _adsAcceptButtonImage.enabled = false;
        _adsCancelButtonImage.enabled = false;
        _adsText.enabled = false;
        
        if(SocialmediaManager.m_isGooglePlayGamesLogInTestMode)
        {
            SocialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
        }

        AdsShow();
    }

    public void AdsCancelButton()
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

    void ChimpionshipBelt()
    {
        if(IsChimpion())
        {
            _chimpionshipBeltImage.sprite = _chimpionshipBeltSprites[1];
        }
        else
        {
            _chimpionshipBeltImage.sprite = _chimpionshipBeltSprites[0];
        }

        Invoke("ChimpionshipBelt" , _chimpionshipBeltInvokeTime);
    }

    public void ContinueButton()
    {
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene("LandRunner");
    }

    void EndFlash()
	{
		m_selfiePanelImage.enabled = false;
	}

    public void ExitButton()
	{
        SceneManager.LoadScene("MainMenu");
    }

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;
        m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();

        if(m_currentScene == 0)
        {
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
            _exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
            _landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
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
            _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();
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

        //if(SocialmediaManager.m_isFacebookShareTestMode && SocialmediaManager.m_facebookShareTestMenuObj != null)
        //{
        //    SocialmediaManager.m_facebookShareTestMenuObj.SetActive(true);
        //}

        if(SocialmediaManager.m_isGooglePlayGamesLeaderboardTestMode)
        {
            SocialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
            ScoreManager.m_minHighScore = 5f;
        }
        else
        {
            ScoreManager.m_minHighScore = 5000f;
        }

        if(ScoreManager.m_scoreValue >= ScoreManager.m_minHighScore && SocialmediaManager.m_googlePlayGamesLeaderboardButton != null)
        {
            SocialmediaManager.m_googlePlayGamesLeaderboardButton.interactable = true;
        }

        if(SocialmediaManager.m_isGooglePlayGamesLogInTestMode && SocialmediaManager.m_googlePlayGamesLogInTestText != null)
        {
            SocialmediaManager.m_googlePlayGamesLogInTestText.enabled = true;
        }

        if(m_currentScene > 0)
        {
            _chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
            _highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            Invoke("ChimpionshipBelt" , _chimpionshipBeltInvokeTime);
        }

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

        Time.timeScale = 1;
    }

    public void GoToFallingLevelButton()
    {
        SceneManager.LoadScene("FallingDown");
    }

    public void GoToLandLevelButton()
    {
        SceneManager.LoadScene("LandRunner");
    }

    public void GoToWaterLevelButton()
    {
        SceneManager.LoadScene("WaterSwimmer");
    }

    bool IsChimpion()
    {
        if(_socialmediaManager.m_playerRank == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MuteUnmuteButton()
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

    public void PauseButton()
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

        if(_chimpionshipBeltImage != null)
        {
            _chimpionshipBeltImage.enabled = false;
        }
        
        if(_exitButtonImage != null)
        {
            _exitButtonImage.enabled = true;
        }

        if(SocialmediaManager.m_isGooglePlayGamesLeaderboardTestMode)
        {
            SocialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
        }

		_highScoreDisplayText.enabled = false;
		_highScoreValueText.enabled = false;

        if(SocialmediaManager.m_googlePlayGamesLeaderboardButtonObj != null)
        {
            SocialmediaManager.m_googlePlayGamesLeaderboardButtonObj.SetActive(false);
        }
        
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

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        //SocialmediaManager.m_facebookButtonImage.enabled = false;
        //SocialmediaManager.m_facebookProfilePicImage.enabled = false;
        //SocialmediaManager.m_facebookUsernameText.enabled = false;
        
        //if(SocialmediaManager.m_facebookShareTestMenuObj != null)
        //{
        //    SocialmediaManager.m_facebookShareTestMenuObj.SetActive(false);
        //}

        m_quitButtonTapped = true;

        SocialmediaManager.m_googlePlayGamesLogInButtonImage.enabled = false;
        SocialmediaManager.m_googlePlayGamesProfilePicImage.enabled = false;
        SocialmediaManager.m_googlePlayRateButtonImage.enabled = false;
        SocialmediaManager.m_googlePlayGamesUsernameText.enabled = false;
        
        if(SocialmediaManager.m_googlePlayGamesLogInTestText != null)
        {
            SocialmediaManager.m_googlePlayGamesLogInTestText.enabled = false;
        }

        if(SocialmediaManager.m_noInternetText != null)
        {
            SocialmediaManager.m_noInternetText.enabled = false;
        }
        
        _playButtonImage.enabled = false;
		_quitButtonImage.enabled = false;
		_quitMenuImage.enabled = true;
		_quitAcceptButtonImage.enabled = true;
		_quitCancelButtonImage.enabled = true;
		_quitText.enabled = true;
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
        //if(SocialmediaManager.m_facebookProfilePicExists)
        //{
        //    SocialmediaManager.m_facebookProfilePicImage.enabled = true;
        //    SocialmediaManager.m_facebookUsernameText.enabled = true;
        //}
        //else
        //{
        //    SocialmediaManager.m_facebookButtonImage.enabled = true;
        //}
        
        //if(SocialmediaManager.m_isFacebookShareTestMode && SocialmediaManager.m_facebookShareTestMenuObj != null)
        //{
        //    SocialmediaManager.m_facebookShareTestMenuObj.SetActive(true);
        //}

        m_quitButtonTapped = false;

        if(SocialmediaManager.m_googlePlayGamesLoggedIn)
        {
            _socialmediaManager.GooglePlayGamesLoggedIn();
        }
        else
        {
            _socialmediaManager.GooglePlayGamesLoggedOut();
        }
        
        if(SocialmediaManager.m_isGooglePlayGamesLogInTestMode && SocialmediaManager.m_googlePlayGamesLogInTestText != null)
        {
            SocialmediaManager.m_googlePlayGamesLogInTestText.enabled = true;
        }

		_playButtonImage.enabled = true;
		_quitButtonImage.enabled = true;
		_quitMenuImage.enabled = false;
		_quitAcceptButtonImage.enabled = false;
		_quitCancelButtonImage.enabled = false;
		_quitText.enabled = false;
	}

	public void RestartButton()
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

	public void RestartAcceptButton()
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

    public void RestartCancelButton()
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

	public void ResumeButton()
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

        if(_chimpionshipBeltImage != null)
        {
            _chimpionshipBeltImage.enabled = true;
        }

        if(_exitButtonImage != null)
        {
            _exitButtonImage.enabled = false;
        }

        if(SocialmediaManager.m_isGooglePlayGamesLeaderboardTestMode)
        {
            SocialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
        }

		_highScoreDisplayText.enabled = true;
		_highScoreValueText.enabled = true;

        if(SocialmediaManager.m_googlePlayGamesLeaderboardButtonObj != null)
        {
            SocialmediaManager.m_googlePlayGamesLeaderboardButtonObj.SetActive(true);
        }
        
		_pauseButtonImage.enabled = true;
		_pauseMenuImage.enabled = false;

        if(_restartButtonImage != null)
        {
            _restartButtonImage.enabled = false;
        }

        _resumeButtonImage.enabled = false;

		Time.timeScale = 1;
	}

	public void SelfieButton()
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
