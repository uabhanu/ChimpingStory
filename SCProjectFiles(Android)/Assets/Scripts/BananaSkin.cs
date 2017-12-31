using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    Camera m_mainCamera;
    Collider2D m_skinCollider2D;
	ChimpController m_chimpController;
    SpriteRenderer m_skinRenderer;
    Vector3 m_positionOnScreen;

	void Start()
    {
        m_mainCamera = FindObjectOfType<Camera>();
		m_chimpController = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
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

        if (m_chimpController.m_slip || m_chimpController.m_super)
		{
			m_skinCollider2D.enabled = false;
			m_skinRenderer.enabled = false;
		}

		if(m_chimpController.m_slip && m_chimpController.m_super && (m_positionOnScreen.x >= 1 || m_positionOnScreen.x < 0))
		{
			m_skinCollider2D.enabled = true;
			m_skinRenderer.enabled = true;
		}
	}
}
