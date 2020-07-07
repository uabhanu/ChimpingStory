using UnityEngine;

public class LandGenerator : MonoBehaviour 
{

    private const float PLAYER_DISTANCE_SPAWN_LAND_PART = 200.0f;

    [SerializeField] private Transform _landPartStart;
    [SerializeField] private Transform _landPartToSpawn;
    [SerializeField] private LandPuss _landPuss;

    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _landPartStart.transform.position;

        int startingSpawnLandParts = 5;

        for(int i = 0; i < startingSpawnLandParts; i++) 
        {
            SpawnLandPart();
        }
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
        Transform chosenLandPart = _landPartToSpawn;
        Transform lastLandPartTransform = SpawnLandPart(chosenLandPart , _lastEndPosition);
        _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLandPart(Transform landPart , Vector3 spawnPosition) 
    {
        float randomYPos = Random.Range(spawnPosition.y - 1.5f , spawnPosition.y + 1.5f);
        Transform landPartTransform = Instantiate(landPart , new Vector3(spawnPosition.x , randomYPos , spawnPosition.z) , Quaternion.identity);
        return landPartTransform;
    }

}

