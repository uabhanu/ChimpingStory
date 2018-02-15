using UnityEngine;

public class BananasMover : MonoBehaviour 
{
    Camera m_mainCamera;
    LevelCreator m_levelCreator;
    Rigidbody2D m_bhanuBody2D;
    Vector3 m_positionOnScreen;

    void Start() 
    {
        m_bhanuBody2D = GetComponent<Rigidbody2D>();
        m_levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	void Update() 
    {
        if(Time.timeScale == 0f)
		{
			return;
		}
        
        m_bhanuBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_bhanuBody2D.velocity.y);
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_positionOnScreen.x < 0)
        {
            BananasSpawner.m_bananasCount--;
            Destroy(gameObject);
        }
	}
}
