using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
	Image m_adsAcceptButtonImage , m_adsCancelButtonImage , m_adsMenuImage , m_backgroundImage , m_backToLandLoseMenuImage , m_backToLandWinMenuImage , m_continueButtonLoseImage;
    Image m_continueButtonWinImage , m_exitButtonImage , m_pauseButtonImage , m_pauseMenuImage , m_playButtonImage , m_quitButtonImage , m_quitAcceptButtonImage , m_quitCancelButtonImage;
    Image m_quitMenuImage , m_restartButtonImage , m_restartAcceptButtonImage , m_restartCancelButtonImage , m_restartMenuImage , m_resumeButtonImage;
    string m_currentScene;
	Text m_ads , m_backToLandLose , m_backToLandWin , m_quit , m_restart;

    public float m_timeSinceGameStarted;

    void Start()
	{
        BananasSpawner.m_bananasCount = 0;
        BhanuPrefs.GetGameTime();
		GetBhanuObjects();
        m_timeSinceGameStarted = Time.time;
    }

    public void Ads()
    {
        m_adsMenuImage.enabled = true;
        m_adsAcceptButtonImage.enabled = true;
        m_adsCancelButtonImage.enabled = true;
        m_ads.enabled = true;
        m_pauseButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void AdsAccept()
    {
        m_adsMenuImage.enabled = false;
        m_adsAcceptButtonImage.enabled = false;
        m_adsCancelButtonImage.enabled = false;
        m_ads.enabled = false;
        AdsShow();
		ScoreManager.m_scoreValue *= 0.25f;
		ScoreManager.m_scoreValue = Mathf.Round(ScoreManager.m_scoreValue);
        BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
    }

    public void AdsCancel()
    {
        BhanuPrefs.DeleteAll();
        SceneManager.LoadScene(m_currentScene);
    }

    void AdResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            //Debug.Log("Video completed - Offer a reward to the player");
            SceneManager.LoadScene(m_currentScene);
            Time.timeScale = 1;
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            BhanuPrefs.DeleteAll();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            BhanuPrefs.DeleteAll();
        }
    }

    void AdsShow()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdResult;

        Advertisement.Show("rewardedVideo" , options);
    }

    public void BackToLandLoseMenu()
    {
        m_backToLandLoseMenuImage.enabled = true;
        m_backToLandLose.enabled = true;
        m_continueButtonLoseImage.enabled = true;
        m_pauseButtonImage.enabled = false;
    }

    public void BackToLandWinMenu()
    {
        m_backToLandWinMenuImage.enabled = true;
        m_backToLandWin.enabled = true;
        m_continueButtonWinImage.enabled = true;
        m_pauseButtonImage.enabled = false;
    }

    public void Continue()
    {
        SceneManager.LoadScene("LandRunner");
    }

    public void ExitToMainMenu()
	{
        SceneManager.LoadScene("MainMenu");
    }

    void GetBhanuObjects()
    {
        Time.timeScale = 1;
        m_currentScene = SceneManager.GetActiveScene().name;
        m_musicSource = GetComponent<AudioSource>();

        if(m_currentScene == "MainMenu")
        {
            m_backgroundImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
            m_playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            m_quit = GameObject.Find("QuitText").GetComponent<Text>();
            m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            m_quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            m_quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            m_quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();
        }

        else if(m_currentScene == "LandRunner")
        {
            m_ads = GameObject.Find("AdsText").GetComponent<Text>();
            m_adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            m_adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            m_adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            m_exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
            m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
            m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
            m_restart = GameObject.Find("RestartText").GetComponent<Text>();
            m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
            m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
            m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
            m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
            m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
        }
    }

    public void Pause()
	{
        if(AVManager.m_musicSource != null)
        {
            AVManager.m_musicSource.Pause();
        }
        
		m_pauseButtonImage.enabled = false;

        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = true;
        }
        
		m_pauseMenuImage.enabled = true;

        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = true;
        }
        
		m_resumeButtonImage.enabled = true;
		Time.timeScale = 0;
	}

	public void Play()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void Quit()
	{
		m_playButtonImage.enabled = false;
		m_quitButtonImage.enabled = false;

		m_quitMenuImage.enabled = true;
		m_quitAcceptButtonImage.enabled = true;
		m_quitCancelButtonImage.enabled = true;
		m_quit.enabled = true;
	}

	public void QuitAccept()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancel()
	{
		m_playButtonImage.enabled = true;
		m_quitButtonImage.enabled = true;

		m_quitMenuImage.enabled = false;
		m_quitAcceptButtonImage.enabled = false;
		m_quitCancelButtonImage.enabled = false;
		m_quit.enabled = false;
	}

	public void Restart()
	{
		m_exitButtonImage.enabled = false;
		m_pauseMenuImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;

		m_restartMenuImage.enabled = true;
		m_restartAcceptButtonImage.enabled = true;
		m_restartCancelButtonImage.enabled = true;
		m_restart.enabled = true;
	}

	public void RestartAccept()
	{
        if(AVManager.m_musicSource != null)
        {
            AVManager.m_musicSource.Play();
        }
        
        BhanuPrefs.DeleteAll();
		SceneManager.LoadScene(m_currentScene);
	}

    public void RestartCancel()
	{
		m_exitButtonImage.enabled = true;
		m_pauseMenuImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		m_restartMenuImage.enabled = false;
		m_restartAcceptButtonImage.enabled = false;
		m_restartCancelButtonImage.enabled = false;
		m_restart.enabled = false;
	}

	public void Resume()
	{
        if(AVManager.m_musicSource != null)
        {
            AVManager.m_musicSource.Play();
        }

        m_pauseButtonImage.enabled = true;

        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = false;
        }

        m_pauseMenuImage.enabled = false;

        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = false;
        }

        m_resumeButtonImage.enabled = false;
		Time.timeScale = 1;
	}
}
