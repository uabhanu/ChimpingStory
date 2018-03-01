using UnityEngine;

public class FallingLevelClouds : MonoBehaviour 
{
    GameManager m_gameManager;

    public float m_moveUpSpeed;

    void Start() 
	{
		m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Invoke("BackToLandWin" , 30f);
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		transform.Translate(Vector2.up * m_moveUpSpeed * Time.deltaTime);

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
