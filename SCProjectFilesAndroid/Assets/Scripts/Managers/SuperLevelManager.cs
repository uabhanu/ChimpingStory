using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperLevelManager : MonoBehaviour
{
    [SerializeField] private float _timerValue; //TODO After testing done, set the value back to 30 in the inspector

    [SerializeField] private GameObject _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] private GameObject _exitToLandMenuObj , _inGameMenuObj , _pauseMenuObj; 
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Text _timerDisplay;
    
    public Text m_HighScoreValueText; //TODO This should be in the ScoreManager Script

    private void Start()
    {
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

        _timerDisplay.text = _timerValue.ToString();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //TODO Figure out a way to do this les expensive way
        if(_timerValue > 0)
        {
            _timerValue -= Time.deltaTime;
            _timerDisplay.text = Mathf.Round(_timerValue).ToString();
        }

        if(_timerValue <= 0)
        {
            //Time.timeScale = 0;
            //_exitToLandMenuObj.SetActive(true);
            //_inGameMenuObj.SetActive(false);
            //return;
            SceneManager.LoadScene("LandRunner");

        }
    }

    public void ExitToLandButton()
    {
        SceneManager.LoadScene("LandRunner");
    }

    public void SoundOffButton()
    {
        _soundManager.m_musicSource.Pause();
        SoundManager.m_playerMutedSounds = 1;
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

    public void PauseButton()
    {
        _inGameMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        _inGameMenuObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        Time.timeScale = 1;
    }
}
