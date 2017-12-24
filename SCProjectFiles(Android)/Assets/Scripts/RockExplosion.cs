using UnityEngine;

public class RockExplosion : MonoBehaviour
{
    [Tooltip("Select the value of seconds after which this object should be destroyed")][SerializeField][Range (0.1f , 1.1f)] float m_destroyAfter;
    ParticleSystem m_explosionSystem;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
        m_explosionSystem.Play();
        Destroy(gameObject , m_destroyAfter);
    }
}
