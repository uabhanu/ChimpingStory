using UnityEngine;

public class Killbox : MonoBehaviour
{
    void Kill(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag.Equals("Clouds"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Collectibles"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Mountains"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Platform"))
        {
            Kill(collider2D.gameObject);
        }
    }
}
