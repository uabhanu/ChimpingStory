#pragma strict

public class MissionTemplateJS extends MonoBehaviour
{
    //Used in distance missions
    public function UpdateMission(distance : int, collectedCoins : int, powerupUsed : boolean) {}
    //Used in coin and crash missions
    public function UpdateMission(missionValue : int) {}
    //Used in shop and collision missions
    public function UpdateMission(missionValue : String) {}
    //Set mission completion
    public function SetCompletition(toValue : boolean) {}
    //Returns true, if the mission is completed
    public function IsCompleted() {return false;}
    //Update the stored value
    public function SetStoredValue(savedValue : int) {}
    //Return the stored value to be saved
    public function MissionData() {return 0;}
    //Returns the mission description
    public function MissionDescription() {return "";}
    //Returns the mission status
    public function MissionStatus() {return "";}
}
