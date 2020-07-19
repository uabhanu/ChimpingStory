using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_COLLECTIBLES_PART = 20.0f;
    private Vector3 _lastEndPosition;

    [SerializeField] private Transform _collectiblesEndPosition;
    [SerializeField] private Transform _collectiblesStart;
    [SerializeField] private List<Transform> _collectiblesList;
    [SerializeField] private LandPuss _landPuss;

    private void Awake() 
    {
        _lastEndPosition = _collectiblesEndPosition.transform.position;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_COLLECTIBLES_PART) 
        {
            // Spawn another collectibles part
            SpawnCollectiblesPart();
        }
    }

    private void SpawnCollectiblesPart() 
    {
        Transform chosenCollectiblesPart = _collectiblesList[Random.Range(0 , _collectiblesList.Count)];
        Transform lastCollectiblesPartTransform = SpawnCollectiblesPart(chosenCollectiblesPart , _lastEndPosition);
        _lastEndPosition = lastCollectiblesPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnCollectiblesPart(Transform collectiblesPart , Vector3 spawnPosition) 
    {
        Transform collectiblesPartTransform = Instantiate(collectiblesPart , spawnPosition , Quaternion.identity);
        return collectiblesPartTransform;
    }

}