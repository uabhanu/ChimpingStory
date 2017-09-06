using System.Collections;
using UnityEngine;

public class Clouds : MonoBehaviour 
{
	LevelCreator m_levelCreationScript;
	Rigidbody2D m_cloudsBody2D;

	void Start() 
	{
		m_cloudsBody2D = GetComponent<Rigidbody2D>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		m_cloudsBody2D.velocity = new Vector2(-m_levelCreationScript.m_gameSpeed / 8 , m_cloudsBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
