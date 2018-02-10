using UnityEngine;

public class Portal : MonoBehaviour 
{
	Collider2D m_portalCollider2D;
	Camera m_mainCamera;
	LandChimp m_landChimp;
    //float m_maxY , m_minY;
    //int m_direction = 1;
	LevelCreator m_levelCreator;
	Rigidbody2D m_portalBody2D;
	SpriteRenderer m_portalRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_landChimp = FindObjectOfType<LandChimp>();
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
        //m_maxY = transform.position.y + 3.1f;
        //m_minY = m_maxY - 3.1f;
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

        //transform.position = new Vector2(transform.position.x , transform.position.y + (m_direction * 0.03f));

        //if(transform.position.y > m_maxY)
        //{
        //    m_direction = -1;
        //}

        //if(transform.position.y < m_minY)
        //{
        //    m_direction = 1;
        //}

        if(m_landChimp.m_isSlipping || m_landChimp.m_isSuper)
        {
            m_portalCollider2D.enabled = false;
            m_portalRenderer.enabled = false;
        }

		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landChimp.m_isSlipping && m_landChimp.m_isSuper && m_positionOnScreen.x >= 1)
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
