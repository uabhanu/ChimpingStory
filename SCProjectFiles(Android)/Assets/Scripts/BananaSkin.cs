using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    bool m_chimpSlipping;
    Collider2D m_skinCollider2D;
    ChimpController m_chimpControlScript;
    SpriteRenderer m_skinRenderer;

    [SerializeField] float m_activeInactiveTime;

	void Start()
    {
		m_chimpControlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
        m_skinCollider2D = GetComponent<Collider2D>();
        m_skinRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("ActiveInactiveRoutine");
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_chimpSlipping = m_chimpControlScript.m_slip;
	}

    IEnumerator ActiveInactiveRoutine()
    {
        yield return new WaitForSeconds(m_activeInactiveTime);

        if(m_chimpSlipping)
        {
            m_skinCollider2D.enabled = false;
            m_skinRenderer.enabled = false;
        }

        if(!m_chimpSlipping)
        {
            m_skinCollider2D.enabled = true;
            m_skinRenderer.enabled = true;
        }

        StartCoroutine("ActiveInactiveRoutine");
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            //Move away in x position
        }
    }
}
