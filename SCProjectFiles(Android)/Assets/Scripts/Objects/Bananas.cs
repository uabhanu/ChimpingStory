using UnityEngine;

public class Bananas : MonoBehaviour 
{
    float m_startPosY;
    Rigidbody2D m_bananasBody2D;
    SoundManager m_soundManager;
    TopDownClouds m_topDownClouds;

	void Start() 
    {
        m_bananasBody2D = GetComponent<Rigidbody2D>();
        m_soundManager = FindObjectOfType<SoundManager>();
        m_startPosY = transform.position.y;
		m_topDownClouds = FindObjectOfType<TopDownClouds>();
	}
	
	void Update() 
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        m_bananasBody2D.velocity = new Vector2(m_bananasBody2D.velocity.x , m_topDownClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = new Vector2(transform.position.x , Random.Range(m_startPosY - 5 , m_startPosY + 6));
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            
            ScoreManager.m_scoreValue += 30;
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            m_soundManager.m_soundsSource.clip = m_soundManager.m_bananaCollected;
            m_soundManager.m_soundsSource.Play();
            transform.position = new Vector2(transform.position.x , Random.Range(m_startPosY - 5 , m_startPosY + 6));
        }
    }
}
