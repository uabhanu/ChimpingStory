using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioVideoManager : MonoBehaviour 
{
	Image m_backgroundImage , m_playButtonImage , m_quitButtonImage;
	VideoPlayer m_videoPlayer;

	[SerializeField] bool m_logoPlayed;

	public static AudioSource m_musicSource;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		m_backgroundImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
		m_playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
		m_quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
		m_musicSource = GetComponent<AudioSource>();
		m_videoPlayer = FindObjectOfType<VideoPlayer>();

		if(!m_logoPlayed)
		{
			m_backgroundImage.enabled = false;
			m_playButtonImage.enabled = false;
			m_quitButtonImage.enabled = false;
			m_videoPlayer.Play();
			m_videoPlayer.loopPointReached += EndReached; //APK Build Success but corrupted for some reason, but not because of Video Player
		}

		if(m_logoPlayed)
		{
			m_backgroundImage.enabled = true;
			m_playButtonImage.enabled = true;
			m_quitButtonImage.enabled = true;
			m_videoPlayer.enabled = false;
			m_musicSource.Play();
		}
	}

	void EndReached(VideoPlayer videoPlayer)
	{
		m_logoPlayed = true;
		m_backgroundImage.enabled = true;
		m_playButtonImage.enabled = true;
		m_quitButtonImage.enabled = true;
		videoPlayer.enabled = false;
		m_musicSource.Play();
	}
}
