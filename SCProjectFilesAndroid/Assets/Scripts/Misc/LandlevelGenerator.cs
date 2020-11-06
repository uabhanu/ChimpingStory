using System;
using System.Collections.Generic;
using PathologicalGames;
using SelfiePuss.Utilities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LandlevelGenerator : MonoBehaviour 
{
    private const                float   PLAYER_DISTANCE_SPAWN_PLATFORMS_PART = 20.0f;
    [SerializeField] private     Vector3 _lastEndPosition;
    private                      float   currentX             = 1.0f;
    [SerializeField] private     float   perlinIncrementValue = 0.5f;

    [SerializeField] private float     _spawnYPosition;
    [SerializeField] private float     _spawnHighYPosition;
    [SerializeField] private float     _spawnLowYPosition;
    
    [SerializeField] private Transform _puss;
    //[SerializeField] private List<Transform> _platformTransformsList;
    [FormerlySerializedAs("_spawnPool")] [SerializeField] private SpawnPool _platformsPool;
    [SerializeField]                                      private SpawnPool _coinsPool;

    private       float perlinValue;
    private       float remappedPerlinValue;
    private       float lowestPerlin                       = 0.4f;
    private       float highestPerlin                      = 0.75f;
    private const uint  NUM_PLATFORMS_TO_GENERATE_ON_START = 30;
    
    private void Start()
    {
        for (int numGenerations = 0; numGenerations < NUM_PLATFORMS_TO_GENERATE_ON_START; numGenerations++)
        {
            SpawnLandPart();
        }
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
        perlinValue =  PerlinNoise.FBM(currentX, 6, 0.75f);
        //perlinValue =  PerlinNoise.Perlin(currentX);
        if (perlinValue < lowestPerlin)
        {
            lowestPerlin = perlinValue;
        }
        else if (perlinValue > highestPerlin)
        {
            highestPerlin = perlinValue;
        }
        remappedPerlinValue =  perlinValue.Map(lowestPerlin, highestPerlin, _spawnLowYPosition, _spawnHighYPosition);
        currentX            += perlinIncrementValue;
        var chosenPlatformToSpawn = _platformsPool._perPrefabPoolOptions[Random.Range(0 , _platformsPool._perPrefabPoolOptions.Count)].prefab;

        _lastEndPosition.y = remappedPerlinValue;
        var spawnedTransform = _platformsPool.Spawn(chosenPlatformToSpawn, _lastEndPosition, Quaternion.identity);

        var spriteRenderer = spawnedTransform.gameObject.GetComponent<SpriteRenderer>();
        var size = spriteRenderer.size;

        _lastEndPosition.x += size.x;
        //_lastEndPosition = _spawnPool.Spawn(chosenPlatformToSpawn
        //                                  , new Vector3(_lastEndPosition.x, _spawnYPosition, _lastEndPosition.z)
        //                                  , Quaternion.identity
        //                                   ).Find("EndPosition").position;
    }
}

