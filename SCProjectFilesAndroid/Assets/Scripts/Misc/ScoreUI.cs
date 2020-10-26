using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private MeteorDataSO _meteorDataSO;
    [SerializeField] private Text _highScoreValueText;

    private void Awake()
    {
        RegisterEvents();
        UpdateScore(BhanuPrefs.GetHighScore());
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void OnScoreUpdate(int scoreValue)
    {
        _highScoreValueText.text = scoreValue.ToString();
    }

    private void UpdateScore(int scoreValue)
    {
        _highScoreValueText.text = scoreValue.ToString();
    }

    private void OnTimeToSpawnPointsPrefab(Transform pointsPrefabPosition)
    {
        Instantiate(_meteorDataSO.GetMeteorSmashedPointsPrefab() , pointsPrefabPosition.position , Quaternion.identity);
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SpawnPointsPrefab , OnTimeToSpawnPointsPrefab);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SpawnPointsPrefab , OnTimeToSpawnPointsPrefab);
    }
}
