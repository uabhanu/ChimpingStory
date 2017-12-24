#pragma strict
import System.Collections.Generic;

public class LevelGeneratorJS extends MonoBehaviour
{
    public var guiManager			: GUIManagerJS;						//A link to the GUI Manager
    public var missionManager		: MissionManagerJS;					//A link to the Mission Manager

    public var movingLayers			: List.<MovingLayerJS>;				//Holds the moving layer
    public var scrollingLayers		: List.<ScrollingLayerJS>;			//Holds the scrolling layers

    public var torpedoLayer			: TorpedoLayerJS;					//A link to the torpedo layer
    public var powerupLayer 		: PowerupLayerJS;					//A link to the powerup layer
    public var particleLayer 		: ParticleLayerJS;					//A link to the particle player

    public var hangar				: GameObject;						//Holds the hangar
    public var playTriggerer		: GameObject;						//Holds the play triggerer

    public var speedIncreaseRate	: float;							//The scrolling speed increase rate per second
    public var distance				: float;							//The current distance

    private var hangarStartPosX		: float;							//The hangar starting position

    private var speedMultiplier		: float;							//Holds the speed multiplier
    private var lastSpeedMultiplier	: float;							//Holds the last speed multiplier

    private var paused				: boolean;							//True, if the game is paused
    private var canModifySpeed		: boolean;							//True, if the generator can modify the current scrolling speed

    // Used for initialization
    function Start()
    {
        speedMultiplier = 1;
        paused = true;
    }
    // Update is called once per frame
    function Update()
    {
        //If the game is not paused
        if (!paused)
        {
            //If the speed can be modified
            if (canModifySpeed)
            {
                //Increase scrolling speed
                speedMultiplier += speedIncreaseRate * Time.deltaTime;

                powerupLayer.UpdateSpeedMultiplier(speedMultiplier);
                torpedoLayer.UpdateSpeedMultiplier(speedMultiplier);
            }

            //Increase distance
            distance += 10 * speedMultiplier * Time.deltaTime;

            //Pass speed multiplier to the layers
            for (var item : MovingLayerJS in movingLayers)
                item.UpdateSpeedMultiplier(speedMultiplier);

            for (var item : ScrollingLayerJS in scrollingLayers)
                item.UpdateSpeedMultiplier(speedMultiplier);

            particleLayer.UpdateSpeedMultiplier(speedMultiplier);

            //Update GUI and Mission Manager
            guiManager.UpdateDistance(parseInt(distance));
            missionManager.DistanceEvent(parseInt(distance));
        }
    }

    //Moves the hangar to the left, out of the view.
    function MoveHangar(posX : float, time : float)
    {
        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        var startPos : Vector3 = hangar.transform.position;
        var endPos : Vector3 = new Vector3(posX, hangar.transform.position.y, 0);

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
            {
                i += Time.deltaTime * rate * speedMultiplier;
                hangar.transform.position = Vector3.Lerp(startPos, endPos, i);
            }

            //Wait for the end of frame
            yield;
        }
    }
    //Changes the speed multiplier to newValue in time
    function ChangeScrollingMultiplier(newValue : float, time : float, enableIncrease : boolean)
    {
        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        var startValue : float = speedMultiplier;

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
            {
                i += Time.deltaTime * rate;
                speedMultiplier = Mathf.Lerp(startValue, newValue, i);
            }

            //Wait for the end of frame
            yield;
        }

        //If we stopped the generator because of a crash
        if (newValue == 0)
        {
            //Notify the mission manager
            missionManager.CrashEvent(parseInt(distance));
        }
    }

	//Adds a coin particle to the level
    public function AddCoinParticle(contactPoint : Vector2)
    {
        particleLayer.AddCoinParticle(contactPoint);
	}
    //Adds an explosion particle to the level
    public function AddExplosionParticle(contactPoint : Vector2)
    {
        particleLayer.AddExplosion(contactPoint);
	}
    //Resume the generator after a revive
	public function ContinueGeneration()
    {
    	torpedoLayer.EnableSpawning(true);
        powerupLayer.EnableSpawning(true);
        
        StartCoroutine(ChangeScrollingMultiplier(lastSpeedMultiplier, 0.5f, true));
	}
    //Resets the level generator
	public function Reset()
    {
        paused = true;
        canModifySpeed = false;

        speedMultiplier = 1;
        distance = 0;

        StopAllCoroutines();

        playTriggerer.SetActive(true);

        for (var item : MovingLayerJS in movingLayers)
            item.Reset();

        for (var item : ScrollingLayerJS in scrollingLayers)
            item.SetPauseState(true);

		torpedoLayer.EnableSpawning(true);
        powerupLayer.EnableSpawning(true);

        torpedoLayer.Reset();
        powerupLayer.Reset();
        particleLayer.Reset();

        hangar.transform.position = new Vector3(hangarStartPosX, hangar.transform.position.y, 0);
	}
    //Starts the level Generator
	public function StartToGenerate()
    {
        paused = false;
        canModifySpeed = true;

        playTriggerer.SetActive(false);

        for (var item : MovingLayerJS in movingLayers)
            item.StartGenerating();

        for (var item : ScrollingLayerJS in scrollingLayers)
            item.SetPauseState(false);

        torpedoLayer.StartGenerating();
        powerupLayer.StartGenerating();
        particleLayer.StartGenerating();

        hangarStartPosX = hangar.transform.position.x;
        StartCoroutine(MoveHangar(-30, 4.75f));
	}
    //Stops the level generaton in time
	public function StopGeneration(time : float)
    {
        lastSpeedMultiplier = speedMultiplier;
        canModifySpeed = false;

		torpedoLayer.EnableSpawning(false);
        powerupLayer.EnableSpawning(false);

        StartCoroutine(ChangeScrollingMultiplier(0, time, false));
	}
    //Set the pause state of the level generator in time
    public function SetPauseState(state : boolean)
    {
        paused = state;

        for (var item : MovingLayerJS in movingLayers)
            item.SetPauseState(state);

        for (var item : ScrollingLayerJS in scrollingLayers)
            item.SetPauseState(state);

        torpedoLayer.SetPauseState(state);
        powerupLayer.SetPauseState(state);
        particleLayer.SetPauseState(state);
    }
    //Return the current distance as an int
    public function CurrentDistance()
    {
        return parseInt(distance);
    }
    //Starts the extra speed powerup effect
    public function StartExtraSpeed(newSpeed : float)
    {
        lastSpeedMultiplier = speedMultiplier;
        canModifySpeed = false;

        speedMultiplier = newSpeed;

        powerupLayer.UpdateSpeedMultiplier(speedMultiplier);
        torpedoLayer.UpdateSpeedMultiplier(speedMultiplier);
    }
    //Stops the extra speed powerup effect
    public function EndExtraSpeed()
    {
        speedMultiplier = lastSpeedMultiplier;
        canModifySpeed = true;
    }
}