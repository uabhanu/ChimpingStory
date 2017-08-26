using System.Collections;
using UnityEngine;

public class BananaSkin : MonoBehaviour 
{
	BoxCollider2D m_skinCollider2D;
	ChimpController m_chimpControlScript;
    float m_startXPos , m_startYPos;
    Ground m_groundScript;
    Rigidbody2D m_skinBody2D;
	SpriteRenderer m_skinRenderer;

    [SerializeField] float randomValue;

	void Start() 
	{
		m_chimpControlScript = FindObjectOfType<ChimpController>();
		m_groundScript = FindObjectOfType<Ground>();
        m_skinBody2D = GetComponent<Rigidbody2D>();
		m_skinCollider2D = GetComponent<BoxCollider2D>();
		m_skinRenderer = GetComponent<SpriteRenderer>();
        m_startXPos = transform.position.x;
        m_startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        m_skinBody2D.velocity = new Vector2(-m_groundScript.speed , m_skinBody2D.velocity.y);
    }

    void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(m_chimpControlScript.m_chimpSlip || m_chimpControlScript.m_superMode)
		{
			m_skinCollider2D.enabled = false;
			m_skinRenderer.enabled = false;
		}

		if(!m_chimpControlScript.m_chimpSlip && !m_chimpControlScript.m_superMode)
		{
			m_skinCollider2D.enabled = true;
			m_skinRenderer.enabled = true;
		}
	}
			
	void OnTriggerEnter2D(Collider2D col2D)
	{
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(Random.Range(m_startXPos - randomValue , m_startXPos + randomValue) , m_startYPos);
        }

		if(col2D.gameObject.tag.Equals("Player"))
		{
			m_skinCollider2D.enabled = false;
			m_skinRenderer.enabled = false;
            transform.position = new Vector2(Random.Range(m_startXPos - randomValue , m_startXPos + randomValue) , m_startYPos);
        }
	}
}
