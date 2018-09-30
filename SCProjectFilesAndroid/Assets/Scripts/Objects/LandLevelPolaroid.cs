using UnityEngine;

public class LandLevelPolaroid : MonoBehaviour
{
    BoxCollider2D m_polaroidCollider2D;
    Camera m_mainCamera;
    GameManager _gameManager;
	LandChimp m_landChimp;
	SoundManager m_soundManager;
	SpriteRenderer m_polaroidRenderer;
    Vector3 m_positionOnScreen;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_polaroidCollider2D = GetComponent<BoxCollider2D>();
		m_polaroidRenderer = GetComponent<SpriteRenderer>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landChimp.m_isSuper)
        {
            m_polaroidCollider2D.enabled = false;
            m_polaroidRenderer.enabled = false;
        }

        if(!m_landChimp.m_isSuper && m_positionOnScreen.x >= 972)
        {
            m_polaroidCollider2D.enabled = true;
            m_polaroidRenderer.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            ScoreManager.m_polaroidsCount++;
            BhanuPrefs.SetPolaroidsCount(ScoreManager.m_polaroidsCount);
            GameManager.m_polaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();

            if(m_landChimp.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 75;
            }
            else
            {
                ScoreManager.m_scoreValue += 25;
            }

            _gameManager.m_highScoreValueText.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_coinCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            m_polaroidCollider2D.enabled = false;
			m_polaroidRenderer.enabled = false;
            //LevelCreator.m_polaroidObj = null;
        }
    }
}
