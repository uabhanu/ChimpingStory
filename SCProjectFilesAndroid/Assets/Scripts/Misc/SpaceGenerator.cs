using UnityEngine;

public class SpaceGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_SPACE_PART = 40.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private Transform _layerEndPosition;
    [SerializeField] private Transform _spacePartToSpawn;
    [SerializeField] private SuperPuss _superPuss;

    private void Awake() 
    {
        _lastEndPositionTransform = _layerEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_superPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_SPACE_PART) 
        {
            SpawnSpacePart();
        }
    }

    private void SpawnSpacePart() 
    {
        Transform lastSpacePartTransform = SpawnSpacePart(_spacePartToSpawn , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastSpacePartTransform.Find("EndPosition");
    }

    private Transform SpawnSpacePart(Transform spacePart , Vector3 spawnPosition) 
    {
        Transform spacePartTransform = Instantiate(spacePart , new Vector3(spawnPosition.x , 0f , spawnPosition.z) , Quaternion.identity);
        return spacePartTransform;
    }
}
