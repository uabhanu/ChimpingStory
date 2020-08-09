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
        m_musicSource.Play();
	}
}
