using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_BACKGROUND_PART = 40.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private Transform _backgroundEndPosition;
    [SerializeField] private List<Transform> _backgroundTransformsList;
    [SerializeField] private Transform _puss;

    private void Awake() 
    {
        _lastEndPositionTransform = _backgroundEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_puss.position , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_BACKGROUND_PART) 
        {
            SpawnBackgroundPart();
        }
    }

    private void SpawnBackgroundPart() 
    {
        Transform chosenBackgroundPart = _backgroundTransformsList[Random.Range(0 , _backgroundTransformsList.Count)];
        Transform lastBackgroundPartTransform = SpawnBackgroundPart(chosenBackgroundPart , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastBackgroundPartTransform.Find("EndPosition");
    }

    private Transform SpawnBackgroundPart(Transform backgroundPart , Vector3 spawnPosition) 
    {
        Transform backgroundPartTransform = Instantiate(backgroundPart , new Vector3(spawnPosition.x  , backgroundPart.position.y , spawnPosition.z) , Quaternion.identity);
        return backgroundPartTransform;
    }
}
