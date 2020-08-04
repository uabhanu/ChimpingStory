using UnityEngine;

public class Killbox : MonoBehaviour
{
    void Kill(GameObject gameObject)
    {
        Destroy(gameObject); //TODO Use Object pooling later
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //if(collider2D.gameObject.layer > 8) //This is not working
        //{
        //    Kill(collider2D.gameObject);
        //}

        if(collider2D.gameObject.tag.Equals("Banana"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Clouds"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Platform"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Portal"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Rock")) //Killbox not destroying the object individually but destroying if it's a part of a prefab for some strange reason so for now, created RockVariant
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Super"))
        {
            Kill(collider2D.gameObject);
        }

        if(collider2D.gameObject.tag.Equals("Trees"))
        {
            Kill(collider2D.gameObject);
        }
    }
}
