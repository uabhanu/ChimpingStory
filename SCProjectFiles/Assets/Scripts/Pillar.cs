using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    float m_startXPos , m_startYPos;
    Ground m_groundScript;
    Rigidbody2D m_pillarBody;

    [SerializeField] float m_randomValue;

    void Start()
    {
        m_groundScript = FindObjectOfType<Ground>();
        m_pillarBody = GetComponent<Rigidbody2D>();
        m_startXPos = transform.position.x;
        m_startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        m_pillarBody.velocity = new Vector2(-m_groundScript.speed , m_pillarBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(Random.Range(m_startXPos - m_randomValue , m_startXPos + m_randomValue) , m_startYPos);
        }
    }
}
