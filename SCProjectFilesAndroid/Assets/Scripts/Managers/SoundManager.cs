using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	//TODO Scriptable Object
	public AudioClip m_coinCollected , m_fallDeath , m_hurdleDeath , m_jump, m_rockExplosion , m_selfie , m_superCollected;
	public AudioSource m_musicSource , m_soundsSource;

    public static int m_playerMutedSounds = 0;

    private void Start()
    {
        m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();
    }
}
