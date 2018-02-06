 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour 
{
	ChimpController m_chimpController;

	[HideInInspector] public BoxCollider2D m_groundCollider2D;
	[HideInInspector] public SpriteRenderer m_groundRenderer;

	void Start() 
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_groundCollider2D = GetComponent<BoxCollider2D>();	
		m_groundRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if (Time.timeScale == 0)
			return;

		if(m_chimpController.m_isSuper)
		{
			//m_groundCollider2D.enabled = false;
			m_groundRenderer.enabled = false;
		}

		if(!m_chimpController.m_isSuper)
		{
			//m_groundCollider2D.enabled = true;
			m_groundRenderer.enabled = true;
		}
	}
}
