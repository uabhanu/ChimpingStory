using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuObj , _quitMenuObj;
    [SerializeField] GameObject _soundOffButtonObj , _soundOnButtonObj;
    [SerializeField] private SoundManagerObject _soundManagerObject;

    private void Start()
    {
        _soundManagerObject.GetSoundsStatus();

        if(_soundManagerObject.m_playerMutedSounds == 1)
        {
            _soundManagerObject.m_musicSource.Pause();
            _soundOffButtonObj.SetActive(false);
            _soundOnButtonObj.SetActive(true);
        }

        else if(_soundManagerObject.m_playerMutedSounds == 0)
        {
            _soundManagerObject.m_musicSource.Play();
            _soundOffButtonObj.SetActive(true);
            _soundOnButtonObj.SetActive(false);
        }
    }

    public void SoundOffButton()
    {
        _soundManagerObject.m_musicSource.Pause();
        _soundManagerObject.m_playerMutedSounds = 1;
        _soundOffButtonObj.SetActive(false);
        _soundOnButtonObj.SetActive(true);
        BhanuPrefs.SetSoundsStatus(_soundManagerObject.m_playerMutedSounds);
    }

    public void SoundOnButton()
    {
        _soundManagerObject.m_musicSource.Play();
        _soundManagerObject.m_playerMutedSounds = 0;
        _soundOffButtonObj.SetActive(true);
        _soundOnButtonObj.SetActive(false);
        BhanuPrefs.SetSoundsStatus(_soundManagerObject.m_playerMutedSounds);   
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

    public void QuitYesButton()
    {
        Application.Quit();
    }

    public void QuitNoButton()
    {
        _mainMenuObj.SetActive(true);
        _quitMenuObj.SetActive(false);
    }
}
