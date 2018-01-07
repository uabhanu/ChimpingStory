using UnityEngine;

public class BananasMover : MonoBehaviour 
{
    Camera m_mainCamera;
    float m_startPos;
    LevelCreator m_levelCreationScript;
    Rigidbody2D m_bhanuBody2D;
    Vector3 m_positionOnScreen;

    void Start() 
    {
        m_bhanuBody2D = GetComponent<Rigidbody2D>();
        m_levelCreationScript = FindObjectOfType<LevelCreator>();
        m_mainCamera = FindObjectOfType<Camera>();
        m_startPos = transform.position.x;
	}
	
	void Update() 
    {
        if(Time.timeScale == 0f)
		{
			return;
		}
        
        m_bhanuBody2D.velocity = new Vector2(-m_levelCreationScript.m_gameSpeed , m_bhanuBody2D.velocity.y);
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_positionOnScreen.x < 0)
        {
            BananasSpawner.m_bananasCount--;
            Destroy(gameObject);
        }
	}
}
