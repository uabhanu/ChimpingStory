using UnityEngine;

public class Rock : MonoBehaviour 
{
    Camera m_mainCamera;
	LandChimp m_landChimp;
	Collider2D m_rockCollider2D;
    GameObject m_explosionPrefab , m_explosionSystemObj;
	SpriteRenderer m_rockRenderer;
	Vector3 m_positionOnScreen;

    [SerializeField] Vector2[] m_randomPositions;

	void Start() 
	{
        m_explosionPrefab = Resources.Load("PF_Explosion") as GameObject;
        m_explosionSystemObj = GameObject.FindGameObjectWithTag("Explosion");
        m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		m_rockCollider2D = GetComponent<Collider2D>();
		m_rockRenderer = GetComponent<SpriteRenderer>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        transform.Translate(Vector2.left * (LevelCreator.m_gameSpeed * 1.5f) * Time.deltaTime);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

		if(m_positionOnScreen.x < 0)
		{
			Destroy(gameObject);
		}

        if(!m_landChimp.m_isSuper && m_positionOnScreen.x >= 0.99f) //Try >= 765.3f if this doesn't work
        {
            m_rockCollider2D.enabled = false;
            m_rockRenderer.enabled = false;
        }
    }
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            SpawnExplosion();
        }
    }

	void SpawnExplosion()
	{
		ScoreManager.m_scoreValue += 100;
        ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);

		if(m_explosionSystemObj == null)
		{
			m_explosionSystemObj = Instantiate(m_explosionPrefab);
			Explosion.m_explosionType = "Rock";
			Destroy(gameObject);
		}
	}
}
