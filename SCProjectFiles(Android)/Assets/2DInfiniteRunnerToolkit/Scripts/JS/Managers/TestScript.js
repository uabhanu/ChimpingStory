#pragma strict

public static class TestScript extends Object
{
	public var coinAmmount 		: int = 1500;			//The ammount of coins the player has
    public var bestDistance 	: int  = 0;				//The best distance the player has reached
	
    public var extraSpeed 		: int  = 0;				//The ammount of extra speed power ups the player has
    public var shield 			: int  = 0;				//The ammount of shield power ups the player has
    public var sonicWave 		: int  = 0;				//The ammount of sonic wave power ups the player has
    public var revive 			: int  = 0;				//The ammount of revive power ups the player has

    public var missionID		: int[];				//Mission 1, 2 and 3 ID
    public var missionData		: int[];				//Mission 1, 2 and 3 Data

    public var missionDataString	: String = "";			//Saved mission data string

	//Loads the player data
    public function LoadData()
    {
        return"Test";
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