using UnityEngine;

public class Mountains : MonoBehaviour 
{
	LevelCreator m_levelCreator;
	Rigidbody2D m_mountainsBody2D;

	void Start() 
	{
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_mountainsBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		//m_mountainsBody2D.velocity = new Vector2(-m_levelCreationScript.m_gameSpeed / 4 , m_mountainsBody2D.velocity.y);
        transform.Translate(Vector2.left * (m_levelCreator.m_gameSpeed / 8));

		if(transform.position.x <= -43.2f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
