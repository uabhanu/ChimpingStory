using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class LandLevelManager : MonoBehaviour
{
    private int _currentSceneIndex;

    [SerializeField] private GameObject _selfieButtonObj;
	[SerializeField] private GameObject _adsMenuObj , _inGameUIObj , _mainMenuObj , _pauseMenuObj , _selfiePanelObj;
    [SerializeField] GameManagerSO _gameManagerSO;
    [SerializeField] private ScoreManagerSO _scoreManagerSO;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] GameObject _soundsMuteButtonObj , _soundsUnmuteButtonObj;

    public bool _bisUnityEditorTestingMode , _bSelfieFlashEnabled;

    private void Start()
    {
        GetReferences();
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    public void OnAdsUI()
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

    public void GetReferences()
    {
        Advertisement.Initialize("3696337");
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _adsMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(false);
        _selfieButtonObj.SetActive(false);
        _selfiePanelObj.SetActive(false);
        
        if(_soundManager.m_PlayerMutedSounds == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
            _soundsMuteButtonObj.SetActive(true);
            _soundsUnmuteButtonObj.SetActive(false);
        }

        else if(_soundManager.m_PlayerMutedSounds == 1)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
            _soundsMuteButtonObj.SetActive(false);
            _soundsUnmuteButtonObj.SetActive(true);
        }

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

		if(_gameManagerSO._bSelfieFlashEnabled)
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
        EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
        _soundsMuteButtonObj.SetActive(false);
        _soundsUnmuteButtonObj.SetActive(true);
    }

    public void SoundsUnmuteButton()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
        _soundsMuteButtonObj.SetActive(true);
        _soundsUnmuteButtonObj.SetActive(false);
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.AdsUI , OnAdsUI);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.AdsUI , OnAdsUI);
    }
}
