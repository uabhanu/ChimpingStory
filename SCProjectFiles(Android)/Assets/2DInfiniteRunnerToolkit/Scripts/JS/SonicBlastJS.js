#pragma strict
#pragma downcast

public class SonicBlastJS extends MonoBehaviour 
{
    public var levelGenerator		: LevelGeneratorJS;			//A link to the level generator

    public var startPosition		: Vector2;					//The starting position of the sonic blast
    public var endPosition			: Vector2;					//The end position of the sonic blast

    public var time					: float;					//The traver time of the sonic wave

    private var lastObstacle		: Transform;				//The last obstace the blast hit
    private var paused				: boolean;					//True, if the level is paused

    //Used for initialisation
    function Start()
    {
        paused = false;
    }
    //Called when the wave collides with a coin or with an obstacle
    function OnTriggerEnter2D(other : Collider2D)
    {
        if (other.tag == "Obstacle")
        {
            levelGenerator.AddExplosionParticle(other.transform.position);
            other.GetComponent.<Renderer>().enabled = false;
            other.GetComponent.<Collider2D>().enabled = false;

            if (other.name != "Torpedo")
                lastObstacle = other.transform;
            else
                other.transform.Find("TorpedoFire").gameObject.SetActive(false);
        }
    }

    //Activate the sonic wave
    public function Activate()
    {
        lastObstacle = null;
        StartCoroutine(Move());
    }
    //Resets the sonic wave
    public function Reset()
    {
        StopAllCoroutines();
        this.transform.position = startPosition;
        this.gameObject.SetActive(false);
    }
    //Set the pause state of the level generator in time
    public function SetPauseState(state : boolean)
    {
        paused = state;
    }

    //Moves the sonic wave from the starting position to the end position in time
    private function Move()
    {
        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / time;	
			
        while (i < 1.0)
        {
            //If the game is not paused, increase t
            if (!paused)
            {
                i += Time.deltaTime * rate;
                this.transform.position = Vector3.Lerp(startPosition, endPosition, i);
            }

            //Wait for the end of frame
            yield;
        }
		
        Disable();
    }

    //Disable the obstacles of the last obstacle group with the wave collided
    private function Disable()
    {
        if (lastObstacle)
        {
            var obstacleParent : Transform = lastObstacle.parent;

            for (var item : Transform in obstacleParent)
            {
                if (item.tag == "Obstacle")
                {
                    item.GetComponent.<Renderer>().enabled = false;
                    item.GetComponent.<Collider2D>().enabled = false;
                }
            }
        }

        Reset();
    }
}
