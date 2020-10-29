using System;
using UnityEngine;

namespace SelfiePuss.Events
{
	public class EventsManager
	{
		private static event Action 				AdsSkipped;
		private static event Action 				AdsStarted;
		private static event Action                 AdsFailed;
		private static event Action 				AdsUI;
		private static event Action<int>            CoinCollected;
		private static event Action                 CountdownFinished;
		private static event Action                 FallDeath;
		private static event Action                 HurdleDeath;
		private static event Action<Vector2>		SpawnCoinPointsPrefab;
		private static event Action<Vector2>		SpawnMeteorPointsPrefab;
		private static event Action                 Jump;
		private static event Action                 MeteorExplosion;
		private static event Action                 NewVersion;
		private static event Action                 Paused;
		private static event Action<bool>           PlaySoundsChanged;
		private static event Action<bool>           PlayMusicChanged;
		private static event Action                 PunishAdWatched;
		private static event Action<bool> 			RestartScene;
		private static event Action                 Resumed;
		private static event Action                 RewardAdWatched;
		private static event Action<int>            ScoreUpdate;
		private static event Action                 SelfieTaken;
		private static event Action                 SoundOff;
		private static event Action                 SoundOn;
		private static event Action                 SuperCollected;

		public static void SubscribeToEvent(SelfiePussEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdsSkipped:
					AdsSkipped += actionFunction;
				return;

				case SelfiePussEvent.AdsStarted:
					AdsStarted += actionFunction;
				return;

				case SelfiePussEvent.AdsFailed:
					AdsFailed += actionFunction;
				return;

				case SelfiePussEvent.AdsUI:
					AdsUI += actionFunction;
				return;

				case SelfiePussEvent.CountdownFinished:
					CountdownFinished += actionFunction;
				return;

				case SelfiePussEvent.FallDeath:
					FallDeath += actionFunction;
				return;

				case SelfiePussEvent.HurdleDeath:
					HurdleDeath += actionFunction;
				return;

				case SelfiePussEvent.Jump:
					Jump += actionFunction;
				return;

				case SelfiePussEvent.MeteorExplosion:
					MeteorExplosion += actionFunction;
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion += actionFunction;
				return;

				case SelfiePussEvent.Paused:
					Paused += actionFunction;
				return;

				case SelfiePussEvent.PunishAdWatched:
					PunishAdWatched += actionFunction;
				return;

				case SelfiePussEvent.Resumed:
					Resumed += actionFunction;
				return;

				case SelfiePussEvent.RewardsAdWatched:
					RewardAdWatched += actionFunction;
				return;

				case SelfiePussEvent.SelfieTaken:
					SelfieTaken += actionFunction;
				return;

				case SelfiePussEvent.SoundsMuted:
					SoundOff += actionFunction;
				return;

				case SelfiePussEvent.SoundsUnmuted:
					SoundOn += actionFunction;
				return;

				case SelfiePussEvent.SuperCollected:
					SuperCollected += actionFunction;
				return;
			}
		}

		public static void SubscribeToEvent(SelfiePussEvent evt , Action<bool> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.PlaySoundsChanged:
					PlaySoundsChanged += actionFunction;
				return;

				case SelfiePussEvent.PlayMusicChanged:
					PlayMusicChanged += actionFunction;
				return;

				case SelfiePussEvent.RestartScene:
					RestartScene += actionFunction;
				return;
			}
		}

		public static void SubscribeToEvent(SelfiePussEvent evt , Action<int> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.CoinCollected:
					CoinCollected += actionFunction;
				return;

				case SelfiePussEvent.ScoreUpdate:
					ScoreUpdate += actionFunction;
				return;
			}
		}

		public static void SubscribeToEvent(SelfiePussEvent evt , Action<Vector2> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.SpawnCoinPointsPrefab:
					SpawnCoinPointsPrefab += actionFunction;
				return;

				case SelfiePussEvent.SpawnMeteorPointsPrefab:
					SpawnMeteorPointsPrefab += actionFunction;
				return;
			}
		}

		public static void UnsubscribeFromEvent(SelfiePussEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdsSkipped:
					AdsSkipped -= actionFunction;
				return;

				case SelfiePussEvent.AdsStarted:
					AdsStarted -= actionFunction;
				return;

				case SelfiePussEvent.AdsFailed:
					AdsFailed -= actionFunction;
				return;

				case SelfiePussEvent.AdsUI:
					AdsUI -= actionFunction;
				return;

				case SelfiePussEvent.CountdownFinished:
					CountdownFinished -= actionFunction;
				return;

				case SelfiePussEvent.FallDeath:
					FallDeath -= actionFunction;
				return;

				case SelfiePussEvent.HurdleDeath:
					HurdleDeath -= actionFunction;
				return;

				case SelfiePussEvent.Jump:
					Jump -= actionFunction;
				return;

				case SelfiePussEvent.MeteorExplosion:
					MeteorExplosion -= actionFunction;
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion -= actionFunction;
				return;

				case SelfiePussEvent.Paused:
					Paused -= actionFunction;
				return;

				case SelfiePussEvent.PunishAdWatched:
					PunishAdWatched -= actionFunction;
				return;

				case SelfiePussEvent.Resumed:
					Resumed -= actionFunction;
				return;

				case SelfiePussEvent.RewardsAdWatched:
					RewardAdWatched -= actionFunction;
				return;

				case SelfiePussEvent.SelfieTaken:
					SelfieTaken -= actionFunction;
				return;

				case SelfiePussEvent.SoundsMuted:
					SoundOff -= actionFunction;
				return;

				case SelfiePussEvent.SoundsUnmuted:
					SoundOn -= actionFunction;
				return;

				case SelfiePussEvent.SuperCollected:
					SuperCollected -= actionFunction;
				return;
			}
		}

		public static void UnsubscribeFromEvent(SelfiePussEvent evt , Action<bool> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.PlaySoundsChanged:
					PlaySoundsChanged -= actionFunction;
				return;

				case SelfiePussEvent.PlayMusicChanged:
					PlayMusicChanged -= actionFunction;
				return;

				case SelfiePussEvent.RestartScene:
					RestartScene -= actionFunction;
				return;
			}
		}

		public static void UnsubscribeFromEvent(SelfiePussEvent evt , Action<int> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.CoinCollected:
					CoinCollected -= actionFunction;
				return;

				case SelfiePussEvent.ScoreUpdate:
					ScoreUpdate -= actionFunction;
				return;
			}
		}

		public static void UnsubscribeFromEvent(SelfiePussEvent evt , Action<Vector2> actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.SpawnCoinPointsPrefab:
					SpawnCoinPointsPrefab -= actionFunction;
				return;

				case SelfiePussEvent.SpawnMeteorPointsPrefab:
					SpawnMeteorPointsPrefab -= actionFunction;
				return;
			}
		}

		public static void InvokeEvent(SelfiePussEvent evt)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdsSkipped:
					AdsSkipped?.Invoke();
				return;

				case SelfiePussEvent.AdsStarted:
					AdsStarted?.Invoke();
				return;

				case SelfiePussEvent.AdsFailed:
					AdsFailed?.Invoke();
				return;

				case SelfiePussEvent.AdsUI:
					AdsUI?.Invoke();
				return;

				case SelfiePussEvent.CountdownFinished:
					CountdownFinished?.Invoke();
				return;

				case SelfiePussEvent.FallDeath:
					FallDeath?.Invoke();
				return;

				case SelfiePussEvent.HurdleDeath:
					HurdleDeath?.Invoke();
				return;

				case SelfiePussEvent.Jump:
					Jump?.Invoke();
				return;

				case SelfiePussEvent.MeteorExplosion:
					MeteorExplosion?.Invoke();
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion?.Invoke();
				return;

				case SelfiePussEvent.Paused:
					Paused?.Invoke();
				return;

				case SelfiePussEvent.PunishAdWatched:
					PunishAdWatched?.Invoke();
				return;

				case SelfiePussEvent.Resumed:
					Resumed?.Invoke();
				return;

				case SelfiePussEvent.RewardsAdWatched:
					RewardAdWatched?.Invoke();
				return;

				case SelfiePussEvent.SelfieTaken:
					SelfieTaken?.Invoke();
				return;

				case SelfiePussEvent.SoundsMuted:
					SoundOff?.Invoke();
				return;

				case SelfiePussEvent.SoundsUnmuted:
					SoundOn?.Invoke();
				return;

				case SelfiePussEvent.SuperCollected:
					SuperCollected?.Invoke();
				return;
			}
		}

		public static void InvokeEvent(SelfiePussEvent evt , bool changedToValue)
		{
			switch(evt)
			{
				case SelfiePussEvent.PlaySoundsChanged:
					PlaySoundsChanged?.Invoke(changedToValue);
				return;

				case SelfiePussEvent.PlayMusicChanged:
					PlayMusicChanged?.Invoke(changedToValue);
				return;

				case SelfiePussEvent.RestartScene:
					RestartScene?.Invoke(changedToValue);
				return;
			}
		}

		public static void InvokeEvent(SelfiePussEvent evt , int changedToValue)
		{
			switch(evt)
			{
				case SelfiePussEvent.CoinCollected:
					CoinCollected?.Invoke(changedToValue);
				return;

				case SelfiePussEvent.ScoreUpdate:
					ScoreUpdate?.Invoke(changedToValue);
				return;
			}
		}

		public static void InvokeEvent(SelfiePussEvent evt , Vector2 changedToValue)
		{
			switch(evt)
			{
				case SelfiePussEvent.SpawnCoinPointsPrefab:
					SpawnCoinPointsPrefab?.Invoke(changedToValue);
				return;

				case SelfiePussEvent.SpawnMeteorPointsPrefab:
					SpawnMeteorPointsPrefab?.Invoke(changedToValue);
				return;
			}
		}
	}
}