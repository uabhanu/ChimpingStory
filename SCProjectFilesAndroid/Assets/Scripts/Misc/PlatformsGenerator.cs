using System;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_PLATFORMS_PART = 20.0f;
    private Vector3 _lastEndPosition;
    

    [SerializeField] private float           _spawnYPosition;
    [SerializeField] private Transform       _puss;
    [SerializeField] private List<Transform> _platformTransformsList;
    [SerializeField] private Transform       _platformEndPositionTransform;
    [SerializeField] private SpawnPool       _spawnPool;

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

        SpawnNewTerrain();
    }

    private void SpawnNewTerrain()
    {
        if (Vector3.Distance(_puss.position, _lastEndPosition) < PLAYER_DISTANCE_SPAWN_PLATFORMS_PART)
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }
    
    private void SpawnLandPart() 
    {
        var chosenPlatformToSpawn = _platformTransformsList[Random.Range(0 , _platformTransformsList.Count)];
        _lastEndPosition = _spawnPool.Spawn(chosenPlatformToSpawn
                                          , new Vector3(_lastEndPosition.x, _spawnYPosition, _lastEndPosition.z)
                                          , Quaternion.identity
                                           ).Find("EndPosition").position;
    }
}

