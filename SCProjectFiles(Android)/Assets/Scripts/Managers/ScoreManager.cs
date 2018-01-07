using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    [SerializeField] Text m_scoreDisplay;

	public static float m_scoreValue;
	public static int m_defaultSupersCount = 1 , m_supersCount;

	void Start()
	{
		m_scoreValue = BhanuPrefs.GetHighScore();
	}

    void Update() 
    {
	    if(Time.timeScale == 0)
        {
            return;
        }

		m_scoreDisplay.text = m_scoreValue.ToString();
	}
}
