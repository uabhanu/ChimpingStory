using UnityEngine;

public class Hurdle : MonoBehaviour
{
    Camera m_mainCamera;
    ChimpController m_chimpController;
    Collider2D m_hurdleCollider2D;
    SpriteRenderer m_hurdleRenderer;
    Vector3 m_positionOnScreen;

    void Start()
    {
        m_chimpController = FindObjectOfType<ChimpController>();
        m_hurdleCollider2D = GetComponent<Collider2D>();
        m_hurdleRenderer = GetComponent<SpriteRenderer>();
        m_mainCamera = FindObjectOfType<Camera>();
    }
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_chimpController.m_super)
        {
            m_hurdleCollider2D.enabled = false;
            m_hurdleRenderer.enabled = false;
        }

        if(!m_chimpController.m_super)
        {
            if(m_positionOnScreen.x > 1)
            {
                m_hurdleCollider2D.enabled = true;
                m_hurdleRenderer.enabled = true;
            }
        }
    }
}
