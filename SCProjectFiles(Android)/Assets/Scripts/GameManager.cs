using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
	Image m_pauseButtonImage , m_pauseMenuImage , m_quitButtonImage , m_quitAcceptButtonImage , m_quitCancelButtonImage , m_quitMenuImage , m_restartButtonImage , m_restartAcceptButtonImage;
	Image m_restartCancelButtonImage , m_restartMenuImage , m_resumeButtonImage;
	string m_currentScene;
	Text m_quit , m_restart;

	void Start()
	{
		m_currentScene = SceneManager.GetActiveScene().name;
		m_musicSource = GetComponent<AudioSource>();	
		m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
		m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
		m_quit = GameObject.Find("QuitText").GetComponent<Text>();
		m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
		m_quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
		m_quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
		m_quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();
		m_restart = GameObject.Find("RestartText").GetComponent<Text>();
		m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
		m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
		m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
		m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
		m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
	}

	public void Pause()
	{
		m_musicSource.Pause();
		m_pauseButtonImage.enabled = false;
		m_pauseMenuImage.enabled = true;
		m_quitButtonImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;
		Time.timeScale = 0;
	}

	public void Quit()
	{
		m_pauseMenuImage.enabled = false;
		m_quitButtonImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;

		m_quitMenuImage.enabled = true;
		m_quitAcceptButtonImage.enabled = true;
		m_quitCancelButtonImage.enabled = true;
		m_quit.enabled = true;
	}

	public void QuitAccept()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitCancel()
	{
		m_pauseMenuImage.enabled = true;
		m_quitButtonImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		m_quitMenuImage.enabled = false;
		m_quitAcceptButtonImage.enabled = false;
		m_quitCancelButtonImage.enabled = false;
		m_quit.enabled = false;
	}

	public void Restart()
	{
		m_pauseMenuImage.enabled = false;
		m_quitButtonImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;

		m_restartMenuImage.enabled = true;
		m_restartAcceptButtonImage.enabled = true;
		m_restartCancelButtonImage.enabled = true;
		m_restart.enabled = true;
	}

	public void RestartAccept()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(m_currentScene);
	}

	public void RestartCancel()
	{
		m_pauseMenuImage.enabled = true;
		m_quitButtonImage.enabled = true;
		m_restartButtonImage.enabled = true;
		m_resumeButtonImage.enabled = true;

		m_restartMenuImage.enabled = false;
		m_restartAcceptButtonImage.enabled = false;
		m_restartCancelButtonImage.enabled = false;
		m_restart.enabled = false;
	}

	public void Resume()
	{
		m_musicSource.Play();
		m_pauseButtonImage.enabled = true;
		m_pauseMenuImage.enabled = false;
		m_quitButtonImage.enabled = false;
		m_restartButtonImage.enabled = false;
		m_resumeButtonImage.enabled = false;
		Time.timeScale = 1;
	}
}
