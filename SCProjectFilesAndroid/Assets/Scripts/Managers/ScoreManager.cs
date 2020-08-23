using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    public int m_scoreValue;
    public Text m_HighScoreLabelText , m_HighScoreValueText;
    //public int m_playerLevelValueDisplay; // This line is for testing purposes only

	void Start()
	{
        m_scoreValue = BhanuPrefs.GetHighScore();
        m_HighScoreValueText.text = m_scoreValue.ToString();
	}
}
