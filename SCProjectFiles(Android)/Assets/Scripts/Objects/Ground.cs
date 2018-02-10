using UnityEngine;

public class Ground : MonoBehaviour 
{
	LandChimp m_landChimp;

	[HideInInspector] public BoxCollider2D m_groundCollider2D;
	[HideInInspector] public SpriteRenderer m_groundRenderer;

	void Start() 
	{
		m_landChimp = FindObjectOfType<LandChimp>();
		m_groundCollider2D = GetComponent<BoxCollider2D>();	
		m_groundRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

		if(m_landChimp.m_isSuper)
		{
			m_groundRenderer.enabled = false;
		}
		else
		{
			m_groundRenderer.enabled = true;
		}
	}
}
