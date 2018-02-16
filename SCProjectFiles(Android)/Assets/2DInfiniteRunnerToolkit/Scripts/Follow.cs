using UnityEngine;

public class Follow : MonoBehaviour 
{
    public Transform target;                    //The target to follow
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = target.position;	
	}
}
