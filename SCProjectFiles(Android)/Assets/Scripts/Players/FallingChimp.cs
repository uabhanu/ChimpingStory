using UnityEngine;

public class FallingChimp : MonoBehaviour 
{
	GameManager m_gameManager;

    public static float m_moveAmount = 4f;

	void Start() 
	{
	    m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(SwipeManager.Instance.IsSwiping(SwipeDirection.LEFT))
        {
            Move(-m_moveAmount);
        }

        if(SwipeManager.Instance.IsSwiping(SwipeDirection.RIGHT))
        {
            Move(m_moveAmount);
        }
    }

    void CheatDeath()
    {
        m_gameManager.BackToLandLoseMenu();
    }

    public void Move(float amount)
	{
		float xPos = Mathf.Clamp(transform.position.x + amount , -8f , 8f);
		float yPos = transform.position.y;
		float zPos = transform.position.z;
		transform.position = new Vector3(xPos , yPos , zPos);
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Spikes"))
        {
            CheatDeath();
        }
    }
}
