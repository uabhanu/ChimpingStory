using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    SpriteRenderer m_cloudsRenderer;

    [SerializeField] bool m_nightTime;
    [SerializeField] Sprite[] m_dayAndNightSprites;

    void Start()
    {
        m_cloudsRenderer = GetComponent<SpriteRenderer>();

        if(!m_nightTime)
        {
            m_cloudsRenderer.sprite = m_dayAndNightSprites[0];
        }
        else
        {
            m_cloudsRenderer.sprite = m_dayAndNightSprites[1];
        }
        
    }
}
