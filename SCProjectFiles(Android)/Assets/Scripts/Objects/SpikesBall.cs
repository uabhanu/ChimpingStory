using UnityEngine;

public class SpikesBall : MonoBehaviour
{
    float m_startPosY;
    Rigidbody2D m_spikesBallBody2D;
    SoundManager m_soundManager;
    TopDownClouds m_topDownClouds;

    void Start()
    {
        m_spikesBallBody2D = GetComponent<Rigidbody2D>();
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

        m_spikesBallBody2D.velocity = new Vector2(m_spikesBallBody2D.velocity.x , m_topDownClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = new Vector2(Random.Range(-8 , 8) , m_startPosY);
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            m_soundManager.m_soundsSource.clip = m_soundManager.m_spikesBallDeath;
            m_soundManager.m_soundsSource.Play();
            transform.position = new Vector2(Random.Range(-8 , 8) , m_startPosY);
        }
    }
}
