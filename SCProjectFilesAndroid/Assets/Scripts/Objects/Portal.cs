using UnityEngine;

public class Portal : MonoBehaviour 
{
	Collider2D m_portalCollider2D;
	Camera m_mainCamera;
	LandChimp m_landChimp;
	SpriteRenderer m_portalRenderer;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		m_portalCollider2D = GetComponent<Collider2D>();	
		m_portalRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landChimp.m_isSuper)
        {
            m_portalCollider2D.enabled = false;
            m_portalRenderer.enabled = false;
			LevelCreator.m_portalObj = null;
        }

        else if(!m_landChimp.m_isSuper && m_positionOnScreen.x >= 972)
        {
            m_portalCollider2D.enabled = true;
            m_portalRenderer.enabled = true;
        }
    }
}
