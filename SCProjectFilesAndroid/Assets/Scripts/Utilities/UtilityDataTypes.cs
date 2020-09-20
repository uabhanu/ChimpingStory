using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SelfiePuss.Utilities
{
	#region Enums

	public enum EditorPlayModeState
	{
		Stopped
	  , Playing
	  , Paused
	}

	public enum GameState
	{
	  	InstallVersion
	  ,	Playing
	  , Paused
	  , ShowingRewardAds
	  , ShowingTutorial
	  , GameOver
	}

	public enum SoundCue
	{
		PlayerDeathByEnemy
	  , PlayerDeathByFall
	  , PlayerDeathByHurdle
	}

	#endregion

	#region Structs

	//[Serializable]
	//public struct CreepPrefabInfo
	//{
	//	[FormerlySerializedAs("type")] public PussType m_type;

	//	[FormerlySerializedAs("creepAnimation")]
	//	public AnimationClip m_creepAnimation;
	//}

	//[Serializable]
	//public struct CreepSpawnInfo
	//{
	//	[FormerlySerializedAs("type")] public PussType m_type;

	//	[FormerlySerializedAs("spawnPositionXY")]
	//	public Vector2 m_spawnPositionXy;
	//}

	[Serializable]
	public struct SoundCueInfo
	{
		[FormerlySerializedAs("soundSource")]  public AudioSource m_soundSource;
		[FormerlySerializedAs("defaultPitch")] public float       m_defaultPitch;

		[FormerlySerializedAs("randomizePitch")]
		public bool m_randomizePitch;

		[FormerlySerializedAs("minPitch")] public float m_minPitch;
		[FormerlySerializedAs("maxPitch")] public float m_maxPitch;
	}

	#endregion
}