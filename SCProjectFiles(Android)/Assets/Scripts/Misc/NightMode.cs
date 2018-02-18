using System;
using UnityEngine;

public class NightMode : MonoBehaviour
{
    DateTime m_dateAndTime;
    SpriteRenderer m_spriteRenderer;

    [SerializeField] int m_hour;
    [SerializeField] Sprite m_nightSprite;

    void Reset()
    {
        m_hour = 18;
    }

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("NightCheck" , 0.1f);
    }

    void NightCheck()
    {
        m_dateAndTime = DateTime.Now;

        if(m_dateAndTime.Hour >= m_hour)
        {
            m_spriteRenderer.sprite = m_nightSprite;
        }

        Invoke("NightCheck" , 0.1f);
    }
}
