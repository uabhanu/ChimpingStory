#pragma strict

public class FollowJS extends MonoBehaviour 
{
    public var target		: Transform;                    //The target to follow
	
	// Update is called once per frame
	function Update () 
    {
        this.transform.position = target.position;	
	}
}