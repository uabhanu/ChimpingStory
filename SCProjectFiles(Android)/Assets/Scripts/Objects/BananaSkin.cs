using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    Camera m_mainCamera;
    Collider2D m_skinCollider2D;
	LandChimp m_landChimp;
    SpriteRenderer m_skinRenderer;
    Vector3 m_positionOnScreen;

	void Start()
    {
		m_landChimp = GameObject.FindGameObjectWithTag("Player").GetComponent<LandChimp>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_skinCollider2D = GetComponent<Collider2D>();
        m_skinRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if (m_landChimp.m_isSlipping || m_landChimp.m_isSuper)
		{
			m_skinCollider2D.enabled = false;
			m_skinRenderer.enabled = false;
		}

		if(m_landChimp.m_isSlipping && m_landChimp.m_isSuper && (m_positionOnScreen.x >= 1 || m_positionOnScreen.x < 0))
		{
			m_skinCollider2D.enabled = true;
			m_skinRenderer.enabled = true;
		}
	}
}
