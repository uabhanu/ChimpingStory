using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    Ground m_groundScript;
    Rigidbody2D m_pillarBody;

    void Start()
    {
        m_groundScript = FindObjectOfType<Ground>();
        m_pillarBody = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);
        }
    }
}
