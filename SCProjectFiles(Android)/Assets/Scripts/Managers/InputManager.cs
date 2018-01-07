using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerManager playerManager;         //A link to the Player Manager

    public void OnPointerDown(PointerEventData eventData)
    {
        playerManager.UpdateInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerManager.UpdateInput(false);
    }
}
