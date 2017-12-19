using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuGame : MonoBehaviour 
{
	[SerializeField] GameObject m_mediaManagerObj , m_mediaManagerPrefab;

	void Start() 
	{
		m_mediaManagerObj = GameObject.FindGameObjectWithTag("MediaManager");

		if(m_mediaManagerObj == null)
		{
			m_mediaManagerObj = Instantiate(m_mediaManagerPrefab);
		}
	}
}
