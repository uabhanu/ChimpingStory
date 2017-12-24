#pragma strict
#pragma downcast
import System.Collections.Generic;

public class TorpedoLayerJS extends MovingLayerJS
{
    public var indicatorContainer		: Transform;				//The container, which holds the indicators
	
    public var minPosY					: float;					//The minimum position for the indicator
    public var maxPosY					: float;					//The maximum position for the indiator
    public var indicatorTime			: float;					//The indicator display time

    public var minTorpedoes				: int;						//The minimum ammount of torpedoes to spawn in the same time
    public var maxTorpedoes				: int;						//The maximum ammount of torpedoes to spawn in the same time

    private var activeIndicators		: List.<GameObject>;		//Stores the active indicators
    private var inactiveIndicators		: List.<GameObject>;		//Stores the inactive indicators

	private var canSpawn				: boolean;

    // Use this for initialization
    public override function Start()
    {
    	canSpawn = true;
    
        activeIndicators = new List.<GameObject>();
        inactiveIndicators = new List.<GameObject>();

        for (var child : Transform in indicatorContainer)
            inactiveIndicators.Add(child.gameObject);
		
        super.Start();
    }
    
    //Spawns new torpedo elements
    public override function SpawnElement(inMiddle : boolean)
    {
    	if (!canSpawn)
    		return;
    
        //Calculate the number
        var toSpawn : int = Random.Range(minTorpedoes, maxTorpedoes + 1);

        for (var i : int = 0; i < toSpawn; i++)
        {
            //Get the first inactive indicator, and add it to the activate ones
            var indicator : GameObject= inactiveIndicators[0];
            inactiveIndicators.Remove(indicator);
            activeIndicators.Add(indicator);

            //Place it in a random Y position
            var yPos : float = Random.Range(minPosY, maxPosY);
            indicator.transform.position = new Vector3(indicator.transform.position.x, yPos, 0);

            //Activate it
            indicator.SetActive(true);
            StartCoroutine(ShowIndicator(indicator, yPos));
        }
    }
    //Resets the active indicators
    public override function Reset()
    {
        StopAllCoroutines();

        while (activeIndicators.Count > 0)
        {
            activeIndicators[0].SetActive(false);
            inactiveIndicators.Add(activeIndicators[0]);

            activeIndicators.RemoveAt(0);
        }

        super.Reset();
    }
    //Shows the torpedo indicator for a set ammount of time
    function ShowIndicator(indicator : GameObject, yPos : float)
    {
        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / indicatorTime;

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
            {
                i += Time.deltaTime * rate;
            }

            //Wait for the end of frame
            yield;
        }

        //Disable the indicator
        indicator.SetActive(false);    
        inactiveIndicators.Add(indicator);
        activeIndicators.Remove(indicator);

        //Spawn a torpedo on the Y position of the indicator
        SpawnTorpedo(yPos);
    }

	//Set the value of the canSpawn variable
    public function EnableSpawning(newState : boolean)
    {
        canSpawn = newState;
    }

    //Spawns a torpedo
    private function SpawnTorpedo(yPos : float)
    {
        //Get the first item from the inactive elements
        var torpedo : Transform = inactive[0];

        //Place the torpedo on the proper y position
        torpedo.position = new Vector3(torpedo.position.x, yPos, 0);

        torpedo.GetComponent.<Renderer>().enabled = true;
        torpedo.GetComponent.<Collider2D>().enabled = true;
        torpedo.transform.Find("TorpedoFire").gameObject.SetActive(true);

        //Activate it, and add it to the active elements
        inactive.Remove(torpedo);
        activeElements.Add(torpedo);
        torpedo.gameObject.SetActive(true);
    }
}
