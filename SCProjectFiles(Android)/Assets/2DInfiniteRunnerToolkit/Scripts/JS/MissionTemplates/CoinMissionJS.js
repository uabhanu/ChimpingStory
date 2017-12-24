#pragma strict

public class CoinMissionJS extends MissionTemplateJS
{
    public var description		: String;
    public var requiredCoins	: int;

    public var inOneRune		: boolean;

    private var storedValue		: int;
    private var receivedValue	: int;

    private var isCompleted		: boolean;

    //Updates the mission
    public override function UpdateMission(missionValue : int)
    {
        receivedValue = missionValue;

        if (inOneRune && requiredCoins < missionValue)
            isCompleted = true;
        else if (!inOneRune && requiredCoins < storedValue + missionValue)
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
        if (!inOneRune)
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
            return (storedValue + receivedValue) + "/" + requiredCoins;
        else
            return requiredCoins + "/" + requiredCoins;
    }
}
