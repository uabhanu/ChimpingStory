using System.Collections;
using UnityEngine;

public class Mountains : MonoBehaviour 
{
	LevelCreator m_levelCreationScript;
	Rigidbody2D m_mountainsBody2D;

	void Start() 
	{
		m_levelCreationScript = FindObjectOfType<LevelCreator>();
		m_mountainsBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		m_mountainsBody2D.velocity = new Vector2(-m_levelCreationScript.m_gameSpeed / 4 , m_mountainsBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
