using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int _currentSceneIndex;

	[SerializeField] private GameObject _mainMenuObj , _quitMenuObj;
    [SerializeField] GameManagerSO _gameManagerSO;
    [SerializeField] GameObject _soundsMuteButtonObj , _soundsUnmuteButtonObj;
    [SerializeField] private SoundManager _soundManager;

    private void Start()
    {
        GetReferences();
    }

    public void GetReferences()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _quitMenuObj.SetActive(false);
        
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

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
		_mainMenuObj.SetActive(true);
        _quitMenuObj.SetActive(false);
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
}
