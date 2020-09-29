using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu]
public class GameManagerObject : ScriptableObject 
{
    //TODO Text Mesh Pro
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use
	private GameObject _adsMenuObj , _inGameUIObj , _pauseMenuObj , _selfiePanelObj;
    private GameObject _selfieButtonObj , _soundOffButtonObj , _soundOnButtonObj;
    private int _currentSceneIndex;
    private Text _quitText;

    
    [SerializeField] private bool _bSelfieFlashEnabled;
    [SerializeField] private ScoreManagerObject _scoreManagerObject;
    [SerializeField] private SoundManagerObject _soundManagerObject;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public bool _bisUnityEditorTestingMode;

    public void Ads()
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
        BhanuPrefs.DeleteScore();
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
            BhanuPrefs.SetHighScore(_scoreManagerObject.m_scoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(_currentSceneIndex);
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            BhanuPrefs.DeleteScore();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            BhanuPrefs.DeleteScore();
        }
    }

 //   void EndFlash()
	//{
	//	_selfiePanelObj.SetActive(false);
	//}

    public void ExitButton()
	{
        BhanuPrefs.DeleteScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void GetBhanuObjects()
    {
        Advertisement.Initialize("3696337");

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _adsMenuObj = GameObject.FindGameObjectWithTag("AdsMenu");
        _inGameUIObj = GameObject.FindGameObjectWithTag("InGameUI");
        _pauseMenuObj = GameObject.FindGameObjectWithTag("PauseMenu");
        _selfieButtonObj = GameObject.FindGameObjectWithTag("SelfieButton");
        _selfiePanelObj = GameObject.FindGameObjectWithTag("SelfiePanel");
        _soundOffButtonObj = GameObject.FindGameObjectWithTag("SoundOff");
        _soundOnButtonObj = GameObject.FindGameObjectWithTag("SoundOn");
        _scoreManagerObject.GetScoreItems();
        _soundManagerObject.GetSoundsStatus();

        _adsMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(false);
        _selfieButtonObj.SetActive(false);
        _selfiePanelObj.SetActive(false);
      
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

        Time.timeScale = 1;
    }

    public void PauseButton()
	{
        _inGameUIObj.SetActive(false);
        _pauseMenuObj.SetActive(true);
        _soundManagerObject.m_musicSource.Pause();
		Time.timeScale = 0;
	}

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        MusicManager.m_musicSource.Pause();
		_quitText.enabled = true;
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
		_quitText.enabled = false;
	}

	public void ResumeButton()
	{
		_inGameUIObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        _soundManagerObject.m_musicSource.Play();
		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
        _soundManagerObject.m_soundsSource.clip = _soundManagerObject.m_selfie;
		
        if(_soundManagerObject.m_playerMutedSounds == 0)
        {
            _soundManagerObject.m_soundsSource.Play();
        }

		_selfieButtonObj.SetActive(false);

		if(_bSelfieFlashEnabled)
		{
			_selfiePanelObj.SetActive(true);
			//Invoke("EndFlash" , 0.25f);
		}

		_scoreManagerObject.m_scoreValue += 20;
		BhanuPrefs.SetHighScore(_scoreManagerObject.m_scoreValue);
        _scoreManagerObject.m_HighScoreValueText.text = _scoreManagerObject.m_scoreValue.ToString();
	}

    public void SoundOffButton()
    {
        _soundManagerObject.m_musicSource.Pause();
        _soundManagerObject.m_playerMutedSounds++;
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
}
