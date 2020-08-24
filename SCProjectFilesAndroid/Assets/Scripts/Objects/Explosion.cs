using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem m_explosionSystem;
    private SoundManager m_soundManager;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] private float m_destroyAfter;
	[SerializeField] private Material _explosionMaterial;
	
	public static string m_explosionType;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
		m_explosionSystem.GetComponent<Renderer>().material = _explosionMaterial;
		m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		m_soundManager.m_soundsSource.clip = m_soundManager.m_rockExplosion;
        m_explosionSystem.Play();

		if(SoundManager.m_playerMutedSounds == 0)
        {
            m_soundManager.m_soundsSource.Play();
        }

        Destroy(gameObject , m_destroyAfter);
    }
}
