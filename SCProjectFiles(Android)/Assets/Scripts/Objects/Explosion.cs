using UnityEngine;

public class Explosion : MonoBehaviour
{
<<<<<<< HEAD
    ParticleSystem m_explosionSystem;
    SoundManager m_soundsContainer;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] float m_destroyAfter;
=======
	ParticleSystem m_explosionSystem;
	SoundManager m_soundManager;

	[SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] float m_destroyAfter;
>>>>>>> 43ba325a6c6e0027b6381c98a026fa4dfd90bcc0
	[SerializeField] Material m_rock , m_super;

	public static string m_explosionType;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
<<<<<<< HEAD
		m_soundsContainer = FindObjectOfType<SoundManager>();
=======
		m_soundManager = FindObjectOfType<SoundManager>();
>>>>>>> 43ba325a6c6e0027b6381c98a026fa4dfd90bcc0

		switch(m_explosionType)
		{
			case "Rock":
				m_explosionSystem.GetComponent<Renderer> ().material = m_rock;
<<<<<<< HEAD
				m_soundsContainer.m_soundsSource.clip = m_soundsContainer.m_rockExplosion;
=======
				m_soundManager.m_soundsSource.clip = m_soundManager.m_rockExplosion;
>>>>>>> 43ba325a6c6e0027b6381c98a026fa4dfd90bcc0
			break;

			case "Super":
				m_explosionSystem.GetComponent<Renderer> ().material = m_super;
			break;
		}

        m_explosionSystem.Play();
<<<<<<< HEAD
		m_soundsContainer.m_soundsSource.Play();
=======
		m_soundManager.m_soundsSource.Play();
>>>>>>> 43ba325a6c6e0027b6381c98a026fa4dfd90bcc0
        Destroy(gameObject , m_destroyAfter);
    }
}
