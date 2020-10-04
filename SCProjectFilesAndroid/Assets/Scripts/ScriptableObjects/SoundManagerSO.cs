using UnityEngine;

[CreateAssetMenu]
public class SoundManagerSO : ScriptableObject
{
    [SerializeField] private AudioClip _coinCollectedAudioClip , _fallDeathAudioClip , _hurdleDeathAudioClip , _jumpAudioClip , _meteorExplosionAudioClip , _selfieAudioClip , _superCollectedAudioClip;
    public int m_playerMutedSounds;

    public AudioClip GetCoinCollectedAudioClip()
    {
        return _coinCollectedAudioClip;
    }

    public AudioClip GetFallDeathAudioClip()
    {
        return _fallDeathAudioClip;
    }

    public AudioClip GetHurdleDeathAudioClip()
    {
        return _hurdleDeathAudioClip;
    }

    public AudioClip GetJumpAudioClip()
    {
        return _jumpAudioClip;
    }

    public AudioClip GetMeteorExplosionAudioClip()
    {
        return _meteorExplosionAudioClip;
    }
    public AudioClip GetSelfieAudioClip()
    {
        return _selfieAudioClip;
    }
    public AudioClip GetSuperCollectedAudioClip()
    {
        return _superCollectedAudioClip;
    }
}
