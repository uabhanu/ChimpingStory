using UnityEngine;

public class WaterPuss : MonoBehaviour
{
    private const float DEFAULT_MOVE_SPEED = 5.0f;
    private const float DEFAULT_GRAVITY_SCALE = 0.5f;

    //private Animator _pussAnim;
    private bool _bCanFloat;
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
        _bCanFloat = true;
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
        if(_bCanFloat)
        {
            _pussBody2D.gravityScale = 0;
            _pussBody2D.velocity = new Vector2(_pussBody2D.velocity.x , _floatHeight);
            _bCanFloat = false;
        }
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
        transform.Translate(Vector2.left * _currentMoveSpeed * Time.deltaTime , Space.World);
    }

    private void OnCollisionEnter2D(Collision2D col2D)
    {
        if(col2D.gameObject.tag.Equals("Bottom"))
        {
            _bCanFloat = true;
        }

        if(col2D.gameObject.tag.Equals("Top"))
        {
            _pussBody2D.gravityScale = DEFAULT_GRAVITY_SCALE;
        }
    }
}
