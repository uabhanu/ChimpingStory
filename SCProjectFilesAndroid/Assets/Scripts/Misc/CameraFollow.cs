using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float m_offsetX , m_offsetY;

    [SerializeField] private Transform m_characterToFollow;

    private void Start()
    {
        m_offsetX = transform.position.x - m_characterToFollow.position.x;
        m_offsetY = transform.position.y - m_characterToFollow.position.y;
    }
    private void LateUpdate()
    {
        Vector3 tempPosition = transform.position;
        tempPosition.x = m_characterToFollow.position.x + m_offsetX;
        tempPosition.y = m_characterToFollow.position.y + m_offsetY;
        tempPosition.z = -10;
        transform.position = tempPosition;
    }
}
