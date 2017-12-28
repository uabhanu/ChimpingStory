using System;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    DateTime m_dateAndTime;
    SpriteRenderer m_spriteRenderer;

    [SerializeField] Sprite[] m_dayAndNightSprites;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_dateAndTime = DateTime.Now;

        if(m_dateAndTime.Hour >= 18)
        {
            m_spriteRenderer.sprite = m_dayAndNightSprites[1];
        }

        else if(m_dateAndTime.Hour < 18)
        {
            m_spriteRenderer.sprite = m_dayAndNightSprites[0];
        }
    }
}
