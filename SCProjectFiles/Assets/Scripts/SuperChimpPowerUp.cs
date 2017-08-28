using System.Collections;
//using SVGImporter;
using UnityEngine;

public class SuperChimpPowerUp : MonoBehaviour 
{
	BoxCollider2D m_superchimpCollider2D;
	ChimpController m_chimpControlScript;
    float m_maxY;
	float m_minY;
	float m_startXPos , m_startYPos;
    Ground m_groundScript;
    int m_direction = 1;
	Rigidbody2D m_powerUpBody2D;
	ScoreManager m_scoreManagementScript;
	SpriteRenderer m_superchimpRenderer;

	void Start() 
	{
		m_chimpControlScript = GameObject.Find("Chimp").GetComponent<ChimpController>();
        m_groundScript = FindObjectOfType<Ground>();
		m_maxY = transform.position.y + 0.5f;
		m_minY = m_maxY - 1.0f;
        m_powerUpBody2D = GetComponent<Rigidbody2D>();
		m_scoreManagementScript = FindObjectOfType<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
        m_startXPos = transform.position.x;
        m_startYPos = transform.position.y;
		m_superchimpCollider2D = GetComponent<BoxCollider2D>();
		m_superchimpRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        m_powerUpBody2D.velocity = new Vector2(-m_groundScript.speed , m_powerUpBody2D.velocity.y);
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		transform.position = new Vector2(transform.position.x , transform.position.y + (m_direction * 0.05f));

		if(transform.position.y > m_maxY) 
		{
			m_direction = -1;
		}
			
		if(transform.position.y < m_minY) 
		{
			m_direction = 1;
		}

		if(m_chimpControlScript.m_superMode)
		{
			m_superchimpCollider2D.enabled = false;
			m_superchimpRenderer.enabled = false;
		}

		if(!m_chimpControlScript.m_superMode)
		{
			m_superchimpCollider2D.enabled = true; //IAP will make monkeynutTaken false again
			m_superchimpRenderer.enabled = true;
		}
	}
		
	IEnumerator SoundObjectTimer()
	{
		yield return new WaitForSeconds(0.4f);
		StartCoroutine("SoundObjectTimer");
	}
		
	void OnTriggerEnter2D(Collider2D col2D)
	{
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(m_startXPos , m_startYPos);
        }

        if(col2D.gameObject.tag.Equals("Player"))
		{
            transform.position = new Vector2(m_startXPos , m_startYPos);
            m_scoreManagementScript.m_superChimpScoreValue++;
		}
	}
}
