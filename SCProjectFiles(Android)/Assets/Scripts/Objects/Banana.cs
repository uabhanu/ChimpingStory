using UnityEngine;

public class Banana : MonoBehaviour
{
    BoxCollider2D m_bananaCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
    LevelCreator m_levelCreationScript;
	SoundManager m_soundManager;
	SpriteRenderer m_bananaRenderer;
    [SerializeField] Vector3 m_positionOnScreen;

    void Start()
    {
		m_bananaCollider2D = GetComponent<BoxCollider2D>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpController = FindObjectOfType<ChimpController>();
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

        if(m_chimpController.m_isSuper)
        {
            m_bananaCollider2D.enabled = false;
            m_bananaRenderer.enabled = false;
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            if(m_chimpController.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 15;
                BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
                ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            }
            else
            {
                ScoreManager.m_scoreValue += 5;
                BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
                ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            }
				
			m_soundManager.m_soundsSource.clip = m_soundManager.m_bananaCollected;
			m_soundManager.m_soundsSource.Play();
            m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
        }
    }
}
