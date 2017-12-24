using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    AudioSource m_soundsSource;
    BoxCollider2D m_bananaCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
    LevelCreator m_levelCreationScript;
	Rigidbody2D m_bananaBody2D;
	SoundsContainer m_soundsContainer;
	SpriteRenderer m_bananaRenderer;
    Vector3 m_positionOnScreen;

    void Start()
    {
        m_bananaBody2D = GetComponent<Rigidbody2D>();
		m_bananaCollider2D = GetComponent<BoxCollider2D>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpController = FindObjectOfType<ChimpController>();
        m_mainCamera = FindObjectOfType<Camera>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();  
		m_soundsContainer = FindObjectOfType<SoundsContainer>();
        m_soundsSource = GetComponent<AudioSource>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(m_chimpController.m_super)
		{
			m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
		}

		if(!m_chimpController.m_super)
		{
			m_bananaCollider2D.enabled = true;
			m_bananaRenderer.enabled = true;
		}
			
		m_bananaBody2D.velocity = new Vector2(-m_levelCreationScript.m_gameSpeed , m_bananaBody2D.velocity.y);
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
			m_soundsSource.clip = m_soundsContainer.m_bananaSound;
			m_soundsSource.Play();
            Destroy(gameObject);
        }
    }
}
