using UnityEngine;

public enum SwipeDirection
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    UP = 4,
    DOWN = 8,
}

[CreateAssetMenu]
public class SwipeManagerObject : ScriptableObject
{
    private float _swipeResistanceX = 2.5f , _swipeResistanceY = 2.5f;
    private Vector3 _touchPos;
    public SwipeDirection SwDirection{set;get;}

    public void BhanuSwipes()
    {
        SwDirection = SwipeDirection.NONE;

        if(Input.GetMouseButtonDown(0))
        {
            _touchPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = _touchPos - Input.mousePosition;

            if(Mathf.Abs(deltaSwipe.x) > _swipeResistanceX)
            {
                SwDirection |= (deltaSwipe.x < 0) ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
            }

            if(Mathf.Abs(deltaSwipe.y) > _swipeResistanceY)
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
