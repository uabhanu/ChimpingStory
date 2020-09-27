using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ScoreManagerObject : ScriptableObject
{
    public int m_scoreValue;
    public Text m_HighScoreLabelText , m_HighScoreValueText;
    //public int m_playerLevelValueDisplay; // This line is for testing purposes only

    public void GetScoreItems()
    {
        m_HighScoreLabelText = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<Text>();
        m_HighScoreValueText = GameObject.FindGameObjectWithTag("ScoreValue").GetComponent<Text>();
        m_scoreValue = BhanuPrefs.GetHighScore();
        m_HighScoreValueText.text = m_scoreValue.ToString();
    }
}
