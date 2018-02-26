using UnityEngine;

public class SpikesBall : MonoBehaviour
{
    FallingLevelClouds m_fallingLevelClouds;
    Rigidbody2D m_spikesBallBody2D;
    SoundManager m_soundManager;

    [SerializeField] Vector2[] m_randomPositions;

    void Start()
    {
        m_fallingLevelClouds = GameObject.Find("Clouds").GetComponent<FallingLevelClouds>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_spikesBallBody2D = GetComponent<Rigidbody2D>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
    }

    void Update()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        m_spikesBallBody2D.velocity = new Vector2(m_spikesBallBody2D.velocity.x , m_fallingLevelClouds.m_moveUpSpeed);

        if(transform.position.y >= 5.68f)
        {
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            m_soundManager.m_soundsSource.clip = m_soundManager.m_spikesBallDeath;
            
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }
}
