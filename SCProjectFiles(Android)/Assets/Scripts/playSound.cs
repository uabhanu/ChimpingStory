using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
	AudioSource[] m_audiSources;
	
	void Start()
    {
		m_audiSources = GetComponents<AudioSource>();
	}
	
	public void SoundToPlay(string type)
    {
		switch(type)
        {
	        case "Jump":
		        m_audiSources[0].Play();
	        break;

	        case "Pickup":
		        m_audiSources[1].Play();
	        break;

	        case "EnemyDeath":
		        m_audiSources[2].Play();
	        break;

	        case "FallDeath":
		        m_audiSources[3].Play();
	        break;
		}
	}
}
