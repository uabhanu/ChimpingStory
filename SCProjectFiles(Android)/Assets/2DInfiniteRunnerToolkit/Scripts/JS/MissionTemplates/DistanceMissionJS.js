#pragma strict

public class DistanceMissionJS extends MissionTemplateJS 
{
    public enum DistanceMissionTypeJS { InOneRun, InMultipleRun, NoCoin, NoPowerup }

    public var description			: String;
    public var requiredDistance		: int;

    public var missionType			: DistanceMissionTypeJS;

    private var storedValue			: int;
    private var receivedValue		: int;

    private var isCompleted			: boolean;

    //Updates the mission
    public override function UpdateMission(distance : int, collectedCoins : int, powerupUsed : boolean)
    {
        receivedValue = distance;

        if (missionType == DistanceMissionTypeJS.InOneRun && requiredDistance < distance)
            isCompleted = true;
        else if (missionType == DistanceMissionTypeJS.InMultipleRun && requiredDistance < storedValue + distance)
            isCompleted = true;
        else if (missionType == DistanceMissionTypeJS.NoCoin && requiredDistance < distance && collectedCoins == 0)
            isCompleted = true;
        else if (missionType == DistanceMissionTypeJS.NoPowerup && requiredDistance < distance && !powerupUsed)
            isCompleted = true;
    }
    //Set mission completion
    public override function SetCompletition(toValue : boolean)
    {
        isCompleted = toValue;
    }
    //Returns true, if the mission is completed
    public override function IsCompleted()
    {
        return isCompleted;
    }
    //Update the stored value
    public override function SetStoredValue(savedValue : int)
    {
        storedValue = savedValue;
    }
    //Return the mission data to be saved
    public override function MissionData()
    {
        if (missionType == DistanceMissionTypeJS.InMultipleRun)
            return storedValue + receivedValue;
        else
            return 0;
    }
    //Returns the mission description
    public override function MissionDescription()
    {
        return description;
    }
    //Returns the mission status
    public override function MissionStatus()
    {
        if (!isCompleted)
            return (storedValue + receivedValue) + "/" + requiredDistance;
        else
            return requiredDistance + "/" + requiredDistance;
    }
}
