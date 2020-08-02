using UnityEngine;

public class Polaroid : MonoBehaviour
{
    GameManager _gameManager;
	SoundManager m_soundManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            ScoreManager.m_polaroidsCount++;
            BhanuPrefs.SetPolaroidsCount(ScoreManager.m_polaroidsCount);
            _gameManager.m_PolaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
            ScoreManager.m_scoreValue += 25;
            _gameManager.m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_coinCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject);
        }
    }
}
