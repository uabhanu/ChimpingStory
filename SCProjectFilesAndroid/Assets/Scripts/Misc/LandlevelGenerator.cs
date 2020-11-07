using PathologicalGames;
using SelfiePuss.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LandlevelGenerator : MonoBehaviour 
{
    private const uint  NUM_PLATFORMS_TO_GENERATE_ON_START = 30;
    private const float   PLAYER_DISTANCE_SPAWN_PLATFORMS_PART = 20.0f;
    
    private float   currentX             = 1.0f;
    private float perlinValue;
    private float remappedPerlinValue;
    private float lowestPerlin           = 0.4f;
    private float highestPerlin          = 0.75f;
    
    [FormerlySerializedAs("_spawnPool")] [SerializeField] private SpawnPool _platformsPool;

    [SerializeField] private     float   perlinIncrementValue;
    //[SerializeField] private float     _spawnYPosition; //This seems to have no use so commented it for now
    [SerializeField] private float     _spawnHighYPosition;
    [SerializeField] private float     _spawnLowYPosition;
    [SerializeField] private SpawnPool _coinsPool;
    [SerializeField] private Transform _puss;
    [SerializeField] private Vector3 _lastEndPosition;
    
    private void Start()
    {
        for(int numGenerations = 0; numGenerations < NUM_PLATFORMS_TO_GENERATE_ON_START; numGenerations++)
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
        if(Vector3.Distance(_puss.position , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_PLATFORMS_PART)
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }
    
    private void SpawnLandPart()
    {
        perlinValue =  PerlinNoise.FBM(currentX , 6 , 0.75f);
        //perlinValue =  PerlinNoise.Perlin(currentX);
        if(perlinValue < lowestPerlin)
        {
            lowestPerlin = perlinValue;
        }

        else if(perlinValue > highestPerlin)
        {
            highestPerlin = perlinValue;
        }

        remappedPerlinValue =  perlinValue.Map(lowestPerlin , highestPerlin , _spawnLowYPosition , _spawnHighYPosition);
        currentX            += perlinIncrementValue;
        var chosenPlatformToSpawn = _platformsPool._perPrefabPoolOptions[Random.Range(0 , _platformsPool._perPrefabPoolOptions.Count)].prefab;

        // Instead of _lastEndPosition, I passed in _lastEndPosition.x & PlatformYPosition due to the different types of platforms and hence commented the line below as well
        //_lastEndPosition.y = remappedPerlinValue;
        var spawnedTransform = _platformsPool.Spawn(chosenPlatformToSpawn , new Vector2(_lastEndPosition.x , chosenPlatformToSpawn.position.y) , Quaternion.identity);
        var spriteRenderer = spawnedTransform.gameObject.GetComponent<SpriteRenderer>();
        var size = spriteRenderer.size;

        _lastEndPosition.x += size.x;
        Debug.Log("LastEndPosition : " + _lastEndPosition);

        //_lastEndPosition = _spawnPool.Spawn(chosenPlatformToSpawn
        //                                  , new Vector3(_lastEndPosition.x, _spawnYPosition, _lastEndPosition.z)
        //                                  , Quaternion.identity
        //                                   ).Find("EndPosition").position;
    }
}

