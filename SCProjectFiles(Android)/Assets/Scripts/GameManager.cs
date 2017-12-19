using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
	Image m_backgroundImage , m_exitButtonImage , m_exitAcceptButtonImage , m_exitCancelButtonImage , m_exitMenuImage , m_pauseButtonImage , m_pauseMenuImage , m_playButtonImage;
	Image m_quitButtonImage , m_quitAcceptButtonImage , m_quitCancelButtonImage , m_quitMenuImage , m_restartButtonImage , m_restartAcceptButtonImage , m_restartCancelButtonImage;
	Image  m_restartMenuImage , m_resumeButtonImage;
	string m_currentScene;
	Text m_exit , m_quit , m_restart;

	void Start()
	{
		m_currentScene = SceneManager.GetActiveScene().name;
		StartCoroutine("GetBhanuObjectsRoutine");
	}

	IEnumerator GetBhanuObjectsRoutine()
	{
		yield return new WaitForSeconds(0.15f);

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

		if(m_currentScene == "LandRunner") 
		{
			m_exit = GameObject.Find("ExitText").GetComponent<Text>();
			m_exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
			m_exitAcceptButtonImage = GameObject.Find("ExitAcceptButton").GetComponent<Image>();
			m_exitCancelButtonImage = GameObject.Find("ExitCancelButton").GetComponent<Image>();
			m_exitMenuImage = GameObject.Find("ExitMenu").GetComponent<Image>();
			m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
			m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
			m_restart = GameObject.Find("RestartText").GetComponent<Text>();
			m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
			m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
			m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
			m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
			m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();	
		} 

		StartCoroutine("GetBhanuObjectsRoutine");
	}

	public void Exit()
	{
		m_exitButtonImage.enabled = false;

		m_exitMenuImage.enabled = true;
		m_exitAcceptButtonImage.enabled = true;
		m_exitCancelButtonImage.enabled = true;
		m_exit.enabled = true;

		m_pauseMenuImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;
	}

	public void ExitAccept()
	{
		MediaManager.m_musicSource.Play();
		SceneManager.LoadScene("MainMenu");
	}

	public void ExitCancel()
	{
		m_exitButtonImage.enabled = true;

		m_exitMenuImage.enabled = false;
		m_exitAcceptButtonImage.enabled = false;
		m_exitCancelButtonImage.enabled = false;
		m_exit.enabled = false;

		m_pauseMenuImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;
	}

	public void Pause()
	{
		MediaManager.m_musicSource.Pause();
		m_pauseButtonImage.enabled = false;

		m_exitButtonImage.enabled = true;
		m_pauseMenuImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;
		Time.timeScale = 0;
	}

	public void Play()
	{
		SceneManager.LoadScene("LandRunner");
		Time.timeScale = 1;
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
		MediaManager.m_musicSource.Play();
		Time.timeScale = 1;
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
		MediaManager.m_musicSource.Play();
		m_pauseButtonImage.enabled = true;

		m_exitButtonImage.enabled = false;
		m_pauseMenuImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;
		Time.timeScale = 1;
	}
}
