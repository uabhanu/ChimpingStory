using UnityEngine;

public class Coin : MonoBehaviour 
{
    Rigidbody2D m_coinBody2D;
    SoundManager m_soundManager;
    TopDownClouds m_topDownClouds;

    [SerializeField] Vector2[] m_randomPositions;

	void Start() 
    {
        m_coinBody2D = GetComponent<Rigidbody2D>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		m_topDownClouds = GameObject.Find("Clouds").GetComponent<TopDownClouds>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
	}
	
	void Update() 
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        m_coinBody2D.velocity = new Vector2(m_coinBody2D.velocity.x , m_topDownClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            
            ScoreManager.m_scoreValue += 15;
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            m_soundManager.m_soundsSource.clip = m_soundManager.m_coin;
            m_soundManager.m_soundsSource.Play();
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }
}
