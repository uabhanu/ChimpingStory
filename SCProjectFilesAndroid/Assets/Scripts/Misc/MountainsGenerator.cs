using UnityEngine;

public class MountainsGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_MOUNTAINS_PART = 40.0f;

    [SerializeField] private Transform _mountainsEndPosition;
    [SerializeField] private Transform _mountainsPartToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _mountainsEndPosition.transform.position;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_MOUNTAINS_PART) 
        {
            // Spawn another cloud part
            SpawnMountainsPart();
        }
    }

    private void SpawnMountainsPart() 
    {
        Transform chosenMountainsPart = _mountainsPartToSpawn;
        Transform lastMountainsPartTransform = SpawnMountainsPart(chosenMountainsPart , _lastEndPosition);
        _lastEndPosition = lastMountainsPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnMountainsPart(Transform mountainsPart , Vector3 spawnPosition) 
    {
        Transform mountainsPartTransform = Instantiate(mountainsPart , new Vector3(spawnPosition.x , -3.29f , spawnPosition.z) , Quaternion.identity);
        return mountainsPartTransform;
    }
}
