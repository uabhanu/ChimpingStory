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
        Invoke("CheckIfMusicShouldPlay" , 0.1f);
	}

    void CheckIfMusicShouldPlay()
    {
        if(GameManager.m_playerMutedSounds == 0 && !m_musicSource.isPlaying)
        {
            m_musicSource.Play();
        }

        Invoke("CheckIfMusicShouldPlay" , 0.1f);
    }
}
