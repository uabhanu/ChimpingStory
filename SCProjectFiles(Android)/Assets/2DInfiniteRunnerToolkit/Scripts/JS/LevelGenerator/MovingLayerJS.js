#pragma strict
#pragma downcast
import System.Collections.Generic;

public class MovingLayerJS extends MonoBehaviour 
{
    public var container			: Transform;				//Contains the layer elements

    public var startingSpeed		: float;					//The starting speed of the layer

    public var startAt				: float;					//The spawned layer elements starts on this X coordinate
    public var spawningRate			: float;					//Spawn a new layer element at this rate
    public var resetAt				: float;					//Reset a layer element, when it reaches this global X coordinates

    public var delayBeforeFirst		: float;					//How much delay should be applied before the spawn of the first element

    public var childCheck			: boolean;					//Enable it, if the child elements of a layer can be disabled by outside event (like when a player collides with an obstacle)
    public var generateInMiddle		: boolean;					//Enable it, if you want an element to be generated in zero X pos at the beginning of the level

    protected var activeElements	: List.<Transform>;			//Contains the active layer elements, which has not reached the spawnAt position
    protected var inactive			: List.<Transform>;         //Contains the inactive layer elements

    protected var speedMultiplier	: float;					//The current speed multiplier

    protected var paused			: boolean;					//True, if the level is paused
    protected var removeLast		: boolean;                  //True, if the first element has passed the reset position

	// Use this for initialization
	public function Start () 
    {
        speedMultiplier = 1;
        paused = true;

        activeElements = new List.<Transform>();
        inactive = new List.<Transform>();

        for (var child : Transform in container)
            inactive.Add(child);

        if (generateInMiddle)
            SpawnElement(true);
	}
	// Update is called once per frame
	function Update () 
    {
        if (!paused)
        {
            //Loop through the active elemets
            for (var item : Transform in activeElements)
            {
                //Move the item to the left
                item.transform.position -= Vector3.right * startingSpeed * speedMultiplier * Time.deltaTime;

                //If the item has reached the reset position
                if (item.transform.position.x < resetAt)
                    removeLast = true;
            }

            //Remove the first element, if needed
            if (removeLast)
            {
                removeLast = false;
                RemoveElement(activeElements[0]);
            }
        }
	}

    //Removes the first element
    private function RemoveElement(item : Transform)
    {
        //If this is a special layer, like obstacles, check and activate every child
        if (childCheck)
            EnableChild(item);

        //Remove it from the active elements, add it to the inactive, and disable it
        activeElements.Remove(item);
        inactive.Add(item);

        item.gameObject.SetActive(false);

        //Reset it's position
        item.transform.position = new Vector3(startAt, item.transform.position.y, 0);
    }
    //Loops trough the children of a layer element, and enables them
    private function EnableChild(element : Transform)
    {
        //Loop trough the childrens, and activate their renderer and collider
        for (var item : Transform in element)
        {
            item.GetComponent.<Renderer>().enabled = true;
            item.GetComponent.<Collider2D>().enabled = true;
        }
    }
    //Spawns a new layer element with a delay
    private function SpawnDelayedElement(time : float)
    {
        //Declare starting variables
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        //Wait for time
        while (i < 1.0)
        {
            if (!paused)
                i += Time.deltaTime * rate;

            yield;
        }

        //Spawn the element
        StartCoroutine(Generator());
    }
    //Spawn new layer elements at the given rate
    private function Generator()
    {
        //Spawn a new element
        SpawnElement(false);

        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / spawningRate;

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
            {
                i += (Time.deltaTime * rate) * speedMultiplier;
            }

            //Wait for the end of frame
            yield;
        }

        //Restart the generator
        StartCoroutine("Generator");
    }

    //Starts the generation of this layer
    public function StartGenerating()
    {
        //Unpause the generator, and spawn the first element
        paused = false;
        StartCoroutine(SpawnDelayedElement(delayBeforeFirst));
    }
    //Spawns a new layer element
    public function SpawnElement(inMiddle : boolean)
    {
        //Get a random item from the inactive elements
        var item : Transform = inactive[Random.Range(0, inactive.Count)];

        //If the item is needed at zero X position
        if (inMiddle)
        {
            //Place it
            item.transform.position = new Vector3(0, item.transform.position.y, 0);
        }

        //Activate it, and add it to the active elements
        item.gameObject.SetActive(true);
        inactive.Remove(item);
        activeElements.Add(item);
    }
    //Sets the pause state
    public function SetPauseState(newState : boolean)
    {
        paused = newState;
    }
    //Removes every item from the level
    public function Reset()
    {
        paused = true;

        StopAllCoroutines();

        for (var item : Transform in activeElements)
        {
            item.transform.position = new Vector3(startAt, item.transform.position.y, 0);

            if (childCheck)
                EnableChild(item);

            item.gameObject.SetActive(false);
            inactive.Add(item);
        }

        activeElements.Clear();

        if (generateInMiddle)
            SpawnElement(true);
    }
  
    //Updates the speed multiplier
    public function UpdateSpeedMultiplier(n : float)
    {
        speedMultiplier = n;
    }
}