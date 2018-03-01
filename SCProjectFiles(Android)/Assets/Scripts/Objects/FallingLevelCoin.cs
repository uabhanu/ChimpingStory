using UnityEngine;

public class FallingLevelCoin : MonoBehaviour 
{
    FallingLevelClouds m_fallingLevelClouds;
    SoundManager m_soundManager;

    [SerializeField] Vector2[] m_randomPositions;

	void Start() 
    {
        m_fallingLevelClouds = GameObject.Find("Clouds").GetComponent<FallingLevelClouds>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
	}
	
	void Update() 
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        transform.Translate(Vector2.up * m_fallingLevelClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            
            ScoreManager.m_scoreValue += 25;
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            m_soundManager.m_soundsSource.clip = m_soundManager.m_coinCollected;
            
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }
}
