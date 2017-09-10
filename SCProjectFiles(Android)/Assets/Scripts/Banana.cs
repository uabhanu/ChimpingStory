using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Collider2D m_bananaCollider2D;
    ChimpController m_chimpControlScript;
    float m_bananaInCameraView;
    SpriteRenderer m_bananaRenderer;
    Vector3 m_positionOnScreen;

    [SerializeField] float m_timeToActive;

	void Start()
    {
        m_bananaCollider2D = GetComponent<Collider2D>();
        m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpControlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
        StartCoroutine("ActiveRoutine");
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
	}

    IEnumerator ActiveRoutine()
    {
        yield return new WaitForSeconds(m_timeToActive);

        if(m_positionOnScreen.x >= 765.3f)
        {
            m_bananaCollider2D.enabled = true;
            m_bananaRenderer.enabled = true;
        }

        StartCoroutine("ActiveRoutine");
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            m_bananaCollider2D.enabled = false;
            m_bananaRenderer.enabled = false;
        }
    }
}
