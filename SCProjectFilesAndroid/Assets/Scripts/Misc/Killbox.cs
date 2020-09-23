using UnityEngine;

public class Killbox : MonoBehaviour
{
    void Kill(GameObject gameObject)
    {
        Destroy(gameObject); //TODO Use Object pooling later
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag.Equals("Destructible"))
        {
            Kill(col2D.gameObject);
        }
    }
}
