using UnityEngine;

public class Hurdle : MonoBehaviour
{
    BoxCollider2D m_hurdleCollider2D;
    Camera m_mainCamera;
    ChimpController m_chimpController;
    LevelCreator m_levelCreator;
    Rigidbody2D m_hurdleBody2D;
    SpriteRenderer m_hurdleRenderer;
    Vector3 m_positionOnScreen;

    void Start()
    {
        m_chimpController = FindObjectOfType<ChimpController>();
        m_levelCreator = FindObjectOfType<LevelCreator>();
        m_mainCamera = FindObjectOfType<Camera>();
        m_hurdleBody2D = GetComponent<Rigidbody2D>();
        m_hurdleCollider2D = GetComponent<BoxCollider2D>();
        m_hurdleRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        m_hurdleBody2D.velocity = new Vector2(-m_levelCreator.m_gameSpeed , m_hurdleBody2D.velocity.y);
        m_positionOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);

        if(m_positionOnScreen.x < 0)
        {
            Destroy(gameObject);
        }
    }
}
