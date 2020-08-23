using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandLevelManager : MonoBehaviour 
{
    //TODO Text Mesh Pro
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use
    private LandPuss _landPuss;
	private SoundManager _soundManager;
	private Text _quitText;

    private static Animator _swipeDownHandAnimator , _swipeUpHandAnimator;
    private static Image _swipeDownHandImage , _swipeUpHandImage , _swipeHandOKButtonImage , _swipeHandPanelImage;
    
    [SerializeField] private bool _bSelfieFlashEnabled;
    [SerializeField] private GameObject _adsMenuObj , _iapCartMenuObj , _inGameUIObj , _pauseMenuObj , _selfieButtonObj , _selfiePanelObj , _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public static bool b_isFirstTimeTutorialTestingMode , b_isMemoryLeakTestingMode , b_isUnityEditorTestingMode , b_quitButtonTapped;
    public static GameObject m_pauseMenuObj;
    public static Image m_uiButtonsTutorialMenuImage;
    public static int m_currentScene , m_firstTimeIAPTutorialAppeared , m_firstTimeUIButtonsTutorial , m_firstTimeWaterLevelTutorial;
    public static Text m_chimpionshipBeltButtonTutorialText , m_leaderboardButtonTutorialText , m_muteUnmuteButtonTutorialText , m_pauseButtonTutorialText;

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
        _scoreManager.m_HighScoreLabelText.enabled = false;
        _scoreManager.m_HighScoreValueText.enabled = false;
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
            BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
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
        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _swipeDownHandAnimator = GameObject.Find("SwipeDownHand").GetComponent<Animator>();
        _swipeUpHandAnimator = GameObject.Find("SwipeUpHand").GetComponent<Animator>();
        _swipeDownHandImage = GameObject.Find("SwipeDownHand").GetComponent<Image>();
        _swipeUpHandImage = GameObject.Find("SwipeUpHand").GetComponent<Image>();
        _swipeHandOKButtonImage = GameObject.Find("SwipeHandOKButton").GetComponent<Image>();
        _swipeHandPanelImage = GameObject.Find("SwipeHandPanel").GetComponent<Image>();

       
        if(SoundManager.m_playerMutedSounds == 1)
        {
            _soundManager.m_musicSource.Pause();
            _soundOffButtonObj.SetActive(false);
            _soundOnButtonObj.SetActive(true);
        }

        else if(SoundManager.m_playerMutedSounds == 0)
        {
            _soundManager.m_musicSource.Play();
            _soundOffButtonObj.SetActive(true);
            _soundOnButtonObj.SetActive(false);
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
        _soundManager.m_musicSource.Pause();
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
        _soundManager.m_musicSource.Play();
		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
		_soundManager.m_soundsSource.clip = _soundManager.m_selfie;
		
        if(SoundManager.m_playerMutedSounds == 0)
        {
            _soundManager.m_soundsSource.Play();
        }

		_selfieButtonObj.SetActive(false);

		if(_bSelfieFlashEnabled)
		{
			_selfiePanelObj.SetActive(true);
			Invoke("EndFlash" , 0.25f);
		}

		_scoreManager.m_scoreValue += 20;
		BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
        _scoreManager.m_HighScoreValueText.text = _scoreManager.m_scoreValue.ToString();
	}

    public void SoundOffButton()
    {
        _soundManager.m_musicSource.Pause();
        SoundManager.m_playerMutedSounds++;
        _soundOffButtonObj.SetActive(false);
        _soundOnButtonObj.SetActive(true);
        BhanuPrefs.SetSoundsStatus(SoundManager.m_playerMutedSounds);
    }

    public void SoundOnButton()
    {
        _soundManager.m_musicSource.Play();
        SoundManager.m_playerMutedSounds = 0;
        _soundOffButtonObj.SetActive(true);
        _soundOnButtonObj.SetActive(false);
        BhanuPrefs.SetSoundsStatus(SoundManager.m_playerMutedSounds);
    }
}
