using SelfiePuss.Events;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem m_explosionSystem;

    [SerializeField] [Tooltip("Select the value of seconds after which this object should be destroyed")] [Range(0.1f , 1.1f)] private float m_destroyAfter;
	[SerializeField] private Material _explosionMaterial;
    [SerializeField] private SoundManagerSO _soundManagerObject;
	
	public static string m_explosionType;

    void Start()
    {
		m_explosionSystem = GetComponent<ParticleSystem>();
		m_explosionSystem.GetComponent<Renderer>().material = _explosionMaterial;
		EventsManager.InvokeEvent(SelfiePussEvent.MeteorExplosion); //TODO This may have to be changed later to do Super Explosion or Meteor Explosion
        m_explosionSystem.Play();
        Destroy(gameObject , m_destroyAfter);
    }
}
