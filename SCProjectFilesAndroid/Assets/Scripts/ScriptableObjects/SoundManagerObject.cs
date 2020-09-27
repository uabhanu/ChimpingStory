using UnityEngine;

[CreateAssetMenu]
public class SoundManagerObject : ScriptableObject
{
    public AudioClip m_coinCollected , m_fallDeath , m_hurdleDeath , m_jump, m_rockExplosion , m_selfie , m_superCollected;
    public AudioSource m_musicSource , m_soundsSource;
    public int m_playerMutedSounds = 0;

    public void GetSoundsStatus()
    {
        m_musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        m_soundsSource = GameObject.FindGameObjectWithTag("Sounds").GetComponent<AudioSource>();
        m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();
    }
}
