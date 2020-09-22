using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_COINS_PART = 10.0f;
    private Vector3 _lastEndPosition;

    [SerializeField] private Transform _puss;
    [SerializeField] private List<Transform> _coinTransformsList;
    [SerializeField] private Transform _coinEndPositionTransform;

    private void Awake() 
    {
        _lastEndPosition = _coinEndPositionTransform.transform.position;
    }

    private void Update() 
    { 
        if(Time.timeScale == 0)
        {
            return;
        }

        if(Vector3.Distance(_puss.position , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_COINS_PART) 
        {
            // Spawn another coins part
            SpawnCoinsPart();
        }
    }

    private void SpawnCoinsPart() 
    {
        Transform chosenPlatformToSpawn = _coinTransformsList[Random.Range(0 , _coinTransformsList.Count)];
        Transform lastLandPartTransform = SpawnCoinsPart(chosenPlatformToSpawn , _lastEndPosition);
        _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnCoinsPart(Transform coinsPartToSpawn , Vector3 spawnPosition) 
    {
        _coinEndPositionTransform = Instantiate(coinsPartToSpawn , new Vector3(spawnPosition.x , coinsPartToSpawn.position.y , spawnPosition.z) , Quaternion.identity);
        return _coinEndPositionTransform;
    }
}
