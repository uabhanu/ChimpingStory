using UnityEngine;

public class Portal : MonoBehaviour 
{
	Collider2D m_portalCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
	LevelCreator m_levelCreator;
	Rigidbody2D m_portalBody2D;
	SpriteRenderer m_portalRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
		m_portalBody2D = GetComponent<Rigidbody2D>();
		m_portalCollider2D = GetComponent<Collider2D>();	
		m_portalRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(m_chimpController.m_slip || m_chimpController.m_super)
        {
            m_portalCollider2D.enabled = false;
            m_portalRenderer.enabled = false;
        }

		m_portalBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_portalBody2D.velocity.y);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_chimpController.m_slip && m_chimpController.m_super && m_positionOnScreen.x >= 1)
        {
            m_portalCollider2D.enabled = true;
            m_portalRenderer.enabled = true;
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
            Destroy(gameObject);
        }
	}
}
