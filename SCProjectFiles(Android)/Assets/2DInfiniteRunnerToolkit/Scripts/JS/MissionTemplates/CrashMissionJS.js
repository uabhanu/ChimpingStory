#pragma strict

public class CrashMissionJS extends MissionTemplateJS
{
    public enum CrashMissionTypesJS { CrashBefore, CrashBetween, CrashAfter };

    public var description		: String;

    public var missionType		: CrashMissionTypesJS;

    public var minDistance		: int;
    public var maxDistance		: int;

    private var isCompleted		: boolean;

    //Updates the mission
    public override function UpdateMission(missionValue : int)
    {
        if (missionType == CrashMissionTypesJS.CrashBefore && missionValue < minDistance)
            isCompleted = true;
        else if (missionType == CrashMissionTypesJS.CrashBetween && minDistance <= missionValue && maxDistance >= missionValue)
            isCompleted = true;
        else if (missionType == CrashMissionTypesJS.CrashAfter && missionValue > maxDistance)
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
