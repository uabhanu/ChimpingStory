﻿using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    string _leaderboardID = "CgkInMKFu8wYEAIQAQ";

	public static float m__leaderboardHighScore , m_scoreValue;
	public static int m_defaultSupersCount = 1 , m_supersCount;
    public static string m_myScores = "Leaderboard:\n";
    public static Text m_scoreDisplay;

	void Start()
	{
        m_scoreDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
		m_scoreValue = BhanuPrefs.GetHighScore();
        m_scoreDisplay.text = m_scoreValue.ToString();

        PlayGamesPlatform.Instance.LoadScores(_leaderboardID , scores =>
        {
            if(scores.Length > 0)
            {
                foreach(IScore score in scores)
                {
                    m_myScores += "\t" + score.userID + "" + score.formattedValue + "" + score.date + "\n";
                }
            }
        });
	}
}
