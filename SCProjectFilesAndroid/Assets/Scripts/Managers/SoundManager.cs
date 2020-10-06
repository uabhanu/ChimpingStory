using SelfiePuss.Events;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource , _soundsSource;
    [SerializeField] private SoundManagerSO _soundManagerSO;

    public int m_PlayerMutedSounds;

    private void Start()
    {
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    public int GetPlayerMutedSoundsValue()
    {
        return m_PlayerMutedSounds;
    }

    private void OnCoinCollected()
    {
        _soundsSource.clip = _soundManagerSO.GetCoinCollectedAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnFallDeath()
    {
        _soundsSource.clip = _soundManagerSO.GetFallDeathAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnHurdleDeath()
    {
        _soundsSource.clip = _soundManagerSO.GetHurdleDeathAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnJump()
    {
        _soundsSource.clip = _soundManagerSO.GetJumpAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnMeteorExplosion()
    {
        _soundsSource.clip = _soundManagerSO.GetMeteorExplosionAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnPaused()
    {
        _musicSource.Pause();
    }

    private void OnResumed()
    {
        _musicSource.Play();
    }

    private void OnSelfieTaken()
    {
        _soundsSource.clip = _soundManagerSO.GetSelfieAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void OnSoundsMuted()
    {
        _musicSource.Pause();
    }

    private void OnSoundsUnmuted()
    {
        _musicSource.Play();
    }

    private void OnSuperCollected()
    {
        _soundsSource.clip = _soundManagerSO.GetSuperCollectedAudioClip();

        if(m_PlayerMutedSounds == 0)
        {
            _soundsSource.Play();
        }
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.CoinCollected , OnCoinCollected);
        EventsManager.SubscribeToEvent(SelfiePussEvent.FallDeath , OnFallDeath);
        EventsManager.SubscribeToEvent(SelfiePussEvent.HurdleDeath , OnHurdleDeath);
        EventsManager.SubscribeToEvent(SelfiePussEvent.Jump , OnJump);
        EventsManager.SubscribeToEvent(SelfiePussEvent.MeteorExplosion , OnMeteorExplosion);
        EventsManager.SubscribeToEvent(SelfiePussEvent.Paused , OnPaused);
        EventsManager.SubscribeToEvent(SelfiePussEvent.Resumed , OnResumed);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SelfieTaken , OnSelfieTaken);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SoundsMuted , OnSoundsMuted);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SoundsUnmuted , OnSoundsUnmuted);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SelfieTaken , OnSelfieTaken);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SuperCollected , OnSuperCollected);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.CoinCollected , OnCoinCollected);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.FallDeath , OnFallDeath);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.HurdleDeath , OnHurdleDeath);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.Jump , OnJump);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.MeteorExplosion , OnMeteorExplosion);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.Paused , OnPaused);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.Resumed , OnResumed);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SelfieTaken , OnSelfieTaken);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SoundsMuted , OnSoundsMuted);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SoundsUnmuted , OnSoundsUnmuted);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SuperCollected , OnSuperCollected);
    }
}
