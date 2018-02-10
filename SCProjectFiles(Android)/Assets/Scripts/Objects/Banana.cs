using UnityEngine;

public class Banana : MonoBehaviour
{
    BoxCollider2D m_bananaCollider2D;
	Camera m_mainCamera;
	LandChimp m_landChimp;
    LevelCreator m_levelCreationScript;
	SoundManager m_soundManager;
	SpriteRenderer m_bananaRenderer;
    [SerializeField] Vector3 m_positionOnScreen;

    void Start()
    {
		m_bananaCollider2D = GetComponent<BoxCollider2D>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_landChimp = FindObjectOfType<LandChimp>();
        m_mainCamera = FindObjectOfType<Camera>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();
        m_soundManager = FindObjectOfType<SoundManager>();
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
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            if(m_landChimp.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 15;
            }
            else
            {
                ScoreManager.m_scoreValue += 5;
            }

            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_bananaCollected;
			m_soundManager.m_soundsSource.Play();
            m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
        }
    }
}
