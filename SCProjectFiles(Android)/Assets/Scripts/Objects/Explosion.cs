using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem m_explosionSystem;
    SoundManager m_soundsContainer;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] float m_destroyAfter;
	[SerializeField] Material m_rock , m_super;

	public static string m_explosionType;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
		m_soundsContainer = FindObjectOfType<SoundManager>();

		switch(m_explosionType)
		{
			case "Rock":
				m_explosionSystem.GetComponent<Renderer> ().material = m_rock;
				m_soundsContainer.m_soundsSource.clip = m_soundsContainer.m_rockExplosion;
			break;

			case "Super":
				m_explosionSystem.GetComponent<Renderer> ().material = m_super;
			break;
		}

        m_explosionSystem.Play();
		m_soundsContainer.m_soundsSource.Play();
        Destroy(gameObject , m_destroyAfter);
    }
}
