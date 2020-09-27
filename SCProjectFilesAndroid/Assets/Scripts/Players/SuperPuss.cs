using UnityEngine;

public class SuperPuss : MonoBehaviour
{
    private const float DEFAULT_MOVE_SPEED = 15.0f;

    //private Animator _pussAnim;
    private Rigidbody2D _pussBody2D;

    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _flyHeight;
    [SerializeField] private SuperLevelManager _superLevelManager;


    void Reset()
    {
        _currentMoveSpeed = DEFAULT_MOVE_SPEED;
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _currentMoveSpeed = DEFAULT_MOVE_SPEED;
        //_pussAnim = GetComponent<Animator>();
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        BhanuInput();
        Movement();
    }

    void BhanuInput()
    {
        if(SwipeManager.Instance.IsSwiping(SwipeDirection.UP))
        {
            Fly();
        }
    }

    public void Fly()
    {
        _pussBody2D.velocity = new Vector2(_pussBody2D.velocity.x , _flyHeight);
    }

    public float GetMoveSpeed()
    {
        return _currentMoveSpeed;
    }

    void Movement()
    {
        transform.Translate(Vector2.right * _currentMoveSpeed * Time.deltaTime , Space.World);
    }
}
