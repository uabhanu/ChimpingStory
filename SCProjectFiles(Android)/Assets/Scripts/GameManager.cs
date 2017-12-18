using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	AudioSource m_musicSource;
	Image m_pauseButtonImage , m_pauseMenuImage , m_quitButtonImage , m_restartButtonImage , m_restartAcceptButtonImage , m_restartCancelButtonImage , m_restartMenuImage;
	Image m_resumeButtonImage;
	string m_currentScene;
	Text m_restart;

	void Start()
	{
		m_currentScene = SceneManager.GetActiveScene().name;
		m_musicSource = GetComponent<AudioSource>();	
		m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
		m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
		m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
		m_restartButtonImage = GameObject.Find("RestartButton").GetComponent<Image>();
		m_restartMenuImage = GameObject.Find("RestartMenu").GetComponent<Image>();
		m_restartAcceptButtonImage = GameObject.Find("RestartAcceptButton").GetComponent<Image>();
		m_restartCancelButtonImage = GameObject.Find("RestartCancelButton").GetComponent<Image>();
		m_restart = GameObject.Find("RestartText").GetComponent<Text>();
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
