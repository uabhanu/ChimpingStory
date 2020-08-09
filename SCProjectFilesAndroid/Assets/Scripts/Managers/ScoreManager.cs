using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour 
{
    private int _sceneIndex;
    private string _leaderboardID = "CgkIia2_r44YEAIQAQ";

    [SerializeField] LandLevelManager _landLevelManager;
    [SerializeField] WaterLevelManager _waterLevelManager;

    public static bool m_isTestingMode;
    public static float m_minHighScore , m_scoreValue;
	public static int m_playerLevel , m_polaroidsCount , m_supersCount;

    //public int m_playerLevelValueDisplay; // This line is for testing purposes only

	void Start()
	{
        //m_isTestingMode = true;

        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(m_isTestingMode)
        {
            BhanuPrefs.DeleteAll();
            m_playerLevel = 0;
            //m_playerLevelValueDisplay = m_playerLevel; // This line is for testing purposes only
            m_polaroidsCount = 150;
            m_scoreValue = 4995f;
            _waterLevelManager.m_HighScoreValueText.text = m_scoreValue.ToString();
        }
        else
        {
            m_playerLevel = BhanuPrefs.GetPlayerLevel();
            //m_playerLevelValueDisplay = m_playerLevel; // This line is for testing purposes only
            m_polaroidsCount = BhanuPrefs.GetPolaroidsCount();
            m_supersCount = BhanuPrefs.GetSupers();
            m_scoreValue = BhanuPrefs.GetHighScore();
            
            if(_sceneIndex == 1) //TODO This is temporary fix and ScoreManager should take care of anything related to Score and not the LandLevelManager or WaterLevelManager
            {
                _landLevelManager.m_HighScoreValueText.text = m_scoreValue.ToString();
            }

            else if(_sceneIndex == 2)
            {
                _waterLevelManager.m_HighScoreValueText.text = m_scoreValue.ToString();
            }
        }
	}
}
