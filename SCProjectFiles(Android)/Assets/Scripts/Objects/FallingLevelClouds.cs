using UnityEngine;

public class FallingLevelClouds : MonoBehaviour 
{
	GameManager m_gameManager;
	Rigidbody2D m_cloudsBody2D;

    [HideInInspector] public float m_moveUpSpeed = 7.5f;

    void Start() 
	{
		m_cloudsBody2D = GetComponent<Rigidbody2D>();
		m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Invoke("BackToLandWin" , 30f);
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		m_cloudsBody2D.velocity = new Vector2(m_cloudsBody2D.velocity.x , m_moveUpSpeed);

		if(transform.position.y >= 19.75f)
		{
			transform.position = new Vector3(transform.position.x , 0f , transform.position.z);
		}
	}

	void BackToLandWin()
	{
		m_gameManager.BackToLandWinMenu();
	}
}
