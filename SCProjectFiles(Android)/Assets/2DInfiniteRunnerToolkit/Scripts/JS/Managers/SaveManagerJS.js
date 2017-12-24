#pragma strict

public static class SaveManagerJS extends Object
{
	public var coinAmmount 		: int = 1500;			//The ammount of coins the player has
    public var bestDistance 	: int  = 0;				//The best distance the player has reached
	
    public var extraSpeed 		: int  = 0;				//The ammount of extra speed power ups the player has
    public var shield 			: int  = 0;				//The ammount of shield power ups the player has
    public var sonicWave 		: int  = 0;				//The ammount of sonic wave power ups the player has
    public var revive 			: int  = 0;				//The ammount of revive power ups the player has
	
	public var currentSkinID	: int  = 0;				//The current submarine skin ID (0 is the default skin)
	public var skin2Unlocked 	: int  = 0;				//Hold the skin 2 owned state
	public var skin3Unlocked 	: int  = 0;				//Hold the skin 3 owned state
	
	public var audioEnabled 	: int  = 1;
	
    public var missionID		: int[];				//Mission 1, 2 and 3 ID
    public var missionData		: int[];				//Mission 1, 2 and 3 Data

    public var missionDataString	: String = "";			//Saved mission data string
	
	//Loads the player data
    public function LoadData()
    {
        //If found the coin ammount data, load the datas
        if (!PlayerPrefs.HasKey("Coin ammount"))
            SaveData();
        else
        {
            coinAmmount = PlayerPrefs.GetInt("Coin ammount");
            bestDistance = PlayerPrefs.GetInt("Best Distance");

            extraSpeed = PlayerPrefs.GetInt("Extra Speed");
            shield = PlayerPrefs.GetInt("Shield");
            sonicWave = PlayerPrefs.GetInt("Sonic Wave");
            revive = PlayerPrefs.GetInt("Revive");
            
            audioEnabled = PlayerPrefs.GetInt("AudioEnabled");
        }
        
        if (!PlayerPrefs.HasKey("Skin ID"))
		{
			PlayerPrefs.SetInt("Skin ID", currentSkinID);
			PlayerPrefs.SetInt("Skin 2 Unlocked", skin2Unlocked);
		}
		else
		{
			currentSkinID = PlayerPrefs.GetInt("Skin ID");
			skin2Unlocked = PlayerPrefs.GetInt("Skin 2 Unlocked");
		}
		
		if (!PlayerPrefs.HasKey("Skin 3 Unlocked"))
			PlayerPrefs.SetInt("Skin 3 Unlocked", skin3Unlocked);
		else
			skin3Unlocked = PlayerPrefs.GetInt("Skin 3 Unlocked");

		PlayerPrefs.Save();
    }
    //Saves the player data
    public function SaveData()
    {
        PlayerPrefs.SetInt("Coin ammount", coinAmmount);
        PlayerPrefs.SetInt("Best Distance", bestDistance);

        PlayerPrefs.SetInt("Extra Speed", extraSpeed);
        PlayerPrefs.SetInt("Shield", shield);
        PlayerPrefs.SetInt("Sonic Wave", sonicWave);
        PlayerPrefs.SetInt("Revive", revive);

		PlayerPrefs.SetInt("AudioEnabled", audioEnabled);
		
		PlayerPrefs.SetInt("Skin ID", currentSkinID);
		PlayerPrefs.SetInt("Skin 2 Unlocked", skin2Unlocked);
		PlayerPrefs.SetInt("Skin 3 Unlocked", skin3Unlocked);

        PlayerPrefs.Save();
    }
    //Loads the mission data
    public function LoadMissionData()
    {
        missionID = new int[3];
        missionID[0] = -1;
        missionID[1] = -1;
        missionID[2] = -1;
        
        missionData = new int[3];
        missionData[0] = 0;
        missionData[1] = 0;
        missionData[2] = 0;

        if (!PlayerPrefs.HasKey("Missions"))
        {
            SaveMissionData();
        }
        else
        {
            missionDataString = PlayerPrefs.GetString("Missions");

            missionID[0] = PlayerPrefs.GetInt("Mission1");
            missionID[1] = PlayerPrefs.GetInt("Mission2");
            missionID[2] = PlayerPrefs.GetInt("Mission3");

            missionData[0] = PlayerPrefs.GetInt("Mission1Data");
            missionData[1] = PlayerPrefs.GetInt("Mission2Data");
            missionData[2] = PlayerPrefs.GetInt("Mission3Data");
        }
    }
    //Saves the mission data
    public function SaveMissionData()
    {
        PlayerPrefs.SetInt("Mission1", missionID[0]);
        PlayerPrefs.SetInt("Mission2", missionID[1]);
        PlayerPrefs.SetInt("Mission3", missionID[2]);

        PlayerPrefs.SetInt("Mission1Data", missionData[0]);
        PlayerPrefs.SetInt("Mission2Data", missionData[1]);
        PlayerPrefs.SetInt("Mission3Data", missionData[2]);

        PlayerPrefs.SetString("Missions", missionDataString);
    }
    //Reset mission data
    public function ResetMissions()
    {
        missionID = new int[3];
        missionID[0] = -1;
        missionID[1] = -1;
        missionID[2] = -1;
        
        missionData = new int[3];
        missionData[0] = 0;
        missionData[1] = 0;
        missionData[2] = 0;

        missionDataString = "";

        SaveMissionData();
    }
}