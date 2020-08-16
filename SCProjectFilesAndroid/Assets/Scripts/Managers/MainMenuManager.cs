using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int _playerMutedSounds;

    [SerializeField] GameObject _quitMenuObj;

    public void MuteButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Pause();
            _playerMutedSounds = 1;
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

    public void UnmuteButton()
    {
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
            _playerMutedSounds = 0;
            BhanuPrefs.SetSoundsStatus(_playerMutedSounds);
        }
    }
}
