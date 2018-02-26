using UnityEngine;

public class Banana : MonoBehaviour
{
    BoxCollider2D m_bananaCollider2D;
    Camera m_mainCamera;
	LandChimp m_landChimp;
	SoundManager m_soundManager;
	SpriteRenderer m_bananaRenderer;
    Vector3 m_positionOnScreen;

    void Start()
    {
		m_bananaCollider2D = GetComponent<BoxCollider2D>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
            m_bananaCollider2D.enabled = false;
            m_bananaRenderer.enabled = false;
        }

        if(m_positionOnScreen.x >= 972)
        {
            m_bananaCollider2D.enabled = true;
            m_bananaRenderer.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            if(m_landChimp.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 75;
            }
            else
            {
                ScoreManager.m_scoreValue += 25;
            }

            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_bananaCollected;

			if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
        }
    }
}
