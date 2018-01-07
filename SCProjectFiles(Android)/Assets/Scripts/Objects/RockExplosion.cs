using UnityEngine;

public class RockExplosion : MonoBehaviour
{
    ParticleSystem m_explosionSystem;
    SoundManager m_soundsContainer;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] float m_destroyAfter;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
        m_explosionSystem.Play();
        m_soundsContainer = FindObjectOfType<SoundManager>();
		m_soundsContainer.m_soundsSource.clip = m_soundsContainer.m_rockExplosion;
		m_soundsContainer.m_soundsSource.Play();
        Destroy(gameObject , m_destroyAfter);
    }
}
