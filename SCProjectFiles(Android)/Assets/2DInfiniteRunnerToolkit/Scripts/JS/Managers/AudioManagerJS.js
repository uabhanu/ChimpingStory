#pragma strict

public class AudioManagerJS extends MonoBehaviour 
{
    public var musicPlayer				: AudioSource;
    public var effectPlayer				: AudioSource;

    public var menuClickClip			: AudioClip;
    public var missionCompleteClip		: AudioClip;

    public var coinCollectedClip		: AudioClip;
    public var explosionClip			: AudioClip;

    public var powerupCollectedClip		: AudioClip;
    public var powerupUsedClip			: AudioClip;
    public var reviveClip				: AudioClip;

    public var audioEnabled				: boolean;

    static var instance					: AudioManagerJS;
    public static function Instance() { return instance; }

	// Use this for initialization
	function Start () 
    {
        instance = this;
		
        if (SaveManagerJS.audioEnabled == 1)
        {
            audioEnabled = true;

            if (musicPlayer)
                musicPlayer.Play();
        }
        else
        {
            audioEnabled = false;
        }
	}

    public function ChangeAudioState()
    {
        if (audioEnabled)
        {
            audioEnabled = false;
            SaveManagerJS.audioEnabled = 0;

            if (musicPlayer)
                musicPlayer.Stop();

            if (effectPlayer)
                effectPlayer.Stop();
        }
        else
        {
            audioEnabled = true;
            SaveManagerJS.audioEnabled = 1;

            if (musicPlayer)
                musicPlayer.Play();
        }

        SaveManagerJS.SaveData();
    }

    public function PlayMenuClick()
    {
        if (menuClickClip && audioEnabled)
        {
            effectPlayer.clip = menuClickClip;
            effectPlayer.Play();
        }
    }
    public function PlayMissionComplete()
    {
        if (missionCompleteClip && audioEnabled)
        {
            effectPlayer.clip = missionCompleteClip;
            effectPlayer.Play();
        }
    }
    public function PlayCoinCollected()
    {
        if (coinCollectedClip && audioEnabled)
        {
            effectPlayer.clip = coinCollectedClip;
            effectPlayer.Play();
        }
    }
    public function PlayExplosion()
    {
        if (explosionClip && audioEnabled)
        {
            effectPlayer.clip = explosionClip;
            effectPlayer.Play();
        }
    }
    public function PlayPowerupCollected()
    {
        if (powerupCollectedClip && audioEnabled)
        {
            effectPlayer.clip = powerupCollectedClip;
            effectPlayer.Play();
        }
    }
    public function PlayPowerupUsed()
    {
        if (powerupUsedClip && audioEnabled)
        {
            effectPlayer.clip = powerupUsedClip;
            effectPlayer.Play();
        }
    }
    public function PlayRevive()
    {
        if (reviveClip && audioEnabled)
        {
            effectPlayer.clip = reviveClip;
            effectPlayer.Play();
        }
    }
}
