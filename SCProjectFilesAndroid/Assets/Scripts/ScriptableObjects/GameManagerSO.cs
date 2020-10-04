using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu]
public class GameManagerSO : ScriptableObject 
{
    //TODO Text Mesh Pro
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use
    private float _countdownValue;
    private GameObject _selfieButtonObj;
	private GameObject _adsMenuObj , _inGameUIObj , _mainMenuObj , _pauseMenuObj , _quitMenuObj , _selfiePanelObj;
    private int _currentSceneIndex;
    private Text _quitText;

    
    [SerializeField] private bool _bSelfieFlashEnabled;
    [SerializeField] private ScoreManagerSO _scoreManagerSO;
    [SerializeField] private SoundManagerSO _soundManagerSO;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public bool _bisUnityEditorTestingMode;
    public GameObject _soundsMuteButtonObj , _soundsUnmuteButtonObj;

    public void Ads()
    {
        _adsMenuObj.SetActive(true);
        _inGameUIObj.SetActive(false);
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
        SceneManager.LoadScene(_currentSceneIndex);
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
            BhanuPrefs.SetHighScore(_scoreManagerSO.m_ScoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(_currentSceneIndex);
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

 //   void EndFlash()
	//{
	//	_selfiePanelObj.SetActive(false);
	//}

    public void ExitButton()
	{
        BhanuPrefs.DeleteScore();
        SceneManager.LoadScene("MainMenu");
    }

    public float GetCountDownValue()
    {
        return _countdownValue;
    }

    public void GetLandLevelObjects()
    {
        Advertisement.Initialize("3696337");

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _adsMenuObj = GameObject.FindGameObjectWithTag("AdsMenu");
        _inGameUIObj = GameObject.FindGameObjectWithTag("InGameUI");
        _pauseMenuObj = GameObject.FindGameObjectWithTag("PauseMenu");
        _selfieButtonObj = GameObject.FindGameObjectWithTag("SelfieButton");
        _selfiePanelObj = GameObject.FindGameObjectWithTag("SelfiePanel");
        _soundsMuteButtonObj = GameObject.FindGameObjectWithTag("Mute");
        _soundsUnmuteButtonObj = GameObject.FindGameObjectWithTag("Unmute");

        if(_soundManagerSO.m_playerMutedSounds == 1)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
            _soundsMuteButtonObj.SetActive(false);
            _soundsUnmuteButtonObj.SetActive(true);
        }

        else if(_soundManagerSO.m_playerMutedSounds == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
            _soundsMuteButtonObj.SetActive(true);
            _soundsUnmuteButtonObj.SetActive(false);
        }

        _adsMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(false);
        _selfieButtonObj.SetActive(false);
        _selfiePanelObj.SetActive(false);

        Time.timeScale = 1;
    }

    public void GetMainMenuLevelObjects()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _mainMenuObj = GameObject.FindGameObjectWithTag("MainMenu");
        _quitMenuObj = GameObject.FindGameObjectWithTag("QuitMenu");
        _soundsMuteButtonObj = GameObject.FindGameObjectWithTag("Mute");
        _soundsUnmuteButtonObj = GameObject.FindGameObjectWithTag("Unmute");

        if(_soundManagerSO.m_playerMutedSounds == 1)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
            _soundsMuteButtonObj.SetActive(false);
            _soundsUnmuteButtonObj.SetActive(true);
        }

        else if(_soundManagerSO.m_playerMutedSounds == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
            _soundsMuteButtonObj.SetActive(true);
            _soundsUnmuteButtonObj.SetActive(false);
        }

        _quitMenuObj.SetActive(false);

        Time.timeScale = 1;
    }

    public void GetOtherLevelObjects()
    {
        _countdownValue = 30.0f;
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _inGameUIObj = GameObject.FindGameObjectWithTag("InGameUI");
        _pauseMenuObj = GameObject.FindGameObjectWithTag("PauseMenu");
        _soundsMuteButtonObj = GameObject.FindGameObjectWithTag("Mute");
        _soundsUnmuteButtonObj = GameObject.FindGameObjectWithTag("Unmute");

        if(_soundManagerSO.m_playerMutedSounds == 1)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
            _soundsMuteButtonObj.SetActive(false);
            _soundsUnmuteButtonObj.SetActive(true);
        }

        else if(_soundManagerSO.m_playerMutedSounds == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
            _soundsMuteButtonObj.SetActive(true);
            _soundsUnmuteButtonObj.SetActive(false);
        }

        _pauseMenuObj.SetActive(false);

        Time.timeScale = 1;
    }

    public void PauseButton()
	{
        _inGameUIObj.SetActive(false);
        _pauseMenuObj.SetActive(true);
        EventsManager.InvokeEvent(SelfiePussEvent.Paused);
		Time.timeScale = 0;
	}

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        _mainMenuObj.SetActive(false);
        _quitMenuObj.SetActive(true);
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
		_mainMenuObj.SetActive(true);
        _quitMenuObj.SetActive(false);
	}

	public void ResumeButton()
	{
		_inGameUIObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        EventsManager.InvokeEvent(SelfiePussEvent.Resumed);
		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
        EventsManager.InvokeEvent(SelfiePussEvent.SelfieTaken);
		_selfieButtonObj.SetActive(false);

		if(_bSelfieFlashEnabled)
		{
			_selfiePanelObj.SetActive(true);
			//Invoke("EndFlash" , 0.25f);
		}

		_scoreManagerSO.m_ScoreValue += 20;
		BhanuPrefs.SetHighScore(_scoreManagerSO.m_ScoreValue);
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreChanged);
	}

    public void SoundsMuteButton()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.Paused);
        _soundManagerSO.m_playerMutedSounds = 1;
        _soundsMuteButtonObj.SetActive(false);
        _soundsUnmuteButtonObj.SetActive(true);
        BhanuPrefs.SetSoundsStatus(_soundManagerSO.m_playerMutedSounds);
    }

    public void SoundsUnmuteButton()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.Resumed);
        _soundManagerSO.m_playerMutedSounds = 0;
        _soundsMuteButtonObj.SetActive(true);
        _soundsUnmuteButtonObj.SetActive(false);
        BhanuPrefs.SetSoundsStatus(_soundManagerSO.m_playerMutedSounds);
    }
}
