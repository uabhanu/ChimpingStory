using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
    bool m_playerMutedSounds;
	Image m_adsAcceptButtonImage , m_adsCancelButtonImage , m_adsMenuImage , m_backToLandLoseMenuImage , m_backToLandWinMenuImage , m_backToLandWithSuperMenuImage , m_chimpionshipBeltImage;
    Image m_continueButtonImage , m_exitButtonImage , m_muteButtonImage , m_pauseButtonImage , m_pauseMenuImage , m_playButtonImage , m_quitButtonImage , m_quitAcceptButtonImage;
    Image m_quitCancelButtonImage , m_quitMenuImage , m_restartButtonImage , m_restartAcceptButtonImage , m_restartCancelButtonImage , m_restartMenuImage , m_resumeButtonImage;
    Image m_unmuteButtonImage;
    LandChimp m_landChimp;
	SoundManager m_soundManager;
	Text m_ads , m_backToLandLose , m_backToLandWin , m_backToLandWithSuper , m_highScoreTextDisplay , m_highScoreValueDisplay , m_quit , m_restart;

	[SerializeField] bool m_selfieFlashEnabled;

    public static Image m_selfieButtonImage , m_selfiePanelImage;
    public static int m_currentScene;

    void Start()
	{
		GetBhanuObjects();

        if(MusicManager.m_musicSource != null && !MusicManager.m_musicSource.isPlaying)
        {
            MusicManager.m_musicSource.Play();
        }
    }
	
    public void Ads()
    {
        m_adsMenuImage.enabled = true;
        m_adsAcceptButtonImage.enabled = true;
        m_adsCancelButtonImage.enabled = true;
        m_ads.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAccept()
    {
        m_adsMenuImage.enabled = false;
        m_adsAcceptButtonImage.enabled = false;
        m_adsCancelButtonImage.enabled = false;
        m_ads.enabled = false;
        AdsShow();
    }

    public void AdsCancel()
    {
        BhanuPrefs.DeleteAll();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    void AdResult(ShowResult result)
    {
		Time.timeScale = 1;

        if(result == ShowResult.Finished)
        {
            //Debug.Log("Video completed - Offer a reward to the player");
            ScoreManager.m_scoreValue *= 0.25f;
		    ScoreManager.m_scoreValue = Mathf.Round(ScoreManager.m_scoreValue);
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            SceneManager.LoadScene(m_currentScene);
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
        m_chimpionshipBeltImage.enabled = false;
		m_continueButtonImage.enabled = true;
		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWinMenu()
    {
        m_backToLandWinMenuImage.enabled = true;
        m_backToLandWin.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
		m_continueButtonImage.enabled = true;
		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWithSuperMenu()
    {
        m_backToLandWithSuperMenuImage.enabled = true;
        m_backToLandWithSuper.enabled = true;
        m_continueButtonImage.enabled = true;
        m_chimpionshipBeltImage.enabled = false;
		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void Continue()
    {
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
		Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("LandRunner");
    }

    void EndFlash()
	{
		m_selfiePanelImage.enabled = false;
	}

    public void ExitToMainMenu()
	{
        SceneManager.LoadScene("MainMenu");
    }

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;

        if(m_currentScene == 0)
        {
            if(MusicManager.m_musicSource != null)
            {
                MusicManager.m_musicSource.Play();
            }
            
            m_playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            m_quit = GameObject.Find("QuitText").GetComponent<Text>();
            m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            m_quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            m_quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            m_quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();
        }

        else if(m_currentScene == 1)
        {
            m_ads = GameObject.Find("AdsText").GetComponent<Text>();
            m_adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            m_adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            m_adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            m_chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
            m_exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
			m_landChimp = FindObjectOfType<LandChimp>();
			m_highScoreTextDisplay = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
            m_restart = GameObject.Find("RestartText").GetComponent<Text>();
			m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
            m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
            m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
            m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
			m_selfieButtonImage = GameObject.Find("SelfieButton").GetComponent<Image>();
			m_selfiePanelImage = GameObject.Find("SelfiePanel").GetComponent<Image>();
            m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();
        }

		else
		{
			m_backToLandLoseMenuImage = GameObject.Find("BackToLandLoseMenu").GetComponent<Image>();
			m_backToLandLose = GameObject.Find("BackToLandLose").GetComponent<Text>();
			m_backToLandWinMenuImage = GameObject.Find("BackToLandWinMenu").GetComponent<Image>();
			m_backToLandWin = GameObject.Find("BackToLandWin").GetComponent<Text>();
            m_backToLandWithSuperMenuImage = GameObject.Find("BackToLandWithSuperMenu").GetComponent<Image>();
			m_backToLandWithSuper = GameObject.Find("BackToLandWithSuper").GetComponent<Text>();
            m_chimpionshipBeltImage = GameObject.Find("ChimpionshipBelt").GetComponent<Image>();
			m_continueButtonImage = GameObject.Find("ContinueButton").GetComponent<Image>();
			m_highScoreTextDisplay = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
			m_highScoreValueDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
			m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();
		}

		Time.timeScale = 1;
    }

    public void MuteUnmute()
    {
        if(MusicManager.m_musicSource != null)
        {
            if(m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = false;
                MusicManager.m_musicSource.Pause();
                m_playerMutedSounds = true;
                m_unmuteButtonImage.enabled = true;
                m_soundManager.m_soundsSource.enabled = false;
            }

            else if(!m_muteButtonImage.enabled)
            {
                m_muteButtonImage.enabled = true;
                MusicManager.m_musicSource.Play();
                m_playerMutedSounds = false;
                m_unmuteButtonImage.enabled = false;
                m_soundManager.m_soundsSource.enabled = true;
            }
        }
    }

    public void Pause()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(MusicManager.m_musicSource.isPlaying)
            {
                MusicManager.m_musicSource.Pause();
                m_muteButtonImage.enabled = false;
            }
            else
            {
                m_unmuteButtonImage.enabled = false;
            }
        }

        m_chimpionshipBeltImage.enabled = false;
        
        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = true;
        }

		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
        
        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = true;
        }
        
		m_pauseButtonImage.enabled = false;
		m_pauseMenuImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		if(m_selfiePanelImage != null)
		{
			m_selfieButtonImage.enabled = false;	
		}

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
        if(MusicManager.m_musicSource != null)
        {
            MusicManager.m_musicSource.Play();
        }
        
        BhanuPrefs.DeleteAll();
		ScoreManager.m_supersCount = ScoreManager.m_defaultSupersCount;
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
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
        if(MusicManager.m_musicSource != null)
        {
            if(!MusicManager.m_musicSource.isPlaying && !m_playerMutedSounds)
            {
                MusicManager.m_musicSource.Play();
                m_muteButtonImage.enabled = true;
            }
            else
            {
                m_unmuteButtonImage.enabled = true;
            }
        }

        m_chimpionshipBeltImage.enabled = true;

        if(m_exitButtonImage != null)
        {
            m_exitButtonImage.enabled = false;
        }

		m_highScoreTextDisplay.enabled = true;
		m_highScoreValueDisplay.enabled = true;
		m_pauseButtonImage.enabled = true;
		m_pauseMenuImage.enabled = false;

        if(m_restartButtonImage != null)
        {
            m_restartButtonImage.enabled = false;
        }

        m_resumeButtonImage.enabled = false;

		Time.timeScale = 1;
	}

	public void SelfieClicked()
	{
		m_soundManager.m_soundsSource.clip = m_soundManager.m_selfie;
		m_soundManager.m_soundsSource.Play();
		m_selfieButtonImage.enabled = false;

		if(m_selfieFlashEnabled)
		{
			m_selfiePanelImage.enabled = true;
			Invoke("EndFlash" , 0.25f);
		}

		if(m_landChimp.m_isSuper) 
		{
			ScoreManager.m_scoreValue += 200;
		} 
		else 
		{
			ScoreManager.m_scoreValue += 20;
		}

		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
	}
}
