using UnityEngine;

public class Banana : MonoBehaviour
{
    AudioClip m_bananaSound;
    BananaEnabler m_bananaEnabler;
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
        m_bananaEnabler = FindObjectOfType<BananaEnabler>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpController = FindObjectOfType<ChimpController>();
        m_mainCamera = FindObjectOfType<Camera>();
		m_levelCreationScript = FindObjectOfType<LevelCreator>();
        m_soundsContainer = FindObjectOfType<SoundsContainer>();
        m_bananaSound = m_soundsContainer.m_bananaSound;
        m_startPos = transform.position.x;
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(m_chimpController.m_slip || m_chimpController.m_super)
		{
			m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
		}
			
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(!m_chimpController.m_slip && !m_chimpController.m_super && m_positionOnScreen.x > 1000)
		{
			m_bananaCollider2D.enabled = true;
			m_bananaRenderer.enabled = true;
		}
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            AudioSource.PlayClipAtPoint(m_bananaSound , transform.position , 1f);
            m_bananaCollider2D.enabled = false;
			m_bananaRenderer.enabled = false;
        }
    }
}
