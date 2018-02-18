using UnityEngine;

public class SpikesBall : MonoBehaviour
{
    Rigidbody2D m_spikesBallBody2D;
    SoundManager m_soundManager;
    TopDownClouds m_topDownClouds;

    [SerializeField] Vector2[] m_randomPositions;

    void Start()
    {
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_spikesBallBody2D = GetComponent<Rigidbody2D>();
        m_topDownClouds = GameObject.Find("Clouds").GetComponent<TopDownClouds>();
        transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
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
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            m_soundManager.m_soundsSource.clip = m_soundManager.m_spikesBallDeath;
            m_soundManager.m_soundsSource.Play();
            transform.position = m_randomPositions[Random.Range(0 , m_randomPositions.Length)];
        }
    }
}
