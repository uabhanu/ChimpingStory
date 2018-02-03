using System.Collections;
using UnityEngine;

public class TopDownClouds : MonoBehaviour 
{
	float m_landRunnerTime = 30f;
	GameManager m_gameManager;
	Rigidbody2D m_cloudsBody2D;

    [HideInInspector] public float m_moveUpSpeed = 7.5f;

    void Start() 
	{
		m_cloudsBody2D = GetComponent<Rigidbody2D>();
		m_gameManager = FindObjectOfType<GameManager>();
		StartCoroutine("LandRunnerRoutine");
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		m_cloudsBody2D.velocity = new Vector2(m_cloudsBody2D.velocity.x , m_moveUpSpeed);

		if(transform.position.y >= 21.47f)
		{
			transform.position = new Vector3(transform.position.x , 0f , transform.position.z);
		}
	}

	IEnumerator LandRunnerRoutine()
	{
		yield return new WaitForSeconds(m_landRunnerTime);
		m_gameManager.BackToLandWinMenu();
	}
}
