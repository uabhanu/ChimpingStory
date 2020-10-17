using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _scoreIncrementValue , _scoreValue;

    [SerializeField] private Coin _coinReference;
    [SerializeField] private ScoreManagerSO _scoreManagerSO;
    [SerializeField] private Text _highScoreValueText;

    private void Start()
    {
        _scoreIncrementValue = 0;
        _scoreValue = _scoreManagerSO.GetScoreValue();
        _highScoreValueText.text = _scoreManagerSO.GetScoreValue().ToString();
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void OnAdsSkipped()
    {
        _scoreValue = 0;
        _scoreManagerSO.SetScoreValue(_scoreValue);
        _highScoreValueText.text = _scoreManagerSO.GetScoreValue().ToString();
    }

    private void OnScoreChangedByCoin()
    {
        _scoreIncrementValue = _coinReference.GetScoreIncrementValue();
        _scoreValue += _scoreIncrementValue;  //This is working great
        _scoreManagerSO.SetScoreValue(_scoreValue);
        _highScoreValueText.text = _scoreManagerSO.GetScoreValue().ToString();
    }

    private void OnScoreChangedByMeteor()
    {
        _scoreValue += 100;
        _scoreManagerSO.SetScoreValue(_scoreValue);
        _highScoreValueText.text = _scoreManagerSO.GetScoreValue().ToString();
    }

    private void OnScoreRetainedByRewardAd()
    {
        _highScoreValueText.text = _scoreManagerSO.GetScoreValue().ToString();
    }

    private void OnScoreChangedBySelfie()
    {
        
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.AdsSkipped , OnAdsSkipped);
        EventsManager.SubscribeToEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.SubscribeToEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.SubscribeToEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.AdsSkipped , OnAdsSkipped);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.CoinCollected , OnScoreChangedByCoin);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.MeteorExplosion , OnScoreChangedByMeteor);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.RewardsAdWatched , OnScoreRetainedByRewardAd);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SelfieTaken , OnScoreChangedBySelfie);
    }
}
