using UnityEngine;

public class Super : MonoBehaviour 
{
	BoxCollider2D m_superCollider2D;
	Camera m_mainCamera;
	LandChimp m_landChimp;
	GameObject m_explosionPrefab , m_explosionSystemObj;
	SoundManager m_soundManager;
	SpriteRenderer m_superRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
        m_explosionPrefab = Resources.Load("PF_Explosion") as GameObject;
		m_explosionSystemObj = GameObject.FindGameObjectWithTag("Explosion");
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		m_superCollider2D = GetComponent<BoxCollider2D>();	
		m_superRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(m_landChimp.m_isSuper)
        {
            m_superCollider2D.enabled = false;
            m_superRenderer.enabled = false;
        }

		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landChimp.m_isSlipping && m_landChimp.m_isSuper && m_positionOnScreen.x >= 1)
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
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			m_soundManager.m_soundsSource.Play();
			SpawnExplosion();
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
