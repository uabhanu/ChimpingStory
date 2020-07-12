using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_CLOUDS_PART = 200.0f;

    [SerializeField] private Transform _cloudsEndPosition;
    [SerializeField] private Transform _cloudsPartToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _cloudsEndPosition.transform.position;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_CLOUDS_PART) 
        {
            // Spawn another cloud part
            SpawnCloudsPart();
        }
    }

    private void SpawnCloudsPart() 
    {
        Transform chosenCloudsPart = _cloudsPartToSpawn;
        Transform lastCloudsPartTransform = SpawnCloudsPart(chosenCloudsPart , _lastEndPosition);
        _lastEndPosition = lastCloudsPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnCloudsPart(Transform cloudsPart , Vector3 spawnPosition) 
    {
        Transform cloudsPartTransform = Instantiate(cloudsPart , new Vector3(spawnPosition.x , 0.7f , spawnPosition.z) , Quaternion.identity);
        return cloudsPartTransform;
    }
}
