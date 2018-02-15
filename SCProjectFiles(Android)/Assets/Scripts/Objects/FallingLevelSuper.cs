using UnityEngine;

public class FallingLevelSuper : MonoBehaviour 
{
    GameManager m_gameManager;
    Rigidbody2D m_superBody2D;
	SoundManager m_soundManager;
    TopDownClouds m_topDownClouds;
	Vector3 m_positionOnScreen;

    [SerializeField] Vector2[] m_randomPositions;

	void Start() 
	{
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_superBody2D = GetComponent<Rigidbody2D>();
        m_topDownClouds = GameObject.Find("Clouds").GetComponent<TopDownClouds>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
        {
            return;
        }

        m_superBody2D.velocity = new Vector2(m_superBody2D.velocity.x , m_topDownClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
	}
		
	void OnTriggerEnter2D(Collider2D tri2D)
	{
		if(tri2D.gameObject.tag.Equals("Player"))
		{
            ScoreManager.m_supersCount++;
            BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			m_soundManager.m_soundsSource.Play();
            m_gameManager.BackToLandWithSuper();
        }
	}
}
