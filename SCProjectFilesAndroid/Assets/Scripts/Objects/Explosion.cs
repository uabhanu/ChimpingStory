using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem m_explosionSystem;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] private float m_destroyAfter;
	[SerializeField] private Material _explosionMaterial;
    [SerializeField] private SoundManagerObject _soundManagerObject;
	
	public static string m_explosionType;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
		m_explosionSystem.GetComponent<Renderer>().material = _explosionMaterial;
		_soundManagerObject.m_soundsSource.clip = _soundManagerObject.m_rockExplosion;
        m_explosionSystem.Play();

		if(_soundManagerObject.m_playerMutedSounds == 0)
        {
            _soundManagerObject.m_soundsSource.Play();
        }

        Destroy(gameObject , m_destroyAfter);
    }
}
