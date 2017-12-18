using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour 
{
	Camera m_mainCamera;
	LevelCreator m_levelCreator;
	Rigidbody2D m_rockBody2D;
	Vector3 m_positionOnScreen;

	void Start() 
	{
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mainCamera = FindObjectOfType<Camera>();
		m_rockBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if (Time.timeScale == 0)
			return;

		m_rockBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_rockBody2D.velocity.y);
		m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

		if(m_positionOnScreen.x < 0)
		{
			Destroy(gameObject);
		}
	}
}
