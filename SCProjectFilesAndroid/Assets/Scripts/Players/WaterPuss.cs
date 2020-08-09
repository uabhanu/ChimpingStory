using UnityEngine;

public class WaterPuss : MonoBehaviour
{
    private const float DEFAULT_MOVE_SPEED = 5.0f;

    //private Animator _pussAnim;
    //private bool _bIsFloating;
    private Rigidbody2D _pussBody2D;

    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _floatHeight;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private WaterLevelManager _waterLevelManager;


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
            Float();
        }
    }

    public void Float()
    {
        _pussBody2D.velocity = new Vector2(_pussBody2D.velocity.x , _floatHeight);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetMoveSpeed()
    {
        return _currentMoveSpeed;
    }

    void Movement()
    {
        transform.Translate(Vector2.left * _currentMoveSpeed * Time.deltaTime , Space.Self);
    }
}
