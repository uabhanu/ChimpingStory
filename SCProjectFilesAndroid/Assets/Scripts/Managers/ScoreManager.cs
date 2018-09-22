﻿using UnityEngine;

public class ScoreManager : MonoBehaviour 
{
    GameManager _gameManager;
    string _leaderboardID = "CgkIia2_r44YEAIQAQ";

    public static bool m_isTestingMode;
    public static float m_minHighScore , m_scoreValue;
	public static int m_playerLevel , m_polaroidsCount , m_supersCount;

	void Start()
	{
        //m_isTestingMode = true;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(m_isTestingMode)
        {
            m_polaroidsCount = 150;
            m_scoreValue = 4995f;
        }
        else
        {
            m_playerLevel = BhanuPrefs.GetPlayerLevel();
            m_polaroidsCount = BhanuPrefs.GetPolaroidsCount();
            m_supersCount = BhanuPrefs.GetSupers();

            m_scoreValue = BhanuPrefs.GetHighScore();
            _gameManager.m_highScoreValueText.text = m_scoreValue.ToString();
        }
	}
}
