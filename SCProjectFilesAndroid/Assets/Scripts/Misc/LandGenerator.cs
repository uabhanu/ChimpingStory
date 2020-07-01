using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour 
{

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200.0f;

    [SerializeField] private Transform _levelPartStart;
    [SerializeField] private List<Transform> _levelPartList;
    [SerializeField] private LandPuss _landPuss;

    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _levelPartStart.transform.position;

        int startingSpawnLevelParts = 5;

        for(int i = 0; i < startingSpawnLevelParts; i++) 
        {
            SpawnLevelPart();
        }
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) 
        {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() 
    {
        Transform chosenLevelPart = _levelPartList[Random.Range(0 , _levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart , _lastEndPosition);
        _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart , Vector3 spawnPosition) 
    {
        Transform levelPartTransform = Instantiate(levelPart , spawnPosition , Quaternion.identity);
        return levelPartTransform;
    }

}
