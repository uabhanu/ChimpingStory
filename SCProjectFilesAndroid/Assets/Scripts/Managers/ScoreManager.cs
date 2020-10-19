using SelfiePuss.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _meteorScoreIncrementValue = 100 , _coinScoreIncrementValue , _scoreValue;

    private void Awake()
    {
        RegisterEvents();
    }

    private void Start()
    {
        _coinScoreIncrementValue = 0;
        _scoreValue = BhanuPrefs.GetHighScore();
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate);
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void IncrementScore(int incrementValue)
    {
        _scoreValue += incrementValue;
        BhanuPrefs.SetHighScore(_scoreValue);
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate , _scoreValue);
    }

    private void OnAdsSkipped()
    {
        _scoreValue = 0;
        BhanuPrefs.SetHighScore(_scoreValue);
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate , _scoreValue);
    }

    private void OnIncrementValueReceived()
    {
         
    }

    private void OnScoreChangedByCoin()
    {
        _coinScoreIncrementValue = 25; //TODO Getting the increment value from the Coin through an event WIP
        IncrementScore(_coinScoreIncrementValue);
    }

    private void OnScoreChangedByMeteor()
    {
        IncrementScore(_meteorScoreIncrementValue);
    }

    private void OnScoreRetainedByRewardAd()
    {
        _scoreValue = BhanuPrefs.GetHighScore();
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate);
    }

    private void OnScoreChangedBySelfie()
    {
        
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.AdsSkipped , OnAdsSkipped);
        EventsManager.SubscribeToEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.SubscribeToEvent(SelfiePussEvent.IncrementValueReceived , OnIncrementValueReceived);
        EventsManager.SubscribeToEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.SubscribeToEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.AdsSkipped , OnAdsSkipped);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.IncrementValueReceived , OnIncrementValueReceived);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }
}
