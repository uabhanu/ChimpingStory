#pragma strict

public class PowerupLayerJS extends MovingLayerJS 
{
    public var minDepth		: float;                      //The minimum depth for the powerup to spawn
    public var maxDepth		: float;                      //The maximum depth for the powerup to spawn
	
	private var canSpawn	: boolean = true;
	
    //Spawns a new element
    public override function SpawnElement(inMiddle : boolean)
    {
    	if (!canSpawn)
    		return;
    
        //Get a random item from the inactive elements
        var item : Transform = inactive[Random.Range(0, inactive.Count)];

        //Place it
        item.transform.position = new Vector3(startAt, Random.Range(maxDepth, minDepth), 0);

        item.GetComponent.<Renderer>().enabled = true;
        item.GetComponent.<Collider2D>().enabled = true;
        item.transform.Find("Trail").gameObject.SetActive(true);

        //Activate it, and add it to the active elements
        item.gameObject.SetActive(true);
        inactive.Remove(item);
        activeElements.Add(item);
    }
    
    //Set the value of the canSpawn variable
    public function EnableSpawning(newState : boolean)
    {
        canSpawn = newState;
    }
}
