using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private float _xOffset;
    [SerializeField] private GameObject _spawnedPointsPrefabObj;
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

    private void OnTimeToSpawnPointsPrefab(Vector2 explosionPosition)
    {
        Instantiate(_meteorDataSO.GetMeteorSmashedPointsPrefab() , explosionPosition , Quaternion.identity);
        _spawnedPointsPrefabObj = GameObject.FindGameObjectWithTag("Points");
         //Anchored Position or Local Position at the left giving the same result & whether explosion position or player position assigned, the points prefab keeps moving to the right which is not desirable
        _spawnedPointsPrefabObj.GetComponent<RectTransform>().anchoredPosition = explosionPosition;
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
