using System.Collections.Generic;
using UnityEngine;

public class WaterCollectiblesGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_COLLECTIBLES_PART = 20.0f;
    private Vector3 _lastEndPosition;

    [SerializeField] private List<Transform> _collectibleTransformsList;
    [SerializeField] private Transform _collectibleEndPositionTransform;
    [SerializeField] private WaterPuss _waterPuss;

    private void Awake() 
    {
        _lastEndPosition = _collectibleEndPositionTransform.transform.position;
    }

    private void Update() 
    { 
        if(Time.timeScale == 0)
        {
            return;
        }

        if(Vector3.Distance(_waterPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_COLLECTIBLES_PART) 
        {
            // Spawn another level part
            SpawnCollectiblesPart();
        }
    }

    private void SpawnCollectiblesPart() 
    {
        Transform chosenCollectibleToSpawn = _collectibleTransformsList[Random.Range(0 , _collectibleTransformsList.Count)];
        Transform collectiblesLandPartTransform = SpawnCollectiblesPart(chosenCollectibleToSpawn , _lastEndPosition);
        _lastEndPosition = collectiblesLandPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnCollectiblesPart(Transform objectToSpawn , Vector3 spawnPosition) 
    {
        float randomYPosition = Random.Range(spawnPosition.y - 0.5f , spawnPosition.y + 0.5f);
        _collectibleEndPositionTransform = Instantiate(objectToSpawn , new Vector3(spawnPosition.x , Mathf.Clamp(randomYPosition , -4.50f , 0.70f) , spawnPosition.z) , Quaternion.identity);
        return _collectibleEndPositionTransform;
    }
}
