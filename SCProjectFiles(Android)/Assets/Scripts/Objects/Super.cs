using UnityEngine;

public class Super : MonoBehaviour 
{
	BoxCollider2D m_superCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
    float m_maxY , m_minY;
	GameObject m_explosionPrefab , m_explosionSystemObj;
    int m_direction = 1;
    LevelCreator m_levelCreator;
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
        m_maxY = transform.position.y + 3.1f;
        m_minY = m_maxY - 3.1f;
        m_soundManager = FindObjectOfType<SoundManager>();
		m_superCollider2D = GetComponent<BoxCollider2D>();	
		m_superRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        transform.position = new Vector2(transform.position.x , transform.position.y + (m_direction * 0.03f));

        if(transform.position.y > m_maxY)
        {
            m_direction = -1;
        }

        if(transform.position.y < m_minY)
        {
            m_direction = 1;
        }

        if(m_chimpController.m_isSlipping || m_chimpController.m_isSuper)
        {
            m_superCollider2D.enabled = false;
            m_superRenderer.enabled = false;
        }

		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_chimpController.m_isSlipping && m_chimpController.m_isSuper && m_positionOnScreen.x >= 1)
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
            BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			m_soundManager.m_soundsSource.Play();
			SpawnExplosion();
			SpawnExplosion();
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
