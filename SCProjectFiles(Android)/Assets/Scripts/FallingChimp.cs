using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingChimp : MonoBehaviour 
{
	float m_landRunnerTime = 2.5f;

	[SerializeField] Image m_backToLandLoseMenuImage , m_backToLandWinMenuImage , m_continueButtonLoseImage , m_continueButtonWinImage , m_pauseButtonImage , m_pauseMenuImage , m_resumeButtonImage;
	[SerializeField] Text m_backToLandLose , m_backToLandWin , m_highScoreTextDisplay , m_highScoreValueDisplay;

	void Start() 
	{
		StartCoroutine("LandRunnerRoutine");
	}

	void Update() 
	{
		
	}

	IEnumerator LandRunnerRoutine()
	{
		yield return new WaitForSeconds(m_landRunnerTime);
		BackToLandWin();
	}

	public void BackToLandWin()
	{
		m_backToLandWin.enabled = true;
		m_backToLandWinMenuImage.enabled = true;
		m_continueButtonWinImage.enabled = true;

		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
		m_pauseButtonImage.enabled = false;
		Time.timeScale = 0;
	}
}
