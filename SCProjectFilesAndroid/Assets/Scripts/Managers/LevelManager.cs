using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //public WaterChimp m_waterChimp; 
    //public GUIManager guiManager;                       //Holds a link to the GUI Manager
    public LevelGenerator levelGenerator;               //Holds a link to the Level Generator
    //public MissionManager missionManager;               //Holds a link to the Mission Manager
    //public PowerupManager powerupManager;               //Holds a link to the Powerup Manager

    private int collectedCoins;                         //Hold the current collected coin ammount

    //Used for initialisation
    void Start()
    {
		Application.targetFrameRate = 60;
        collectedCoins = 0;

        //SaveManager.SaveData();
        //SaveManager.ResetMissions();

        //SaveManager.LoadData();
        //SaveManager.LoadMissionData();

        //missionManager.LoadData();
    }

    //Called when a coin is collected by the player
	public void CoinCollected(Vector2 contactPoint)
    {
        collectedCoins++;
        //guiManager.UpdateCoins(collectedCoins);

        levelGenerator.AddCoinParticle(contactPoint);
        //missionManager.CoinEvent(collectedCoins);
	}
    //Adds an explosion to the level
    public void Collision(string collidedWith, Vector2 contactPoint)
    {
        levelGenerator.AddExplosionParticle(contactPoint);
        //missionManager.CollisionEvent(collidedWith);
    }
    //Called when the player picks up a powerup
    public void PowerupPickup(string name)
    {
        //missionManager.CollisionEvent(name);
        //guiManager.ShowPowerup(name);
    }
    //Restarts the level
	public void Restart()
    {
        levelGenerator.Reset();
        //m_waterChimp.Reset();
        //missionManager.SaveData();

        StartLevel();
	}
    //Returns to the main menu
    public void QuitToMain()
    {
        //m_waterChimp.Reset();
        levelGenerator.Reset();
        //missionManager.SaveData();
    }
    //Starts the level
	public void StartLevel()
    {
        collectedCoins = 0;

        //m_waterChimp.EnableChimp();
        levelGenerator.StartToGenerate();

        //missionManager.LoadData();
	}
    //Pauses the level
	public void PauseLevel()
    {
        //m_waterChimp.SetPauseState(true);
        levelGenerator.SetPauseState(true);
        //powerupManager.SetPauseState(true);
	}
    //Resume the level
    public void ResumeLevel()
    {
        //m_waterChimp.SetPauseState(false);
        levelGenerator.SetPauseState(false);
        //powerupManager.SetPauseState(false);
    }
    //Stops the level after a crash
    public void StopLevel()
    {
        levelGenerator.StopGeneration(2);

        //StartCoroutine(FunctionLibrary.CallWithDelay(guiManager.BackToLandLose , levelGenerator.CurrentDistance(), 2.5f));
    }
    //Revives the player, launches a sonic wave, and continue the level generation
    public void ReviveUsed()
    {
        //m_waterChimp.Revive();
        //StartCoroutine(FunctionLibrary.CallWithDelay(levelGenerator.ContinueGeneration, 0.75f));
    }
    //Called when the level has ended
    public void LevelEnded()
    {
        SaveStats();
        //missionManager.SaveData();
        //missionManager.LoadData();
    }
    //Returns the number of collected coins
    public int CollectedCoins()
    {
        return collectedCoins;
    }

    //Saves the best distance, and the collected coins
    private void SaveStats()
    {
        //if (SaveManager.bestDistance < levelGenerator.CurrentDistance())
            //SaveManager.bestDistance = levelGenerator.CurrentDistance();

        //SaveManager.coinAmmount += collectedCoins;
        //SaveManager.SaveData();
    }
}