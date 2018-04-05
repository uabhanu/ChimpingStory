using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    string _leaderboardID = "CgkInMKFu8wYEAIQAQ";

    public static bool m_isTestingMode;
    public static float m_minHighScore , m_scoreValue;
	public static int m_bananasCollected , m_defaultSupersCount = 1 , m_supersCount;
    public static Text m_scoreDisplay;

	void Start()
	{
        m_isTestingMode = true; //TODO Remove this after testing finished
        m_bananasCollected = BhanuPrefs.GetBananasCollected();
        m_scoreDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();

        if(m_isTestingMode)
        {
            m_scoreValue = 4990f;
        }
        else
        {
            m_scoreValue = BhanuPrefs.GetHighScore();
        }
		
        m_scoreDisplay.text = m_scoreValue.ToString();
	}
}
