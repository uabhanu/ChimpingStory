using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    bool m_chimpSlipping;
    Camera m_mainCamera;
    Collider2D m_skinCollider2D;
	ChimpController m_chimpController;
    float m_skinInCameraView;
    SpriteRenderer m_skinRenderer;
    Vector3 m_positionOnScreen;

    [SerializeField] float m_timeToActiveInactive;

	void Start()
    {
        m_mainCamera = FindObjectOfType<Camera>();
		m_chimpController = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
        m_skinCollider2D = GetComponent<Collider2D>();
        m_skinRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("ActiveInactiveRoutine");
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
            return;

		if(m_chimpController.m_super)
		{
			m_skinCollider2D.enabled = false;
			m_skinRenderer.enabled = false;
		}

		if(!m_chimpController.m_super)
		{
			m_skinCollider2D.enabled = true;
			m_skinRenderer.enabled = true;
		}
        

        m_chimpSlipping = m_chimpController.m_slip;
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);
	}

    IEnumerator ActiveInactiveRoutine()
    {
        yield return new WaitForSeconds(m_timeToActiveInactive);

        if(m_chimpSlipping)
        {
            m_skinCollider2D.enabled = false;
            m_skinRenderer.enabled = false;
        }

        if(!m_chimpSlipping && m_positionOnScreen.x >= 765.3f)
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
            m_skinCollider2D.enabled = false;
            m_skinRenderer.enabled = false;
        }
    }
}
