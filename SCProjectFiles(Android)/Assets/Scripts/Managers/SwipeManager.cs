using UnityEngine;
using UnityEngine.SceneManagement;

public enum SwipeDirection
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    UP = 4,
    DOWN = 8,
}

public class SwipeManager : MonoBehaviour 
{
    float m_swipeResistanceX = 2.5f , m_swipeResistanceY = 2.5f;
    int m_currentScene;
    LandChimp m_landChimp;
    static SwipeManager instance;
    Vector3 m_touchPos;

    public static SwipeManager Instance{get{return instance;}}
    public SwipeDirection SwDirection{set;get;}

    void Start()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;
        instance = this;    

        if(m_currentScene == 1)
        {
            m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        }
    }

    void Update() 
	{
		if(GameManager.m_currentScene == 2 || Time.deltaTime == 0f)
		{
			return;
		}

        BhanuSwipes();
	}

    void BhanuSwipes()
    {
        SwDirection = SwipeDirection.NONE;

        if(Input.GetMouseButtonDown(0))
        {
            m_touchPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = m_touchPos - Input.mousePosition;

            if(Mathf.Abs(deltaSwipe.x) > m_swipeResistanceX)
            {
                SwDirection |= (deltaSwipe.x < 0) ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
            }

            if(Mathf.Abs(deltaSwipe.y) > m_swipeResistanceY)
            {
                SwDirection |= (deltaSwipe.y < 0) ? SwipeDirection.UP : SwipeDirection.DOWN;
            }
        }
    }

    public bool IsSwiping(SwipeDirection dir)
    {
        return(SwDirection & dir) == dir;
    }
}
