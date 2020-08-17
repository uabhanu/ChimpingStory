using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int _playerMutedSounds;

    [SerializeField] GameObject _quitMenuObj;
    [SerializeField] GameObject _soundOffButtonObj , _soundOnButtonObj;

    public void SoundOffButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Pause();
            _playerMutedSounds = 1;
            _soundOffButtonObj.SetActive(false);
            _soundOnButtonObj.SetActive(true);
            BhanuPrefs.SetSoundsStatus(_playerMutedSounds);
        }
    }

    public void SoundOnButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
            _playerMutedSounds = 0;
            _soundOffButtonObj.SetActive(true);
            _soundOnButtonObj.SetActive(false);
            BhanuPrefs.SetSoundsStatus(_playerMutedSounds);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("LandRunner");
    }

    public void QuitButton()
    {
        _quitMenuObj.SetActive(true);
    }

    public void QuitAcceptButton()
    {
        Application.Quit();
    }

    public void QuitCancelButton()
    {
        _quitMenuObj.SetActive(false);
    }
}
