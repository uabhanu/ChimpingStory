using UnityEngine;

public class MusicManagerSpawner : MonoBehaviour 
{
	[SerializeField] GameObject m_musicManagerObj , m_musicManagerPrefab;

	void Start() 
	{
		m_musicManagerObj = GameObject.FindGameObjectWithTag("MusicManager");

		if(m_musicManagerObj == null)
		{
			m_musicManagerObj = Instantiate(m_musicManagerPrefab);
		}
	}
}
