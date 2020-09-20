using System;

namespace SelfiePuss.Events
{
	public class EventsManager
	{
		private static event Action 				AdStarted;
		private static event Action                 AdFailed;
		private static event Action                 CountDownOver;
		private static event Action                 NewVersion;
		private static event Action                 OutOfWater;
		private static event Action                 Paused;
		private static event Action<bool>           PlaySoundsChanged;
		private static event Action<bool>           PlayMusicChanged;
		private static event Action                 PunishAdWatched;
		private static event Action<bool> 			RestartScene;
		private static event Action                 Resumed;
		private static event Action                 RewardAdWatched;

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

				case SelfiePussEvent.CountDownOver:
					CountDownOver += actionFunction;
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion += actionFunction;
				return;

				case SelfiePussEvent.CollidedWithEnemy:
					OutOfWater += actionFunction;
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

				case SelfiePussEvent.CountDownOver:
					CountDownOver -= actionFunction;
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion -= actionFunction;
				return;

				case SelfiePussEvent.CollidedWithEnemy:
					OutOfWater -= actionFunction;
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

				case SelfiePussEvent.CountDownOver:
					CountDownOver?.Invoke();
				return;

				case SelfiePussEvent.NewVersion:
					NewVersion?.Invoke();
				return;

				case SelfiePussEvent.CollidedWithEnemy:
					OutOfWater?.Invoke();
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