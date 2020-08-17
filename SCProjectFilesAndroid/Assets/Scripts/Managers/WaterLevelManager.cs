using UnityEngine;
using UnityEngine.UI;

public class WaterLevelManager : MonoBehaviour
{
    private int _playerMutedSounds;

    [SerializeField] GameObject _inGameMenuObj , _pauseMenuObj;
    
    public Text m_HighScoreValueText;
    void Start()
    {
        
    }

    public void SoundOffButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Pause();
            _playerMutedSounds = 1;
            BhanuPrefs.SetSoundsStatus(_playerMutedSounds);
        }
    }

    public void SoundOnButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
            _playerMutedSounds = 0;
            BhanuPrefs.SetSoundsStatus(_playerMutedSounds);
        }
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
