using SelfiePuss.Events;
using SelfiePuss.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WaterPuss : MonoBehaviour
{
    private const float DEFAULT_MOVE_SPEED = 5.0f;

    //private Animator _pussAnim;
    private Rigidbody2D _pussBody2D;

    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _floatHeight;
    [SerializeField] private WaterLevelManager _waterLevelManager;


    private void Reset()
    {
        _currentMoveSpeed = DEFAULT_MOVE_SPEED;
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentMoveSpeed = DEFAULT_MOVE_SPEED;
        //_pussAnim = GetComponent<Animator>();
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        BhanuInput();
        Movement();
    }

    private void BhanuInput()
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

    private void Movement()
    {
        transform.Translate(Vector2.left * _currentMoveSpeed * Time.deltaTime , Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag.Equals("Enemy"))
        {
            SceneManager.LoadScene("LandRunner");
        }
    }
}