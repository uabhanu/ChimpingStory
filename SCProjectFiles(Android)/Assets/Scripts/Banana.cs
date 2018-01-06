using UnityEngine;

public class Banana : MonoBehaviour
{
    AudioSource m_soundSource;
    BoxCollider2D m_bananaCollider2D;
	Camera m_mainCamera;
	ChimpController m_chimpController;
    float m_startPos;
    LevelCreator m_levelCreationScript;
	SoundsContainer m_soundsContainer;
	SpriteRenderer m_bananaRenderer;
    [SerializeField] Vector3 m_positionOnScreen;

    void Start()
    {
		m_bananaCollider2D = GetComponent<BoxCollider2D>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpController = FindObjectOfType<ChimpController>();
        m_mainCamera = FindObjectOfType<Camera>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();
        m_soundsContainer = FindObjectOfType<SoundsContainer>();
        m_soundSource = GetComponent<AudioSource>();
        m_startPos = transform.position.x;
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
	
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_chimpController.m_super)
        {
            m_bananaCollider2D.enabled = false;
            m_bananaRenderer.enabled = false;
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            m_soundSource.clip = m_soundsContainer.m_bananaSound;
            m_soundSource.Play();
            m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
        }
    }
}
