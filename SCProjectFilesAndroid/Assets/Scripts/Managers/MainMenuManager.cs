using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _quitMenuObj;
    [SerializeField] GameObject _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] SoundManager _soundManager;

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
