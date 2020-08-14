using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandLevelManager : MonoBehaviour 
{
    //TODO Portals should be visible every 24 hours
    //TODO Super Visible only after it's collected from the Water Level

    private AudioSource _musicSource;
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use
    private LandPuss _landPuss;
	private SoundManager _soundManager;
	private Text _firstTimePlayTutorialText, _quitText;

    private static Animator _swipeDownHandAnimator , _swipeUpHandAnimator;
    private static Image _swipeDownHandImage , _swipeUpHandImage , _swipeHandOKButtonImage , _swipeHandPanelImage;
    private static int _firstTimeJump = 0 , _firstTimeSlide = 0;

	[SerializeField] private bool _bSelfieFlashEnabled;
    [SerializeField] private GameObject _adsMenuObj , _iapCartMenuObj , _inGameUIObj , _pauseMenuObj , _selfieButtonObj , _selfiePanelObj;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public static bool b_isFirstTimeTutorialTestingMode , b_isMemoryLeakTestingMode , b_isUnityEditorTestingMode , b_quitButtonTapped;
    public static Button m_chimpionshipBeltButton , m_muteButton , m_pauseButton , m_unmuteButton;
    public static GameObject m_pauseMenuObj , m_uiButtonsTutorialMenuObj;
    public static Image m_arrow01Image , m_arrow02Image , m_arrow03Image , m_arrow04Image , m_chimpionshipBeltButtonImage , m_muteButtonImage , m_nextButtonImage , m_pauseButtonImage;
    public static Image m_uiButtonsTutorialMenuImage , m_unmuteButtonImage;
    public static int m_currentScene , m_firstTimeIAPTutorialAppeared , m_firstTimeUIButtonsTutorial , m_firstTimeWaterLevelTutorial , m_playerMutedSounds;
    public static Text m_chimpionshipBeltButtonTutorialText , m_leaderboardButtonTutorialText , m_muteUnmuteButtonTutorialText , m_pauseButtonTutorialText;

    public Text m_HighScoreLabelText , m_HighScoreValueText , m_PolaroidsCountText;

    void Start()
	{
        //b_isFirstTimeTutorialTestingMode = true; //This is for testing only
        //b_isMemoryLeakTestingMode = true; //This is for testing only
        //b_isUnityEditorTestingMode = true; //This is for testing only
        Advertisement.Initialize("3696337");
        GetBhanuObjects();
    }

    public void Ads()
    {
        _adsMenuObj.SetActive(true);
        _inGameUIObj.SetActive(false);
        m_chimpionshipBeltButtonImage.enabled = false;
        m_HighScoreLabelText.enabled = false;
        m_HighScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_PolaroidsCountText.enabled = false;
		_selfieButtonObj.SetActive(false);
        m_unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAcceptButton()
    {
        _adsMenuObj.SetActive(false);
        AdsShow();
    }

    public void AdsCancelButton()
    {
        BhanuPrefs.DeleteScore();
        ScoreManager.m_supersCount = 0;
        BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    void AdsShow()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdsWatchResult;
        Advertisement.Show("rewardedVideo" , options);
    }

    void AdsWatchResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(m_currentScene);
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            BhanuPrefs.DeleteScore();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            BhanuPrefs.DeleteScore();
        }
    }

    public void ContinueButton()
    {
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene("LandRunner");
    }

    void EndFlash()
	{
		_selfiePanelObj.SetActive(false);
	}

    public void ExitButton()
	{
        BhanuPrefs.DeleteScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void FirstTimeJumpTutorial()
    {
        if(_firstTimeJump == 0)
        {
            m_chimpionshipBeltButtonImage.enabled = false;
            m_HighScoreLabelText.enabled = false;
            m_HighScoreValueText.enabled = false;
            m_muteButtonImage.enabled = false;
            m_pauseButtonImage.enabled = false;
            _swipeUpHandAnimator.enabled = true;
            _swipeUpHandImage.enabled = true;
            _swipeHandOKButtonImage.enabled = true;
            _swipeHandPanelImage.enabled = true;
            m_unmuteButtonImage.enabled = false;
            _firstTimeJump++;
            BhanuPrefs.SetFirstTimeJumpTutorialStatus(_firstTimeJump);
            Time.timeScale = 0;
        }
    }

    public void FirstTimeSlideTutorial()
    {
        if(_firstTimeSlide == 0)
        {
            m_chimpionshipBeltButtonImage.enabled = false;
            m_HighScoreLabelText.enabled = false;
            m_HighScoreValueText.enabled = false;
            m_muteButtonImage.enabled = false;
            m_pauseButtonImage.enabled = false;
            _swipeDownHandAnimator.enabled = true;
            _swipeDownHandImage.enabled = true;
            _swipeHandOKButtonImage.enabled = true;
            _swipeHandPanelImage.enabled = true;
            m_unmuteButtonImage.enabled = false;
            _firstTimeSlide++;
            BhanuPrefs.SetFirstTimeSlideTutorialStatus(_firstTimeSlide);
            Time.timeScale = 0;
        }
    }

    public void FirstTimeWaterLevelOKButton()
    {
        m_chimpionshipBeltButtonImage.enabled = true;
        _firstTimePlayTutorialText.enabled = false;
        m_HighScoreLabelText.enabled = true;
        m_HighScoreValueText.enabled = true;
        m_pauseButtonImage.enabled = true;
        m_PolaroidsCountText.enabled = true;
        m_firstTimeWaterLevelTutorial = 1;
        BhanuPrefs.SetFirstTimeWaterLevelTutorialStatus(m_firstTimeWaterLevelTutorial);
        Time.timeScale = 1;
    }

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;
        m_muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
        m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
        m_unmuteButton = GameObject.Find("UnmuteButton").GetComponent<Button>();
        m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

        if(!b_isFirstTimeTutorialTestingMode)
        {
            //_chimpionshipsCount = BhanuPrefs.GetChimpionshipsCount();
            m_firstTimeIAPTutorialAppeared = BhanuPrefs.GetFirstTimeIAPTutorialStatus();
            _firstTimeJump = BhanuPrefs.GetFirstTimeJumpTutorialStatus();
            _firstTimeSlide = BhanuPrefs.GetFirstTimeSlideTutorialStatus();
            m_firstTimeUIButtonsTutorial = BhanuPrefs.GetFirstTimeUIButtonsTutorialStatus();
            m_firstTimeWaterLevelTutorial = BhanuPrefs.GetFirstTimeWaterLevelTutorialStatus();
            m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();
            Time.timeScale = 1;
        }
        else
        {
            BhanuPrefs.DeleteAll();
        }

        m_arrow01Image = GameObject.Find("Arrow01").GetComponent<Image>();
        m_arrow02Image = GameObject.Find("Arrow02").GetComponent<Image>();
        m_arrow03Image = GameObject.Find("Arrow03").GetComponent<Image>();
        m_arrow04Image = GameObject.Find("Arrow04").GetComponent<Image>();
        m_chimpionshipBeltButton = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Button>();
        m_chimpionshipBeltButtonImage = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Image>();
        m_chimpionshipBeltButtonTutorialText = GameObject.Find("ChimpionBeltButtonTutorialText").GetComponent<Text>();
        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_leaderboardButtonTutorialText = GameObject.Find("LeaderboardButtonTutorialText").GetComponent<Text>();
        m_muteUnmuteButtonTutorialText = GameObject.Find("MuteButtonTutorialText").GetComponent<Text>();
        m_nextButtonImage = GameObject.Find("NextButton").GetComponent<Image>();
        m_pauseButton = GameObject.Find("PF_PauseButton").GetComponent<Button>();
        m_pauseButtonImage = GameObject.Find("PF_PauseButton").GetComponent<Image>();
        m_pauseButtonTutorialText = GameObject.Find("PauseButtonTutorialText").GetComponent<Text>();
        m_PolaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _swipeDownHandAnimator = GameObject.Find("SwipeDownHand").GetComponent<Animator>();
        _swipeUpHandAnimator = GameObject.Find("SwipeUpHand").GetComponent<Animator>();
        _swipeDownHandImage = GameObject.Find("SwipeDownHand").GetComponent<Image>();
        _swipeUpHandImage = GameObject.Find("SwipeUpHand").GetComponent<Image>();
        _swipeHandOKButtonImage = GameObject.Find("SwipeHandOKButton").GetComponent<Image>();
        _swipeHandPanelImage = GameObject.Find("SwipeHandPanel").GetComponent<Image>();
        m_uiButtonsTutorialMenuObj = GameObject.Find("UIButtonsTutorialMenu");
        m_uiButtonsTutorialMenuImage = m_uiButtonsTutorialMenuObj.GetComponent<Image>();

        if(m_firstTimeUIButtonsTutorial == 0)
        {
            m_arrow01Image.enabled = true;
            m_chimpionshipBeltButton.interactable = false;
            m_leaderboardButtonTutorialText.enabled = true;
            m_muteButton.interactable = false;
            m_nextButtonImage.enabled = true;
            m_pauseButton.interactable = false;
            m_PolaroidsCountText.enabled = false;
            m_uiButtonsTutorialMenuImage.enabled = true;
            m_unmuteButton.interactable = false;
            Time.timeScale = 0;
        }

        if(MusicManager.m_musicSource != null && _soundManager.m_soundsSource != null)
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

    public void IAPCartButton()
    {
        _iapCartMenuObj.SetActive(true);
    }

    public void IAPCancelButton()
    {
        _iapCartMenuObj.SetActive(false);
    }

    public void IAPNoButton()
    {
        BhanuPrefs.DeleteScore();
        ScoreManager.m_supersCount = 0;
        BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    public void IAPYesButton()
    {
        _iapCartMenuObj.SetActive(true);
    }

    public void MuteUnmuteButton()
    {
        if(m_firstTimeUIButtonsTutorial == 1 && m_currentScene > 0)
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

        else if(m_currentScene == 0)
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
                }

                else if(!m_muteButtonImage.enabled)
                {
                    m_muteButtonImage.enabled = true;
                    MusicManager.m_musicSource.Play();
                    m_playerMutedSounds = 0;
                    BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                    m_unmuteButtonImage.enabled = false;
                }
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
                m_unmuteButton.interactable = true;
            }
            else
            {
                m_muteButton.interactable = true;
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
            m_firstTimeUIButtonsTutorial = 1;
            m_leaderboardButtonTutorialText.enabled = false;
            m_muteUnmuteButtonTutorialText.enabled = false;
            m_nextButtonImage.enabled = false;
            m_pauseButtonTutorialText.enabled = false;
            m_uiButtonsTutorialMenuImage.enabled = false;
            m_PolaroidsCountText.enabled = true;
            Time.timeScale = 1;

            if(!b_isFirstTimeTutorialTestingMode)
            {
                BhanuPrefs.SetFirstTimeUIButtonsTutorialStatus(m_firstTimeUIButtonsTutorial);
            }
        }
    }

    public void PauseButton()
	{
        if(m_firstTimeUIButtonsTutorial == 1)
        {
            if(MusicManager.m_musicSource != null)
            {
                MusicManager.m_musicSource.Pause();
            }

            m_muteButtonImage.enabled = false;
            m_unmuteButtonImage.enabled = false;

            if(m_chimpionshipBeltButtonImage != null)
            {
                m_chimpionshipBeltButtonImage.enabled = false;
            }

            _inGameUIObj.SetActive(false);
            _pauseMenuObj.SetActive(true);
			_selfieButtonObj.SetActive(false);
		    
		    Time.timeScale = 0;
        }
	}

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        MusicManager.m_musicSource.Pause();
        m_muteButtonImage.enabled = false;
        b_quitButtonTapped = true;
		_quitText.enabled = true;
        m_unmuteButtonImage.enabled = false;
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
        if(m_playerMutedSounds == 0)
        {
            MusicManager.m_musicSource.Play();
            m_muteButtonImage.enabled = true;
        }
        else
        {
            m_unmuteButtonImage.enabled = true;
        }

        b_quitButtonTapped = false;
		_quitText.enabled = false;
	}

	public void ResumeButton()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(m_playerMutedSounds == 0 && !MusicManager.m_musicSource.isPlaying)
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

		_inGameUIObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
		_soundManager.m_soundsSource.clip = _soundManager.m_selfie;
		
        if(_soundManager.m_soundsSource.enabled)
        {
            _soundManager.m_soundsSource.Play();
        }

		_selfieButtonObj.SetActive(false);

		if(_bSelfieFlashEnabled)
		{
			_selfiePanelObj.SetActive(true);
			Invoke("EndFlash" , 0.25f);
		}

		if(_landPuss.m_isSlipping)
        {
            ScoreManager.m_scoreValue += 60;
        }

        else if(_landPuss.m_isSuper) 
		{
			ScoreManager.m_scoreValue += 200;
		} 

		else 
		{
			ScoreManager.m_scoreValue += 20;
		}

		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
	}

    public void SwipeHandOKButton()
    {
        m_chimpionshipBeltButtonImage.enabled = true;
        m_HighScoreLabelText.enabled = true;
        m_HighScoreValueText.enabled = true;

        if(m_playerMutedSounds == 0)
        {
            m_muteButtonImage.enabled = true;
        }
        else
        {
            m_unmuteButtonImage.enabled = true;
        }
        
        m_pauseButtonImage.enabled = true;
        _swipeDownHandAnimator.enabled = false;
        _swipeUpHandAnimator.enabled = false;
        _swipeDownHandImage.enabled = false;
        _swipeUpHandImage.enabled = false;
        _swipeHandOKButtonImage.enabled = false;
        _swipeHandPanelImage.enabled = false;
        Time.timeScale = 1;
    }
}
