using UnityEngine;

public class FallingLevelSuper : MonoBehaviour 
{
    FallingLevelClouds m_fallingLevelClouds;
    GameManager m_gameManager;
	SoundManager m_soundManager;
	Vector3 m_positionOnScreen;

    [SerializeField] Vector2[] m_randomPositions;

	void Start() 
	{
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_fallingLevelClouds = GameObject.Find("Clouds").GetComponent<FallingLevelClouds>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
        {
            return;
        }

        transform.Translate(Vector2.up * m_fallingLevelClouds.m_moveUpSpeed * Time.deltaTime);

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

			if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            m_gameManager.BackToLandWithSuperMenu();
        }
	}
}
