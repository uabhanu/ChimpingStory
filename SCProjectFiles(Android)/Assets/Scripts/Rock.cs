using UnityEngine;

public class Rock : MonoBehaviour 
{
    [SerializeField] bool m_toggleExplosion;
    Camera m_mainCamera;
	ChimpController m_chimpController;
	Collider2D m_rockCollider2D;
    GameObject m_explosionPrefab , m_explosionSystemObj;
    LevelCreator m_levelCreator;
	Rigidbody2D m_rockBody2D;
	SpriteRenderer m_rockRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_explosionPrefab = Resources.Load("PF_RockExplosion") as GameObject;
        m_explosionSystemObj = GameObject.FindGameObjectWithTag("Explosion");
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
		m_rockBody2D = GetComponent<Rigidbody2D>();
		m_rockCollider2D = GetComponent<Collider2D>();
		m_rockRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

		m_rockBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_rockBody2D.velocity.y);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

		if(m_positionOnScreen.x < 0)
		{
			Destroy(gameObject);
		}

        if (!m_chimpController.m_super && m_positionOnScreen.x >= 0.99f) //Try >= 765.3f if this doesn't work)
        {
            m_rockCollider2D.enabled = false;
            m_rockRenderer.enabled = false;
        }
    }

    void Explosion()
    {
        if(m_explosionSystemObj == null)
        {
            m_explosionSystemObj = Instantiate(m_explosionPrefab);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            Explosion();
        }
    }
}
