using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingChimp : MonoBehaviour 
{
	float m_landRunnerTime = 2.5f;
	GameManager m_gameManager;

	void Start() 
	{
		m_gameManager = FindObjectOfType<GameManager>();
		StartCoroutine("LandRunnerRoutine");
	}
		
	IEnumerator LandRunnerRoutine()
	{
		yield return new WaitForSeconds(m_landRunnerTime);
		m_gameManager.BackToLandWinMenu();
	}
}
