#pragma strict

public class PowerupManagerJS extends MonoBehaviour 
{
    public var levelGenerator			: LevelGeneratorJS;			//A link to the Level Generator
    public var playerManager			: PlayerManagerJS;			//A link to the Player Manager
    public var sonicBlast				: SonicBlastJS;				//A link to the Sonic Blast

    public var extraSpeedFactor			: float;					//The scrolling speed during the Extra Speed powerup
    public var extraSpeedLength			: float;					//The duration of the extra speed powerup

    private var powerupUsed				: boolean;					//True, if the player used a powerup in this run
    private var paused					: boolean;					//True, if the level is paused

    // Used for initialization
    function Start()
    {
        powerupUsed = false;
        paused = false;
    }

    //Changes pause state to state
    public function SetPauseState(state : boolean)
    {
        paused = state;
        sonicBlast.SetPauseState(state);
    }
    //Return true, if a powerup can be used
    public function CanUsePowerup()
    {
        return playerManager.CanUsePowerup();
    }
    //Returns true, if a powerup was used
    public function PowerupUsed()
    {
        return powerupUsed;
    }
    //Resets the variables
    public function Reset()
    {
        powerupUsed = false;
        paused = false;
    }

    //Activate the extra speed powerup
    public function ExtraSpeed()
    {
        powerupUsed = true;
        StartCoroutine(ExtraSpeedEffect());
    }
    //Activate the extra speed powerup
    public function Shield()
    {
        powerupUsed = true;
        playerManager.RaiseShield();
    }
    //Activate the extra speed powerup
    public function SonicBlast()
    {
        powerupUsed = true;
        sonicBlast.gameObject.SetActive(true);
        sonicBlast.Activate();
    }
    //Revives the player, and launches a Sonic Blast
    public function Revive()
    {
        powerupUsed = true;

        sonicBlast.gameObject.SetActive(true);
        sonicBlast.Activate();
    }

    //Activates the extra speed for the given duration, then disables it
    private function ExtraSpeedEffect()
    {
        levelGenerator.StartExtraSpeed(extraSpeedFactor);
        playerManager.ActivateExtraSpeed();

        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / extraSpeedLength;

        while (i < 1.0)
        {
            //If the game is not paused, increase t
            if (!paused)
                i += Time.deltaTime * rate;

            //Wait for the end of frame
            yield;
        }

        levelGenerator.EndExtraSpeed();
        playerManager.DisableExtraSpeed();
    }
}
