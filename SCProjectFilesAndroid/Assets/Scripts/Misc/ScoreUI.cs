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

    private void OnTimeToSpawnCoinPointsPrefab(Vector2 coinPosition , CoinDataSO coinDataSO)
    {
        Instantiate(coinDataSO.GetCoinPointsPrefab() , coinPosition , Quaternion.identity);
        _spawnedPointsPrefabObj = GameObject.FindGameObjectWithTag("Points");
        _spawnedPointsPrefabObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(coinPosition.x + _xOffset , coinPosition.y);
    }

    private void OnTimeToSpawnMeteorPointsPrefab(Vector2 explosionPosition)
    {
        Instantiate(_meteorDataSO.GetMeteorSmashedPointsPrefab() , explosionPosition , Quaternion.identity);
        _spawnedPointsPrefabObj = GameObject.FindGameObjectWithTag("Points");
        _spawnedPointsPrefabObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(explosionPosition.x + _xOffset , explosionPosition.y);
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SpawnCoinPointsPrefab , OnTimeToSpawnCoinPointsPrefab);
        EventsManager.SubscribeToEvent(SelfiePussEvent.SpawnMeteorPointsPrefab , OnTimeToSpawnMeteorPointsPrefab);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.ScoreUpdate , OnScoreUpdate);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SpawnCoinPointsPrefab , OnTimeToSpawnCoinPointsPrefab);
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.SpawnMeteorPointsPrefab , OnTimeToSpawnMeteorPointsPrefab);
    }
}
