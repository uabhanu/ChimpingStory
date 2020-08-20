using UnityEngine;
using UnityEngine.UI;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] GameObject _inGameMenuObj , _pauseMenuObj , _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] SoundManager _soundManager;
    
    public Text m_HighScoreValueText; //TODO This should be in the ScoreManager Script
    void Start()
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

        Time.timeScale = 1;
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
