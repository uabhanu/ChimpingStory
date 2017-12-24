#pragma strict
import System.Collections.Generic;

public class MissionManagerJS extends MonoBehaviour 
{
    public var guiManager			: GUIManagerJS;					//A link to the GUI Manager
    public var levelManager			: LevelManagerJS;				//A link to the Level Manager
    public var powerupManager		: PowerupManagerJS;				//A link to the Powerup Manager
    
	public var missions				: MissionTemplateJS[];			//List of missions

	private var missionID			: int[];						//The ID of the active missions
    private var missionData			: int[];						//Stores the saved data for the active missions
    private var data				: String = "";					//The data string containing the saved status

    //Load the saved data
    public function LoadData()
    {
        missionID = SaveManagerJS.missionID;
        missionData = SaveManagerJS.missionData;

        data = SaveManagerJS.missionDataString;

        //If a mission is removed, reset the data string
        if (data.Length > missions.Length)
            ResetMissions();

        //If a mission was added, update the data string
        else if (data.Length < missions.Length)
            AddNewMissions();

        //Set completition status for the missions
        for (var i : int = 0; i < data.Length; i++)
        {
            if (data[i] == '1')
                missions[i].SetCompletition(true);
        }

        //If a mission slot is empty, or contains a completed mission, look for a new mission, if possible
        for (var j : int = 0; j < 3; j++)
        {
            if (missionID[j] == -1 || missions[missionID[j]].IsCompleted())
                GetNewMission(j);
        }
		
		//Load saves mission data
        for (var k : int = 0; k < 3; k++)
        {
            if (missionID[k] != -1 && !missions[missionID[k]].IsCompleted())
                missions[missionID[k]].SetStoredValue(missionData[k]);
        }
		
        SaveManagerJS.SaveMissionData();

        //Update mission GUI texts
        //UpdateGUITexts();
    }
    //Saves the mission data
    public function SaveData()
    {
        missionData = new int[3];

        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] == -1)
                missionData[i] = 0;
            else
                missionData[i] = missions[missionID[i]].MissionData();
        }

        SaveManagerJS.missionData = missionData;
        SaveManagerJS.missionID = missionID;

        SaveManagerJS.missionDataString = data;

        SaveManagerJS.SaveMissionData();
    }
    public function GetMissionTexts()
    {
        var missionTexts : String[] = new String[missionID.Length];

        for (var i : int = 0; i < missionID.Length; i++)
            if (missionID[i] > -1)
                missionTexts[i] = missions[missionID[i]].MissionDescription();
            else
                missionTexts[i] = "Completed";

        return missionTexts;
    }
    public function GetMissionStats()
    {
        var missionStatus : String[] = new String[missionID.Length];

        for (var i : int = 0; i < missionID.Length; i++)
            if (missionID[i] > -1)
                missionStatus[i] = missions[missionID[i]].MissionStatus();
            else
                missionStatus[i] = "";

        return missionStatus;
    }

    //Distance event
    public function DistanceEvent(distance : int)
    {
        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] != -1 && missions[missionID[i]].GetType() == DistanceMissionJS && !missions[missionID[i]].IsCompleted())
            {
                missions[missionID[i]].UpdateMission(distance, levelManager.CollectedCoins(), powerupManager.PowerupUsed());

                if (missions[missionID[i]].IsCompleted())
                    MissionCompleted(i);
                //else
                //    UpdateGUITexts();
            }
        }
    }
    //Coin event
    public function CoinEvent(collectedCoins : int)
    {
        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] != -1 && missions[missionID[i]].GetType() == CoinMissionJS && !missions[missionID[i]].IsCompleted())
            {
                missions[missionID[i]].UpdateMission(collectedCoins);

                if (missions[missionID[i]].IsCompleted())
                    MissionCompleted(i);
                //else
                //    UpdateGUITexts();
            }
        }
    }
    //Crash event
    public function CrashEvent(distance : int)
    {
        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] != -1 && missions[missionID[i]].GetType() == CrashMissionJS && !missions[missionID[i]].IsCompleted())
            {
                missions[missionID[i]].UpdateMission(distance);

                if (missions[missionID[i]].IsCompleted())
                    MissionCompleted(i);
            }
        }
    }
    //Crash event
    public function ShopEvent(bought : String)
    {
        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] != -1 && missions[missionID[i]].GetType() == ShopMissionJS && !missions[missionID[i]].IsCompleted())
            {
                missions[missionID[i]].UpdateMission(bought);

                if (missions[missionID[i]].IsCompleted())
                    MissionCompleted(i);
            }
        }
    }
    //Crash event
    public function CollisionEvent(collidedWith : String)
    {
        for (var i : int = 0; i < missionID.Length; i++)
        {
            if (missionID[i] != -1 && missions[missionID[i]].GetType() == CollisionMissionJS && !missions[missionID[i]].IsCompleted())
            {
                missions[missionID[i]].UpdateMission(collidedWith);

                if (missions[missionID[i]].IsCompleted())
                    MissionCompleted(i);
            }
        }
    }

    //Resets the mision data
    private function ResetMissions()
    {
        data = "";

        for (var i : int = 0; i < missions.Length; i++)
            data += "0";

        for (var j : int = 0; j < missionID.Length; j++)
            missionID[j] = -1;

        for (var k : int = 0; k < missionData.Length; k++)
            missionData[k] = 0;

        SaveManagerJS.missionDataString = data;

        SaveManagerJS.missionID = missionID;
        SaveManagerJS.missionData = missionData;

    }
    //Adds new elements to the mission data string
    private function AddNewMissions()
    {
        for (var i : int = data.Length; i < missions.Length; i++)
            data += "0";

        SaveManagerJS.missionDataString = data;
    }
    //Called when a mission is completed, notifies the player, and updates the mission data string
    private function MissionCompleted(id : int)
    {
        guiManager.ShowMissionComplete(missions[missionID[id]].MissionDescription());

        var dataChar : char[] = data.ToCharArray();

        dataChar[missionID[id]] = "1"[0];
        data = new String(dataChar);

        SaveData();

        if (!guiManager.InPlayMode())
        {
            for (var i : int = 0; i < 3; i++)
            {
                if (missionID[i] == -1 || missions[missionID[i]].IsCompleted())
                    GetNewMission(i);
            }
    }
    }
    //Gets a new mission to the given slot
    private function GetNewMission(id : int)
    {
        for (var i : int = 0; i < data.Length; i++)
        {
            if (data[i] == '0' && missionID[0] != i && missionID[1] != i && missionID[2] != i)
            {
                missionID[id] = i;
                missionData[id] = 0;
                return;
            }
        }

        missionID[id] = -1;
        missionData[id] = 0;
    }
}
