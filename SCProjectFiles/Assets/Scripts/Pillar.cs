using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    Ground m_groundScript;
    int m_index;
    Rigidbody2D m_pillarBody;

    [SerializeField] float[] m_xPositions;

    void Start()
    {
        m_groundScript = FindObjectOfType<Ground>();
        m_index = Random.Range(0 , m_xPositions.Length);
        m_pillarBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(m_xPositions[m_index] , transform.position.y , transform.position.z);
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
