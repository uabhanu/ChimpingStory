#pragma strict

public class TorpedoIndicatorJS extends MonoBehaviour 
{
	public var speed 		: float = 5.0f;						//Vertical speed
	public var distance		: float = 1.0f;						//Vertical distance
	
	var offset				: float = 0.0f;						//Offset
	
	var originalPos			: float = 0;						//The original position of the indicator
	var nextPos 			: Vector3 = new Vector3();			//The next position of the indicator
	
	var paused 				: boolean = false;					//Is the game paused
	
	//Called when the object is enabled
    function OnEnable()
	{
		//Set original position, and set pause to false
		originalPos = this.transform.position.y;
		paused = false;
	}
	//Called at every frame
	function Update () 
	{	
		//If the game is not paused
		if (!paused)
		{
			//Calculate offset
			offset = (1 + Mathf.Sin(Time.time * speed)) * distance / 2.0f;
			
			//Modify next position
			nextPos = this.transform.position;
			nextPos.y = originalPos + offset;
			
			//Apply next position
			this.transform.position = nextPos;
		}
	}
	//Pause the indicator
	public function SetPauseState(newState : boolean)
	{
        paused = newState;
	}
}
