using System.Collections.Generic;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_PLATFORMS_PART = 20.0f;
    private Vector3 _lastEndPosition;

    [SerializeField] private float _spawnYPosition;
    [SerializeField] private Transform _puss;
    [SerializeField] private List<Transform> _platformTransformsList;
    [SerializeField] private Transform _platformEndPositionTransform;

    private void Awake() 
    {
        _lastEndPosition = _platformEndPositionTransform.transform.position;
    }

    private void Update() 
    { 
        if(Time.timeScale == 0)
        {
            return;
        }

        if(Vector3.Distance(_puss.position , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_PLATFORMS_PART) 
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }

    private void SpawnLandPart() 
    {
        Transform chosenPlatformToSpawn = _platformTransformsList[Random.Range(0 , _platformTransformsList.Count)];
        Transform lastLandPartTransform = SpawnLandPart(chosenPlatformToSpawn , _lastEndPosition);
        _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLandPart(Transform platformsPartToSpawn , Vector3 spawnPosition) 
    {
        _platformEndPositionTransform = Instantiate(platformsPartToSpawn , new Vector3(spawnPosition.x , _spawnYPosition , spawnPosition.z) , Quaternion.identity);
        return _platformEndPositionTransform;
    }
}

