using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class LandLevelManager : MonoBehaviour
{
    private int _currentSceneIndex , _playerMutedSounds;

    [SerializeField] private GameObject _selfieButtonObj;
	[SerializeField] private GameObject _adsMenuObj , _inGameUIObj , _pauseMenuObj , _selfiePanelObj;
    [SerializeField] GameManagerSO _gameManagerSO;
    [SerializeField] private ScoreManager _scoreManager;
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
        EventsManager.InvokeEvent(SelfiePussEvent.AdsSkipped);
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
            EventsManager.InvokeEvent(SelfiePussEvent.RewardsAdWatched);
            Time.timeScale = 1;
            SceneManager.LoadScene(_currentSceneIndex);
        }

        else if(result == ShowResult.Skipped)
        {
            
        }

        else if(result == ShowResult.Failed)
        {
            
        }
    }

    public void ExitButton()
	{
        BhanuPrefs.DeleteScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void GetReferences()
    {
        Advertisement.Initialize("3696337");

        _playerMutedSounds = _soundManager.GetPlayerMutedSoundsValue();

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _adsMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(false);
        _selfieButtonObj.SetActive(false);
        _selfiePanelObj.SetActive(false);
        
        if(_playerMutedSounds == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
            _soundsMuteButtonObj.SetActive(true);
            _soundsUnmuteButtonObj.SetActive(false);
        }

        else if(_playerMutedSounds == 1)
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

        EventsManager.InvokeEvent(SelfiePussEvent.SelfieTaken);
	}

    public void SoundsMuteButton()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.SoundsMuted);
        _playerMutedSounds = 1;
        _soundManager.SetPlayerMutedSoundsValue(_playerMutedSounds);
        _soundsMuteButtonObj.SetActive(false);
        _soundsUnmuteButtonObj.SetActive(true);
    }

    public void SoundsUnmuteButton()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.SoundsUnmuted);
        _playerMutedSounds = 0;
        _soundManager.SetPlayerMutedSoundsValue(_playerMutedSounds);
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
