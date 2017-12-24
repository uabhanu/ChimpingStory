#pragma strict

public class LevelManagerJS extends MonoBehaviour
{
    public var playerManager		: PlayerManagerJS;				//Holds a link to the Player Manager
    public var guiManager			: GUIManagerJS;					//Holds a link to the GUI Manager
    public var levelGenerator		: LevelGeneratorJS;				//Holds a link to the Level Generator
    public var missionManager		: MissionManagerJS;				//Holds a link to the Mission Manager
    public var powerupManager		: PowerupManagerJS;				//Holds a link to the Powerup Manager

    private var collectedCoins		: int;							//Hold the current collected coin ammount

    //Used for initialisation
    function Start()
    {
        collectedCoins = 0;
		
        //SaveManagerJS.SaveData();
        //SaveManagerJS.ResetMissions();
		
        SaveManagerJS.LoadData();
        SaveManagerJS.LoadMissionData();

        missionManager.LoadData();
    }

    //Called when a coin is collected by the player
	public function CoinCollected(contactPoint : Vector2)
    {
        collectedCoins++;
        guiManager.UpdateCoins(collectedCoins);

        levelGenerator.AddCoinParticle(contactPoint);
        missionManager.CoinEvent(collectedCoins);
	}
    //Adds an explosion to the level
    public function Collision(collidedWith : String, contactPoint : Vector2)
    {
        levelGenerator.AddExplosionParticle(contactPoint);
        missionManager.CollisionEvent(collidedWith);
    }
    //Called when the player picks up a powerup
    public function PowerupPickup(name : String)
    {
        missionManager.CollisionEvent(name);
        guiManager.ShowPowerup(name);
    }
    //Restarts the level
	public function Restart()
    {
        levelGenerator.Reset();
        playerManager.Reset();
		missionManager.SaveData();
		
        StartLevel();
	}
    //Returns to the main menu
    public function QuitToMain()
    {
        playerManager.Reset();
        levelGenerator.Reset();
        missionManager.SaveData();
    }
    //Starts the level
	public function StartLevel()
    {
        collectedCoins = 0;

        playerManager.EnableSubmarine();
        levelGenerator.StartToGenerate();

        missionManager.LoadData();
	}
    //Pauses the level
	public function PauseLevel()
    {
        playerManager.SetPauseState(true);
        levelGenerator.SetPauseState(true);
        powerupManager.SetPauseState(true);
	}
    //Resume the level
    public function ResumeLevel()
    {
        playerManager.SetPauseState(false);
        levelGenerator.SetPauseState(false);
        powerupManager.SetPauseState(false);
    }
    //Stops the level after a crash
    public function StopLevel()
    {
        levelGenerator.StopGeneration(2);

        StartCoroutine(FunctionLibraryJS.CallWithDelay(guiManager.ShowCrashScreen, levelGenerator.CurrentDistance(), 2.5f));
    }
    //Revives the player, launches a sonic wave, and continue the level generation
    public function ReviveUsed()
    {
        playerManager.Revive();
        StartCoroutine(FunctionLibraryJS.CallWithDelay(levelGenerator.ContinueGeneration, 0.75f));
    }
    //Called when the level has ended
    public function LevelEnded()
    {
        SaveStats();
        missionManager.SaveData();
        missionManager.LoadData();
    }
    //Returns the number of collected coins
    public function CollectedCoins()
    {
        return collectedCoins;
    }

    //Saves the best distance, and the collected coins
    private function SaveStats()
    {
        if (SaveManagerJS.bestDistance < levelGenerator.CurrentDistance())
            SaveManagerJS.bestDistance = levelGenerator.CurrentDistance();

        SaveManagerJS.coinAmmount += collectedCoins;
        SaveManagerJS.SaveData();
    }
}