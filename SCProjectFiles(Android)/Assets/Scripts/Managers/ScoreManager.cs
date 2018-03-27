using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    string _leaderboardID = "CgkInMKFu8wYEAIQAQ";

    public static float m__leaderboardHighScore , m_minHighScore , m_scoreValue;
	public static int m_defaultSupersCount = 1 , m_supersCount;
    public static string m_scoreFromLeaderboard;
    public static Text m_scoreDisplay;

	void Start()
	{
        m_scoreDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
		m_scoreValue = BhanuPrefs.GetHighScore();
        m_scoreDisplay.text = m_scoreValue.ToString();
	}
}
