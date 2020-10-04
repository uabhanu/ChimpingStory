using UnityEngine.SceneManagement;
using UnityEngine;

public class WaterPuss : MonoBehaviour
{
    private Rigidbody2D _pussBody2D;

    [SerializeField] private Animator _pussAnim;
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private float _floatHeight;
    [SerializeField] private SwipeManagerSO _swipeManagerObject;


    private void Reset()
    {
        _pussBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _pussAnim.SetBool("Fly" , true);
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
        _swipeManagerObject.BhanuSwipes();

        if(_swipeManagerObject.IsSwiping(SwipeDirection.UP))
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