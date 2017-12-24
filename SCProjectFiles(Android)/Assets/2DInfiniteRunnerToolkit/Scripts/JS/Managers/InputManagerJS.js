import UnityEngine.EventSystems;
#pragma strict

public class InputManagerJS extends MonoBehaviour implements IPointerDownHandler, IPointerUpHandler
{
    public var playerManager	: PlayerManagerJS;         //A link to the Player Manager

    public function OnPointerDown(eventData : PointerEventData)
    {
        playerManager.UpdateInput(true);
    }

    public function OnPointerUp(eventData : PointerEventData)
    {
        playerManager.UpdateInput(false);
    }
}
