using UnityEngine;

public class Paddle : MonoBehaviour
{
    Ball m_ball;
    [SerializeField] bool m_autoPlay;
    [SerializeField] float m_maxXConstraint , m_minXConstraint;

    void Reset()
    {
        m_maxXConstraint = 15.5f;
        m_minXConstraint = 0.5f;
    }

    void Start()
    {
		m_ball = FindObjectOfType<Ball>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(!m_autoPlay)
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
	}

    void AutoPlay()
    {
        Vector3 ballPosition = m_ball.transform.position;
        Vector3 paddlePosition = new Vector3(0.5f , transform.position.y , 0f);
        paddlePosition.x = Mathf.Clamp(ballPosition.x , m_minXConstraint , m_maxXConstraint);
        transform.position = paddlePosition;
    }

    void MoveWithMouse()
    {
        float mousePosition = (Input.mousePosition.x / Screen.width) * 16;
        Vector3 paddlePosition = new Vector3(0.5f , transform.position.y , 0f);
        paddlePosition.x = Mathf.Clamp(mousePosition , m_minXConstraint , m_maxXConstraint);
        transform.position = paddlePosition;
    }
}
