using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public WaterChimp m_waterChimp;         //A link to the Player Manager

    public void OnPointerDown(PointerEventData eventData)
    {
        m_waterChimp.UpdateInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_waterChimp.UpdateInput(false);
    }
}
