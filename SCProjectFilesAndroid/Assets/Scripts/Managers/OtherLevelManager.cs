using SelfiePuss.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherLevelManager : MonoBehaviour
{
    private int _currentSceneIndex , _playerMutedSounds;

    [SerializeField] private float _countDownTimer;
    [SerializeField] private GameManagerSO _gameManagerSO;
    [SerializeField] private GameObject _inGameUIObj , _pauseMenuObj;
    [SerializeField] GameObject _soundsMuteButtonObj , _soundsUnmuteButtonObj;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private TextMeshProUGUI _countdownDisplayText;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_countDownTimer > 0)
        {
            _countDownTimer -= Time.deltaTime;
        }
    
        _countdownDisplayText.text = Mathf.Round(_countDownTimer).ToString();

        if(_countDownTimer <= 0)
        {
            SceneManager.LoadScene("LandRunner");
        }
    }

    public void GetReferences()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _countDownTimer = _gameManagerSO.GetCountDownValue();

        _pauseMenuObj.SetActive(false);

        _playerMutedSounds = _soundManager.GetPlayerMutedSoundsValue();
        
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

	public void ResumeButton()
	{
		_inGameUIObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        EventsManager.InvokeEvent(SelfiePussEvent.Resumed);
		Time.timeScale = 1;
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
}
