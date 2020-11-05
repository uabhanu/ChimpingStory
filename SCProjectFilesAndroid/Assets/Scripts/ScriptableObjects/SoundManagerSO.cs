using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Sound Manager", fileName = "NewSoundManager")]
public class SoundManagerSO : ScriptableObject
{
    [SerializeField] private AudioClip _coinCollectedAudioClip , _fallDeathAudioClip , _hurdleDeathAudioClip , _jumpAudioClip , _meteorExplosionAudioClip , _selfieAudioClip , _superCollectedAudioClip;

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
