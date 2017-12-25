using UnityEngine;

public class Super : MonoBehaviour 
{
	BoxCollider2D m_superCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
	LevelCreator m_levelCreator;
	Rigidbody2D m_superBody2D;
	SpriteRenderer m_superRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
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

        if(m_chimpController.m_slip)
        {
            m_superCollider2D.enabled = false;
            m_superRenderer.enabled = false;
        }

		m_superBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_superBody2D.velocity.y);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

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
