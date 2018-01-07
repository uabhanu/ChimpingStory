using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    [SerializeField] Text m_scoreDisplay;

	public static float m_scoreValue;
	public static int m_supersSpawned;

    void Start()
    {
        m_scoreValue = BhanuPrefs.GetHighScore();
        m_supersSpawned = BhanuPrefs.GetSupers();
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
