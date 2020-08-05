using System.Collections.Generic;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_LAND_PART = 20.0f;
    private Vector3 _lastEndPosition;

    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private List<Transform> _platformTransformsList;
    [SerializeField] private Transform _platformEndPositionTransform;
    [SerializeField] private Transform _rockTransform;

    private void Awake() 
    {
        _lastEndPosition = _platformEndPositionTransform.transform.position;
    }

    private void Update() 
    { 
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LAND_PART) 
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }

    private void SpawnLandPart() 
    {
        if(!_landPuss.m_isSuper)
        {
            Transform chosenObjectToSpawn = _platformTransformsList[Random.Range(0 , _platformTransformsList.Count)];
            Transform lastLandPartTransform = SpawnLandPart(chosenObjectToSpawn , _lastEndPosition);
            _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
        }

        else if(_landPuss.m_isSuper)
        {
            Transform chosenObjectToSpawn = _rockTransform;
            Transform lastLandPartTransform = SpawnLandPart(chosenObjectToSpawn , _lastEndPosition);
            _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
        }
    }

    private Transform SpawnLandPart(Transform objectToSpawn , Vector3 spawnPosition) 
    {
        if(!_landPuss.m_isSuper)
        {
            float randomYPosition = Random.Range(spawnPosition.y - 0.5f , spawnPosition.y + 0.5f); //TODO Ideal values yet to be decided
            Transform landPartTransform = Instantiate(objectToSpawn , new Vector3(spawnPosition.x , Mathf.Clamp(randomYPosition , -4.50f , 0.70f) , spawnPosition.z) , Quaternion.identity);
            return landPartTransform;
        }

        else if(_landPuss.m_isSuper)
        {
            float randomYPosition = Random.Range(spawnPosition.y - 2.5f , spawnPosition.y + 2.5f);
            Transform landPartTransform = Instantiate(objectToSpawn , new Vector3(spawnPosition.x , Mathf.Clamp(randomYPosition , -2.25f , 4.66f) , spawnPosition.z) , Quaternion.identity);
            return landPartTransform;
        }

        return null;
    }
}

