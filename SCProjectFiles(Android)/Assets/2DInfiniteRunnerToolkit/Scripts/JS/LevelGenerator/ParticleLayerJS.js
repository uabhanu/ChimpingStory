#pragma strict
#pragma downcast
import System.Collections.Generic;

public class ParticleLayerJS extends MonoBehaviour 
{
    public var container			: Transform;			//Contains the layer elements

    public var coinPrefab			: GameObject;			//The coin particle prefab, used to instantiate more if needed
    public var explosionPrefab		: GameObject;			//The explosion particle prefab, used to instantiate more if needed

    public var startingSpeed		: float;				//The starting speed of the layer

    private var activeElements		: List.<Transform>;		//Contains the active layer elements, which has not reached the spawnAt position
    private var inactive			: List.<Transform>;		//Contains the inactive layer elements

    private var speedMultiplier		: float;				//The current speed multiplier
    private var paused				: boolean;				//True, if the level is paused

    // Use this for initialization
    function Start()
    {
        speedMultiplier = 1;
        paused = true;

        activeElements = new List.<Transform>();
        inactive = new List.<Transform>();

        for (var child : Transform in container)
            inactive.Add(child);
    }
    // Update is called once per frame
    function Update()
    {
        if (!paused)
        {
            //Loop through the active elemets
            for (var item : Transform in activeElements)
            {
                //Move the item to the left
                item.transform.position -= Vector3.right * startingSpeed * speedMultiplier * Time.deltaTime;
            }
        }
    }

    //Adds the given element to the layer
    private function AddElement(item : Transform, pos :  Vector2)
    {
        item.transform.position = pos;
        item.gameObject.SetActive(true);

        inactive.Remove(item);
        activeElements.Add(item);

        StartCoroutine(PlayParticle(item, 2f));
    }
    //Removes the given element
    private function RemoveElement(item : Transform)
    {
        //Reset it's position, disable it, and remove it from the active elements
        item.transform.position = new Vector3(-12, item.transform.position.y, 0);

        //Remove it from the active elements, add it to the inactive, and disable it
        activeElements.Remove(item);
        inactive.Add(item);

        item.gameObject.SetActive(false);
    }
    //Generates a new explosion particle
    private function SpawnNewParticle(prefab : GameObject)
    {
        //Create a new object from the given prefab, and name it
        var newGo : GameObject = Instantiate(prefab) as GameObject;
        newGo.name = prefab.name;

        //Put it to the system
        newGo.transform.parent = this.transform;
        inactive.Add(newGo.transform);

        return newGo.transform;
    }

    //Enables the generator
    public function StartGenerating()
    {
        //Unpause the layer
        paused = false;
    }
    //Adds an explosion particle to the given position
    public function AddExplosion(position : Vector2)
    {
        //Find an unused explosion particle
        var item : Transform = Find(inactive, "ExplosionParticle");

        //If there is none, create a new
        if (item == null)
            item = SpawnNewParticle(explosionPrefab);

        //Add it to the level
        AddElement(item, position);      
    }
    //Adds a coin particle to the given position
    public function AddCoinParticle(position : Vector2)
    {
        //Find an unused coin particle
        var item : Transform = Find(inactive, "CoinParticle");

        //If there is none, create a new
        if (item == null)
            item = SpawnNewParticle(coinPrefab);

        //Add it to the level
        AddElement(item, position); 
    }
    //Resets the layer
    public function Reset()
    {
        paused = true;

        StopAllCoroutines();

        for (var item : Transform in activeElements)
        {
            item.transform.position = new Vector3(-12, item.transform.position.y, 0);
            item.gameObject.SetActive(false);

            inactive.Add(item);
        }

        activeElements.Clear();
    }
    //Sets scrolling state
    public function SetPauseState(state : boolean)
    {
        paused = state;
    }
    //Updates speed multiplier
    public function UpdateSpeedMultiplier(n : float)
    {
        speedMultiplier = n;
    }

    //Plays the particle then remove it
    private function PlayParticle(particle : Transform, time : float)
    {
        var p : ParticleSystem = particle.GetComponent("ParticleSystem") as ParticleSystem;
        p.Play();

        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
                i += Time.deltaTime * rate;

            //Wait for the end of frame
            yield;
        }

        p.Stop();
        p.Clear();

        RemoveElement(particle);
    }   
    private function Find(list : List.<Transform>, name : String)
    {
    	for (var element : Transform in list)
    	{
    		if (element.name == name)
    			return element;
    	}
    	
    	return null;
    }
}
