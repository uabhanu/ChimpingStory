using SelfiePuss.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _meteorScoreIncrementValue = 100 , _coinScoreIncrementValue , _scoreValue;
    
    [SerializeField] private Coin _coinReference;
    [SerializeField] private ScoreManagerSO _scoreManagerSO;

    private void Awake()
    {
        RegisterEvents();
    }

    private void Start()
    {
        _coinScoreIncrementValue = 0;
        _scoreValue = BhanuPrefs.GetHighScore();
        _scoreManagerSO.SetScoreValue(_scoreValue);
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate);
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void IncrementScore(int incrementValue)
    {
        _scoreValue += incrementValue;
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreUpdate , _scoreValue);
    }

    private void OnAdsSkipped()
    {
        _scoreValue = 0;
        _scoreManagerSO.SetScoreValue(_scoreValue);
    }

    private void OnGetCoinReference()
    {
        _coinScoreIncrementValue = _coinReference.GetScoreIncrementValue(); //This is not working
    }

    private void OnScoreChangedByCoin()
    {
        IncrementScore(_coinScoreIncrementValue);
        _scoreManagerSO.SetScoreValue(_scoreValue);
    }

    private void OnScoreChangedByMeteor()
    {
        IncrementScore(_meteorScoreIncrementValue);
        _scoreManagerSO.SetScoreValue(_scoreValue);
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
        EventsManager.SubscribeToEvent(SelfiePussEvent.CoinCollected , OnGetCoinReference);
        EventsManager.SubscribeToEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.SubscribeToEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.SubscribeToEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.AdsSkipped , OnAdsSkipped);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.CoinCollected , OnGetCoinReference);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }
}
