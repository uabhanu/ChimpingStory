using UnityEngine;

public class Super : MonoBehaviour 
{
	BoxCollider2D m_superCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
	GameObject m_explosionPrefab , m_explosionSystemObj;
	LevelCreator m_levelCreator;
	Rigidbody2D m_superBody2D;
	SoundManager m_soundManager;
	SpriteRenderer m_superRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_explosionPrefab = Resources.Load("PF_Explosion") as GameObject;
		m_explosionSystemObj = GameObject.FindGameObjectWithTag("Explosion");
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
		m_soundManager = FindObjectOfType<SoundManager>();
		m_superBody2D = GetComponent<Rigidbody2D>();
		m_superCollider2D = GetComponent<BoxCollider2D>();	
		m_superRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(m_chimpController.m_slip || m_chimpController.m_super)
        {
            m_superCollider2D.enabled = false;
            m_superRenderer.enabled = false;
        }

		m_superBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_superBody2D.velocity.y);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_chimpController.m_slip && m_chimpController.m_super && m_positionOnScreen.x >= 1)
        {
            m_superCollider2D.enabled = true;
            m_superRenderer.enabled = true;
        }

		if(m_positionOnScreen.x < 0)
		{
			Destroy(gameObject);
		}
	}
		
	void OnTriggerEnter2D(Collider2D tri2D)
	{
		if(tri2D.gameObject.tag.Equals("Player"))
		{
            ScoreManager.m_supersCount--;
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			m_soundManager.m_soundsSource.Play();
			SpawnExplosion();
            BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            Destroy(gameObject);
        }
	}

	void SpawnExplosion()
	{
		if(m_explosionSystemObj == null)
		{
			m_explosionSystemObj = Instantiate(m_explosionPrefab);
			Explosion.m_explosionType = "Super";
			Destroy(gameObject);
		}
	}
}
