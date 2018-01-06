using UnityEngine;

public class AVMSpawner : MonoBehaviour 
{
	[SerializeField] GameObject m_avManagerObj , m_avManagerPrefab;

	void Start() 
	{
		m_avManagerObj = GameObject.FindGameObjectWithTag("AVManager");

		if(m_avManagerObj == null)
		{
			m_avManagerObj = Instantiate(m_avManagerPrefab);
		}
	}
}
