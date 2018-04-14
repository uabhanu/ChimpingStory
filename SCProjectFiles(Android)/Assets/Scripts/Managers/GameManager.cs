using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	Achievement _selfieAchievement;
    AudioSource _musicSource;
	Image _adsAcceptButtonImage , _adsCancelButtonImage , _backToLandLoseMenuImage , _backToLandWinMenuImage , _backToLandWithSuperMenuImage , _continueButtonImage , _exitButtonImage;
    Image _pauseMenuImage , _playButtonImage , _quitButtonImage , _quitAcceptButtonImage , _quitCancelButtonImage , _quitMenuImage , _restartButtonImage , _restartAcceptButtonImage;
    Image _restartCancelButtonImage , _restartMenuImage , _resumeButtonImage;
    LandChimp _landChimp;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;
	Text _adsText , _backToLandLoseText , _backToLandWinText , _backToLandWithSuperText , _quitText , _restartText;

	[SerializeField] bool _bSelfieFlashEnabled , _bVersionCodeDisplayEnabled;
    [SerializeField] Image _chimpionshipBeltMenuImage , _chimpionshipOKButtonImage , _fallingLevelImage , _landLevelImage , _waterLevelImage;
    [SerializeField] Sprite[] _chimpionshipBeltSprites;
    [SerializeField] string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID;
    [SerializeField] Text _chimpionshipBeltText , _memoryLeakTestText , _versionCodeText;

    public static bool b_isFirstTimeUIButtonsTutorialTestingMode , b_isMemoryLeakTestingMode , b_isUnityEditorTestingMode, b_quitButtonTapped;
    public static Button m_chimpionshipBeltButton , m_muteButton , m_pauseButton , m_unmuteButton;
    public static GameObject m_pauseMenuObj , m_uiButtonsTutorialMenuObj;
    public static Image m_adsMenuImage , m_arrow01Image , m_arrow02Image , m_arrow03Image , m_arrow04Image , m_chimpionshipBeltButtonImage , m_muteButtonImage , m_nextButtonImage , m_pauseButtonImage;
    public static Image m_selfieButtonImage , m_selfiePanelImage , m_uiButtonsTutorialMenuImage , m_unmuteButtonImage;
    public static int m_currentScene , m_firstTimeUIButtonsTutorial , m_playerMutedSounds;
    public static Text m_chimpionshipBeltButtonTutorialText , m_highScoreDisplayText , m_highScoreValueText , m_leaderboardButtonTutorialText , m_muteUnmuteButtonTutorialText , m_pauseButtonTutorialText;

    void Start()
	{
        b_isFirstTimeUIButtonsTutorialTestingMode = true; //TODO Remove this for Live Version
        //b_isMemoryLeakTestingMode = true; //TODO Remove this for Live Version
        GetBhanuObjects();
    }

    public void Ads()
    {
        m_adsMenuImage.enabled = true;
        _adsAcceptButtonImage.enabled = true;
        _adsCancelButtonImage.enabled = true;
        _adsText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
        
        if(SocialmediaManager.b_isGPGsLeaderboardTestMode)
        {
            _socialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
        }

        m_highScoreDisplayText.enabled = false;
        m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        SocialmediaManager.m_gpgsLeaderboardButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAcceptButton()
    {
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
        m_adsMenuImage.enabled = false;
        _adsAcceptButtonImage.enabled = false;
        _adsCancelButtonImage.enabled = false;
        _adsText.enabled = false;
        
        if(SocialmediaManager.b_isGPGsLogInTestMode)
        {
            _socialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
        }

        AdsShow();
    }

    public void AdsCancelButton()
    {
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
        BhanuPrefs.DeleteScore();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    void AdResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            //Debug.Log("Video completed - Offer a reward to the player");
            _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
            ScoreManager.m_scoreValue *= 0.25f;
		    ScoreManager.m_scoreValue = Mathf.Round(ScoreManager.m_scoreValue);
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(m_currentScene);
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
            BhanuPrefs.DeleteScore();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
            BhanuPrefs.DeleteScore();
        }
    }

    void AdsShow()
    {
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdResult;
        Advertisement.Show("rewardedVideo" , options);
    }

    public void BackToLandLoseMenu()
    {
        _backToLandLoseMenuImage.enabled = true;
        _backToLandLoseText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		_continueButtonImage.enabled = true;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(false);
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWinMenu()
    {
        _backToLandWinMenuImage.enabled = true;
        _backToLandWinText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		_continueButtonImage.enabled = true;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(false);
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWithSuperMenu()
    {
        _backToLandWithSuperMenuImage.enabled = true;
        _backToLandWithSuperText.enabled = true;
        _continueButtonImage.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		m_highScoreDisplayText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(false);
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void ChimpionshipBelt()
    {
        m_chimpionshipBeltButtonImage = GameObject.Find("ChimpionshipBeltButton").GetComponent<Image>();
        
        if(LandChimp.IsChimpion())
        {
            m_chimpionshipBeltButtonImage.sprite = _chimpionshipBeltSprites[1];
            _socialmediaManager.GooglePlayGamesAchievements(_chimpionAchievementID);
        }
        else
        {
            m_chimpionshipBeltButtonImage.sprite = _chimpionshipBeltSprites[0];
        }   
    }

    public void ChimpionBeltButton()
    {
        m_chimpionshipBeltButtonImage.enabled = false;
        _chimpionshipBeltMenuImage.enabled = true;
        _chimpionshipBeltText.enabled = true;
        _chimpionshipOKButtonImage.enabled = true;
        SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(false);
        m_highScoreDisplayText.enabled = false;
        m_highScoreValueText.enabled = false;
        Time.timeScale = 0;
    }

    public void ChimpionshipBeltOKButton()
    {
        m_chimpionshipBeltButtonImage.enabled = true;
        _chimpionshipBeltMenuImage.enabled = false;
        _chimpionshipBeltText.enabled = false;
        _chimpionshipOKButtonImage.enabled = false;
        SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(true);
        m_highScoreDisplayText.enabled = true;
        m_highScoreValueText.enabled = true;
        Time.timeScale = 1;
    }

    public void ContinueButton()
    {
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
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

    void GetAchievement()
    {
        _selfieAchievement = PlayGamesPlatform.Instance.GetAchievement(_selfieAchievementID);

        if(_selfieAchievement == null)
        {
            Invoke("GetAchievement" , 0.5f);
        }
    }

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;

        if(!b_isFirstTimeUIButtonsTutorialTestingMode)
        {
            m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();
        }
        else
        {
            m_playerMutedSounds = 0;
        }
        
        _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();

        if(m_currentScene == 0)
        {
            _playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            _quitText = GameObject.Find("QuitText").GetComponent<Text>();
            _quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            _quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            _quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            _quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();

            if(_bVersionCodeDisplayEnabled)
            {
                _versionCodeText.enabled = true;
            }
        }

        else if(m_currentScene == 1)
        {
            _adsText = GameObject.Find("AdsText").GetComponent<Text>();
            _adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            _adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            m_adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            m_arrow01Image = GameObject.Find("Arrow01").GetComponent<Image>();
            m_arrow02Image = GameObject.Find("Arrow02").GetComponent<Image>();
            m_arrow03Image = GameObject.Find("Arrow03").GetComponent<Image>();
            m_arrow04Image = GameObject.Find("Arrow04").GetComponent<Image>();
            #region Better Code for getting Arrows
            //for(int i = 0; i < m_arrowImages.Length; ++i)
            //{
            //    m_arrowImages[i] = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Image>();
            //}
            #endregion
            m_chimpionshipBeltButton = GameObject.Find("ChimpionshipBeltButton").GetComponent<Button>();
            m_chimpionshipBeltButtonImage = GameObject.Find("ChimpionshipBeltButton").GetComponent<Image>();
            m_chimpionshipBeltButtonTutorialText = GameObject.Find("ChimpionBeltButtonTutorialText").GetComponent<Text>();
            _exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();

            if(!b_isFirstTimeUIButtonsTutorialTestingMode)
            {
                m_firstTimeUIButtonsTutorial = BhanuPrefs.GetFirstTimeUIButtonsTutorialStatus();
            }
            else
            {
                m_firstTimeUIButtonsTutorial = 0;
            }
            
            _landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
            m_leaderboardButtonTutorialText = GameObject.Find("LeaderboardButtonTutorialText").GetComponent<Text>();
            m_muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
            m_muteUnmuteButtonTutorialText = GameObject.Find("MuteButtonTutorialText").GetComponent<Text>();
            m_nextButtonImage = GameObject.Find("NextButton").GetComponent<Image>();
            m_pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
            m_pauseButtonTutorialText = GameObject.Find("PauseButtonTutorialText").GetComponent<Text>();
            m_pauseMenuObj = GameObject.Find("PauseMenu");
			_pauseMenuImage = m_pauseMenuObj.GetComponent<Image>();
            _restartText = GameObject.Find("RestartText").GetComponent<Text>();
			_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            _restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
            _restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
            _restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
            _restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
			m_selfieButtonImage = GameObject.Find("SelfieButton").GetComponent<Image>();
			m_selfiePanelImage = GameObject.Find("SelfiePanel").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_uiButtonsTutorialMenuObj = GameObject.Find("UIButtonsTutorialMenu");
            m_uiButtonsTutorialMenuImage = m_uiButtonsTutorialMenuObj.GetComponent<Image>();
            m_unmuteButton = GameObject.Find("UnmuteButton").GetComponent<Button>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(m_firstTimeUIButtonsTutorial == 0)
            {
                m_arrow01Image.enabled = true;
                m_chimpionshipBeltButton.interactable = false;
                m_leaderboardButtonTutorialText.enabled = true;
                m_muteButton.interactable = false;
                m_pauseButton.interactable = false;
                m_nextButtonImage.enabled = true;
                m_uiButtonsTutorialMenuImage.enabled = true;
                m_unmuteButton.interactable = false;
                Time.timeScale = 0;
            }

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;

                    if(m_firstTimeUIButtonsTutorial == 1)
                    {
                        m_unmuteButtonImage.enabled = true;
                    }
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
			m_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuObj = GameObject.Find("PauseMenu");
			_pauseMenuImage = m_pauseMenuObj.GetComponent<Image>();
			_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }
            }
		}

        //if(SocialmediaManager.m_isFacebookShareTestMode && SocialmediaManager.m_facebookShareTestMenuObj != null)
        //{
        //    SocialmediaManager.m_facebookShareTestMenuObj.SetActive(true);
        //}

        if(SocialmediaManager.b_isGPGsLeaderboardTestMode)
        {
            _socialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
            ScoreManager.m_minHighScore = 5f;
        }
        else
        {
            ScoreManager.m_minHighScore = 5000f;
        }

        if(SocialmediaManager.b_isGPGsLogInTestMode && SocialmediaManager.m_gpgsLogInTestText != null)
        {
            SocialmediaManager.m_gpgsLogInTestText.enabled = true;
        }

        if(m_currentScene > 0)
        {
            m_highScoreDisplayText = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueText = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();

            if(SocialmediaManager.b_isGPGsAchievementsTestMode)
            {
                SocialmediaManager.m_gpgsAchievementsTestText.enabled = true;
            }
        }

        if(b_isMemoryLeakTestingMode)
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

        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
        Invoke("GetAchievement" , 0.5f);
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

    public void MuteUnmuteButton()
    {
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

        if(MusicManager.m_musicSource != null)
        {
            if(m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = false;
                MusicManager.m_musicSource.Pause();
                m_playerMutedSounds = 1;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                m_unmuteButtonImage.enabled = true;
                _soundManager.m_soundsSource.enabled = false;
            }

            else if(!m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = true;
                MusicManager.m_musicSource.Play();
                m_playerMutedSounds = 0;
                BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                m_unmuteButtonImage.enabled = false;
                _soundManager.m_soundsSource.enabled = true;
            }
        }
    }

    public void NextButton()
    {
        if(m_arrow01Image.enabled)
        {
            m_arrow01Image.enabled = false;
            m_arrow02Image.enabled = true;
            m_leaderboardButtonTutorialText.enabled = false;
            m_pauseButton.interactable = true;
            m_pauseButtonImage.enabled = true;
            m_pauseButtonTutorialText.enabled = true;
        }

        else if(m_arrow02Image.enabled)
        {
            m_arrow02Image.enabled = false;
            m_arrow03Image.enabled = true;

            if(m_playerMutedSounds == 0)
            {
                m_muteButton.interactable = true;
                m_muteButtonImage.enabled = true;
            }
            else
            {
                m_unmuteButton.interactable = true;
                m_unmuteButtonImage.enabled = true;
            }
            
            m_muteUnmuteButtonTutorialText.enabled = true;
            m_pauseButtonTutorialText.enabled = false;
        }

        else if(m_arrow03Image.enabled)
        {
            m_arrow03Image.enabled = false;
            m_arrow04Image.enabled = true;
            m_chimpionshipBeltButton.interactable = true;
            m_chimpionshipBeltButtonImage.enabled = true;
            m_chimpionshipBeltButtonTutorialText.enabled = true;
            m_muteUnmuteButtonTutorialText.enabled = false;
        }

        else
        {
            m_arrow01Image.enabled = false;
            m_arrow02Image.enabled = false;
            m_arrow03Image.enabled = false;
            m_arrow04Image.enabled = false;
            m_chimpionshipBeltButtonTutorialText.enabled = false;
            m_leaderboardButtonTutorialText.enabled = false;
            m_muteUnmuteButtonTutorialText.enabled = false;
            m_nextButtonImage.enabled = false;
            m_pauseButtonTutorialText.enabled = false;
            m_uiButtonsTutorialMenuImage.enabled = false;
            Time.timeScale = 1;

            if(!b_isFirstTimeUIButtonsTutorialTestingMode)
            {
                m_firstTimeUIButtonsTutorial = 1;
                BhanuPrefs.SetFirstTimeUIButtonsTutorialStatus(m_firstTimeUIButtonsTutorial);
            }
        }
    }

    public void PauseButton()
	{
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

        if(m_firstTimeUIButtonsTutorial == 0)
        {
            _exitButtonImage.enabled = true;
            m_highScoreDisplayText.enabled = false;
            m_highScoreValueText.enabled = false;
            _pauseMenuImage.enabled = true;
            _restartButtonImage.enabled = true;
            _resumeButtonImage.enabled = true;
            m_uiButtonsTutorialMenuObj.SetActive(false);
        }
        else
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

            if(m_chimpionshipBeltButtonImage != null)
            {
                m_chimpionshipBeltButtonImage.enabled = false;
            }
        
            if(_exitButtonImage != null)
            {
                _exitButtonImage.enabled = true;
            }

            if(SocialmediaManager.b_isGPGsLeaderboardTestMode)
            {
                _socialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
            }

		    m_highScoreDisplayText.enabled = false;
		    m_highScoreValueText.enabled = false;

            if(SocialmediaManager.m_gpgsAchievementsButtonObj != null)
            {
                SocialmediaManager.m_gpgsAchievementsButtonImage.enabled = true;
            }

            if(SocialmediaManager.m_gpgsLeaderboardButtonObj != null)
            {
                SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(false);
            }
        
            if(_restartButtonImage != null)
            {
                _restartButtonImage.enabled = true;
            }
        
		    m_pauseButtonImage.enabled = false;
		    _pauseMenuImage.enabled = true;
		    _resumeButtonImage.enabled = true;

		    if(m_selfiePanelImage != null)
		    {
			    m_selfieButtonImage.enabled = false;	
		    }

		    Time.timeScale = 0;
        }
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
        MusicManager.m_musicSource.Pause();
        b_quitButtonTapped = true;
        SocialmediaManager.m_gpgsLogInButtonImage.enabled = false;
        SocialmediaManager.m_gpgsProfilePicImage.enabled = false;
        SocialmediaManager.m_gpgsProfilePicMaskImage.enabled = false;
        SocialmediaManager.m_gpgsRateButtonImage.enabled = false;
        SocialmediaManager.m_gpgsUsernameText.enabled = false;
        
        if(SocialmediaManager.m_gpgsLogInTestText != null)
        {
            SocialmediaManager.m_gpgsLogInTestText.enabled = false;
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

        if(m_playerMutedSounds == 0)
        {
            MusicManager.m_musicSource.Play();
        }

        if(SocialmediaManager.b_gpgsLoggedIn)
        {
            SocialmediaManager.m_gpgsProfilePicImage.enabled = true;
            SocialmediaManager.m_gpgsProfilePicMaskImage.enabled = true;
        }

        b_quitButtonTapped = false;

        if(SocialmediaManager.b_gpgsLoggedIn)
        {
            _socialmediaManager.GooglePlayGamesLoggedIn();
        }
        else
        {
            _socialmediaManager.GooglePlayGamesLoggedOut();
        }
        
        if(SocialmediaManager.b_isGPGsLogInTestMode && SocialmediaManager.m_gpgsLogInTestText != null)
        {
            SocialmediaManager.m_gpgsLogInTestText.enabled = true;
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
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

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
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
        }
        
        BhanuPrefs.DeleteScore();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
		SceneManager.LoadScene(m_currentScene);
	}

    public void RestartCancelButton()
	{
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

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
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();

        if(m_firstTimeUIButtonsTutorial == 0)
        {
            _exitButtonImage.enabled = false;
            m_highScoreDisplayText.enabled = true;
            m_highScoreValueText.enabled = true;
            _pauseMenuImage.enabled = false;
            _restartButtonImage.enabled = false;
            _resumeButtonImage.enabled = false;
            m_uiButtonsTutorialMenuObj.SetActive(true);
        }
        else
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

            if(m_chimpionshipBeltButtonImage != null)
            {
                m_chimpionshipBeltButtonImage.enabled = true;
            }

            if(_exitButtonImage != null)
            {
                _exitButtonImage.enabled = false;
            }

            if(SocialmediaManager.b_isGPGsLeaderboardTestMode)
            {
                _socialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
            }

		    m_highScoreDisplayText.enabled = true;
		    m_highScoreValueText.enabled = true;

            if(SocialmediaManager.m_gpgsAchievementsButtonObj != null)
            {
                SocialmediaManager.m_gpgsAchievementsButtonImage.enabled = false;
            }

            if(SocialmediaManager.m_gpgsLeaderboardButtonObj != null)
            {
                SocialmediaManager.m_gpgsLeaderboardButtonObj.SetActive(true);
            }
        
		    m_pauseButtonImage.enabled = true;
		    _pauseMenuImage.enabled = false;

            if(_restartButtonImage != null)
            {
                _restartButtonImage.enabled = false;
            }

            _resumeButtonImage.enabled = false;

		    Time.timeScale = 1;
        }
	}

	public void SelfieButton()
	{
        _socialmediaManager.GooglePlayGamesAchievements(_selfieAchievementID);

        if(_selfieAchievement != null && _selfieAchievement.IsUnlocked)
        {
            _socialmediaManager.GooglePlayGamesIncrementalAchievements(_selfieLegendAchievementID , 1);
        }
		_soundManager.m_soundsSource.clip = _soundManager.m_selfie;
		
        if(_soundManager.m_soundsSource.enabled)
        {
            _soundManager.m_soundsSource.Play();
        }

		m_selfieButtonImage.enabled = false;

		if(_bSelfieFlashEnabled)
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
