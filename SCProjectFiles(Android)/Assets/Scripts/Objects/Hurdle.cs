using UnityEngine;

public class Hurdle : MonoBehaviour
{
    Camera m_mainCamera;
    LandChimp m_landChimp;
    Collider2D m_hurdleCollider2D;
    SpriteRenderer m_hurdleRenderer;
    [SerializeField] Vector3 m_positionOnScreen;

    void Start()
    {
        m_hurdleCollider2D = GetComponent<Collider2D>();
        m_hurdleRenderer = GetComponent<SpriteRenderer>();
        m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landChimp.m_isSlipping || m_landChimp.m_isSuper)
        {
            m_hurdleCollider2D.enabled = false;
            m_hurdleRenderer.enabled = false;
        }
        else
        {
            if(m_positionOnScreen.x >= 972)
            {
                m_hurdleCollider2D.enabled = true;
                m_hurdleRenderer.enabled = true;
            }
        }
    }
}
