using UnityEngine;

public class Clouds : MonoBehaviour 
{
	LevelCreator m_levelCreator;
	Rigidbody2D m_cloudsBody2D;

	void Start() 
	{
		m_cloudsBody2D = GetComponent<Rigidbody2D>();
		m_levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		m_cloudsBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed / 8 , m_cloudsBody2D.velocity.y);

		if(transform.position.x <= -43.2f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
