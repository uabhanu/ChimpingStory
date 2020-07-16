using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] private Transform _raycastBottom , _raycastTop;
    //private void Update()
    //{
    //    if(Time.timeScale == 0)
    //    {
    //        return;
    //    }

    //    Debug.DrawLine(_raycastTop.position , _raycastBottom.position , Color.red);
    //    RaycastHit2D hit2D = Physics2D.Raycast(_raycastTop.position , _raycastBottom.position);

    //    if(hit2D)
    //    {
    //        Debug.Log("Killbox : " + hit2D.collider.gameObject);
    //        Kill(hit2D.collider.gameObject);
    //    }
    //}

    void Kill(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Killbox : " + collision2D.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("Killbox : " + collider2D.gameObject);
    }
}
