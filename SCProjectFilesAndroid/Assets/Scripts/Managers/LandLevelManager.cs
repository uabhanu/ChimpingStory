using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandLevelManager : MonoBehaviour 
{
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use
    private LandPuss _landPuss;
	private SoundManager _soundManager;
	private Text _quitText;

    private static Animator _swipeDownHandAnimator , _swipeUpHandAnimator;
    private static Image _swipeDownHandImage , _swipeUpHandImage , _swipeHandOKButtonImage , _swipeHandPanelImage;
    private static int _firstTimeJump = 0 , _firstTimeSlide = 0;

	[SerializeField] private bool _bSelfieFlashEnabled;
    [SerializeField] private GameObject _adsMenuObj , _iapCartMenuObj , _inGameUIObj , _pauseMenuObj , _selfieButtonObj , _selfiePanelObj , _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public static bool b_isFirstTimeTutorialTestingMode , b_isMemoryLeakTestingMode , b_isUnityEditorTestingMode , b_quitButtonTapped;
    public static GameObject m_pauseMenuObj;
    public static Image m_uiButtonsTutorialMenuImage;
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
        m_HighScoreLabelText.enabled = false;
        m_HighScoreValueText.enabled = false;
        m_PolaroidsCountText.enabled = false;
		_selfieButtonObj.SetActive(false);
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
        BhanuPrefs.SetSuperPickedUp(ScoreManager.m_supersCount);
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
		BhanuPrefs.SetSuperPickedUp(ScoreManager.m_supersCount);
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

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;

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

        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_PolaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _swipeDownHandAnimator = GameObject.Find("SwipeDownHand").GetComponent<Animator>();
        _swipeUpHandAnimator = GameObject.Find("SwipeUpHand").GetComponent<Animator>();
        _swipeDownHandImage = GameObject.Find("SwipeDownHand").GetComponent<Image>();
        _swipeUpHandImage = GameObject.Find("SwipeUpHand").GetComponent<Image>();
        _swipeHandOKButtonImage = GameObject.Find("SwipeHandOKButton").GetComponent<Image>();
        _swipeHandPanelImage = GameObject.Find("SwipeHandPanel").GetComponent<Image>();

        if(m_playerMutedSounds == 1)
        {
            _soundManager.m_soundsSource.Play();
            _soundOffButtonObj.SetActive(true);
            _soundOnButtonObj.SetActive(false);
        }

        else if(m_playerMutedSounds == 0)
        {
            _soundManager.m_soundsSource.Pause();
            _soundOffButtonObj.SetActive(false);
            _soundOnButtonObj.SetActive(true);
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
        BhanuPrefs.SetSuperPickedUp(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    public void IAPYesButton()
    {
        _iapCartMenuObj.SetActive(true);
    }

    public void PauseButton()
	{
        _inGameUIObj.SetActive(false);
        _pauseMenuObj.SetActive(true);
        _soundManager._musicSource.Pause();
		Time.timeScale = 0;
	}

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        MusicManager.m_musicSource.Pause();
        b_quitButtonTapped = true;
		_quitText.enabled = true;
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
        b_quitButtonTapped = false;
		_quitText.enabled = false;
	}

	public void ResumeButton()
	{
		_inGameUIObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        _soundManager._musicSource.Play();
		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
		_soundManager.m_soundsSource.clip = _soundManager._selfie;
		
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

    public void SoundOffButton()
    {
        _soundManager._musicSource.Pause();
        m_playerMutedSounds = 1;
        _soundOffButtonObj.SetActive(false);
        _soundOnButtonObj.SetActive(true);
        BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
    }

    public void SoundOnButton()
    {
        _soundManager._musicSource.Play();
        m_playerMutedSounds = 0;
        _soundOffButtonObj.SetActive(true);
        _soundOnButtonObj.SetActive(false);
        BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
    }

    public void SwipeHandOKButton()
    {
        m_HighScoreLabelText.enabled = true;
        m_HighScoreValueText.enabled = true;
        _swipeDownHandAnimator.enabled = false;
        _swipeUpHandAnimator.enabled = false;
        _swipeDownHandImage.enabled = false;
        _swipeUpHandImage.enabled = false;
        _swipeHandOKButtonImage.enabled = false;
        _swipeHandPanelImage.enabled = false;
        Time.timeScale = 1;
    }
}
