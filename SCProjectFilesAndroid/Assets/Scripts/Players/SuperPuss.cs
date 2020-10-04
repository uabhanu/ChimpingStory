using UnityEngine;

public class SuperPuss : MonoBehaviour
{
    private Rigidbody2D _pussBody2D;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _flyHeight;
    [SerializeField] private SwipeManagerSO _swipeManagerObject;


    void Reset()
    {
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _animator.SetBool("Fly" , true);
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
        _swipeManagerObject.BhanuSwipes();

        if(_swipeManagerObject.IsSwiping(SwipeDirection.UP))
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
