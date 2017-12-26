using UnityEngine;

public class Ball : MonoBehaviour
{
    bool m_gameStarted;
    GameManager m_gameManager;
    [SerializeField] Paddle m_paddleController;
    Rigidbody2D m_ballBody2D;
    Vector3 m_paddleToBallDistance;

    void Start()
    {
        m_ballBody2D = GetComponent<Rigidbody2D>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_paddleToBallDistance = transform.position - m_paddleController.transform.position;    
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(!m_gameStarted)
        {
            transform.position = m_paddleController.transform.position + m_paddleToBallDistance; //Lock ball position relative to paddle position every frame

            if(Input.GetMouseButtonDown(0))
            {
                m_gameStarted = true; //Use of this boolean is to undo the Lock ball position every frame so ball actually moves around now which is correct
                m_ballBody2D.velocity = new Vector2(4f , 15f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        Vector2 randomBounce = new Vector2(Random.Range(0f , 0.2f) , Random.Range(0f , 0.2f));
        m_ballBody2D.velocity += randomBounce;
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("GameOver"))
        {
            m_gameManager.BrickNotDestroyed();
        }
    }
}
