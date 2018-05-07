using UnityEngine;

public class MusicManager : MonoBehaviour 
{
	public static AudioSource m_musicSource;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		m_musicSource = GetComponent<AudioSource>();
        Invoke("CheckIfMusicShouldPlay" , 0.5f);
	}

    void CheckIfMusicShouldPlay()
    {
        if(GameManager.m_playerMutedSounds == 0 && !GameManager.b_quitButtonTapped && !m_musicSource.isPlaying)
        {
            m_musicSource.Play();
        }

        else if(GameManager.m_playerMutedSounds == 1 && m_musicSource.isPlaying)
        {
            m_musicSource.Pause();
        }

        Invoke("CheckIfMusicShouldPlay" , 0.5f);
    }
}
