using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour 
{
	LevelCreator m_levelCreator;
	Rigidbody2D m_rockBody2D;

	void Start() 
	{
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_rockBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if (Time.timeScale == 0)
			return;

		m_rockBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_rockBody2D.velocity.y);
	}
}
