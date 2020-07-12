using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Kill(col2D.gameObject);
    }

    void Kill(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
