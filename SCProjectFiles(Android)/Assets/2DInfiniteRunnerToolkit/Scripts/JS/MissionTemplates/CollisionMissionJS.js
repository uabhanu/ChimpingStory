#pragma strict
import System.Collections.Generic;

public class CollisionMissionJS extends MissionTemplateJS
{
    public var description			: String;
    public var requiredCollision	: List.<String>;

    private var isCompleted			: boolean;

    //Updates the mission
    public override function UpdateMission(missionValue : String)
    {
        if (requiredCollision.Contains(missionValue))
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
    public override function SetStoredValue(savedValue : int) { }
    //Return the mission data to be saved
    public override function MissionData()
    {
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
        return "";
    }
}
