using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Camera m_mainCamera;
    LevelCreator m_levelCreationScript;
	Rigidbody2D m_bananaBody2D;
    Vector3 m_positionOnScreen;

    void Start()
    {
        m_bananaBody2D = GetComponent<Rigidbody2D>();
        m_mainCamera = FindObjectOfType<Camera>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();    
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
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
            Destroy(gameObject);
        }
    }
}
