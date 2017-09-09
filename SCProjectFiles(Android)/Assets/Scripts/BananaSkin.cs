using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    bool m_chimpSlipping;
    Collider2D m_skinCollider2D;
    ChimpController m_chimpControlScript;
    float m_skinInCameraView;
    SpriteRenderer m_skinRenderer;

    [SerializeField] float m_activeInactiveTime;

	void Start()
    {
		m_chimpControlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
        m_skinCollider2D = GetComponent<Collider2D>();
        m_skinRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        m_chimpSlipping = m_chimpControlScript.m_slip;

        if(m_chimpSlipping)
        {
            m_skinCollider2D.enabled = false;
            m_skinRenderer.enabled = false;
        }

        //If Slipping is false and skin is outside camera view, make collider and renderer true
	}
}
