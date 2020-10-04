using System;

namespace SelfiePuss.Events
{
	public class EventsManager
	{
		private static event Action 				AdStarted;
		private static event Action                 AdFailed;
		private static event Action                 CoinCollected;
		private static event Action                 CountdownFinished;
		private static event Action                 FallDeath;
		private static event Action                 HurdleDeath;
		private static event Action                 Jump;
		private static event Action                 NewVersion;
		private static event Action                 Paused;
		private static event Action<bool>           PlaySoundsChanged;
		private static event Action<bool>           PlayMusicChanged;
		private static event Action                 PunishAdWatched;
		private static event Action<bool> 			RestartScene;
		private static event Action                 Resumed;
		private static event Action                 RewardAdWatched;
		private static event Action                 RockExplosion;
		private static event Action                 ScoreChanged;
		private static event Action                 SelfieTaken;
		private static event Action                 SoundOff;
		private static event Action                 SoundOn;
		private static event Action                 SuperCollected;

		public static void SubscribeToEvent(SelfiePussEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdStarted:
					AdStarted += actionFunction;
				return;

				case SelfiePussEvent.AdFailed:
					AdFailed += actionFunction;
				return;

				case SelfiePussEvent.CoinCollected:
					CoinCollected += actionFunction;
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

				case SelfiePussEvent.RewardAdWatched:
					RewardAdWatched += actionFunction;
				return;

				case SelfiePussEvent.MeteorExplosion:
					RockExplosion += actionFunction;
				return;

				case SelfiePussEvent.ScoreChanged:
					ScoreChanged += actionFunction;
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

		public static void UnsubscribeFromEvent(SelfiePussEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdStarted:
					AdStarted -= actionFunction;
				return;

				case SelfiePussEvent.AdFailed:
					AdFailed -= actionFunction;
				return;

				case SelfiePussEvent.CoinCollected:
					CoinCollected -= actionFunction;
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

				case SelfiePussEvent.RewardAdWatched:
					RewardAdWatched -= actionFunction;
				return;

				case SelfiePussEvent.MeteorExplosion:
					RockExplosion -= actionFunction;
				return;

				case SelfiePussEvent.ScoreChanged:
					ScoreChanged -= actionFunction;
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

		public static void InvokeEvent(SelfiePussEvent evt)
		{
			switch(evt)
			{
				case SelfiePussEvent.AdStarted:
					AdStarted?.Invoke();
				return;

				case SelfiePussEvent.AdFailed:
					AdFailed?.Invoke();
				return;

				case SelfiePussEvent.CoinCollected:
					CoinCollected?.Invoke();
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

				case SelfiePussEvent.RewardAdWatched:
					RewardAdWatched?.Invoke();
				return;

				case SelfiePussEvent.MeteorExplosion:
					RockExplosion?.Invoke();
				return;

				case SelfiePussEvent.ScoreChanged:
					ScoreChanged?.Invoke();
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
	}
}