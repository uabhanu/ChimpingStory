﻿using UnityEngine;

public class BananaSkin : MonoBehaviour
{
    Camera m_mainCamera;
    Collider2D m_skinCollider2D;
	LandPuss m_landPuss;
    SpriteRenderer m_skinRenderer;
    Vector3 m_positionOnScreen;

	void Start()
    {
		m_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_skinCollider2D = GetComponent<Collider2D>();
        m_skinRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_landPuss.m_isSlipping || m_landPuss.m_isSuper)
        {
            m_skinCollider2D.enabled = false;
            m_skinRenderer.enabled = false;
            //LevelCreator.m_bananaSkinObj = null;
        }
        else
        {
            if(m_positionOnScreen.x >= 972)
            {
                m_skinCollider2D.enabled = true;
                m_skinRenderer.enabled = true;
            }
        }
    }
}
