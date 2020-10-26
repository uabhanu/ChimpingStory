using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject _pointsPrefab;
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

    private void OnSpawnPointsPrefab(RectTransform pointsPrefabPosition)
    {
        SetPointsPrefabPosition(pointsPrefabPosition);
    }

    private void OnTimeToSpawnPointsPrefab(RectTransform unusedVariable)
    {
        Instantiate(_meteorDataSO.GetMeteorSmashedPointsPrefab() , transform.position , Quaternion.identity);
    }

    private void SetPointsPrefabPosition(RectTransform pointsPrefabPosition)
    {
        _pointsPrefab.transform.position = pointsPrefabPosition.position;
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SpawnPointsPrefab , OnTimeToSpawnPointsPrefab);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SpawnPointsPrefab , OnSpawnPointsPrefab);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SpawnPointsPrefab , OnTimeToSpawnPointsPrefab);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SpawnPointsPrefab , OnSpawnPointsPrefab);
    }
}
