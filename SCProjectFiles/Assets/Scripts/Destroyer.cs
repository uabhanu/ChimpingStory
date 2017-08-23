using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
