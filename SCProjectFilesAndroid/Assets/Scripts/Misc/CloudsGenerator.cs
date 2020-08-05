using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    //TODO Night Mode
    private const float PLAYER_DISTANCE_SPAWN_CLOUDS_PART = 40.0f;

    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _cloudsEndPosition;
    [SerializeField] private Transform _cloudsPartToSpawn;
    [SerializeField] private Transform _lastEndPositionTransform;

    private void Awake() 
    {
        _lastEndPositionTransform = _cloudsEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_CLOUDS_PART) 
        {
            SpawnCloudsPart();
        }
    }

    private void SpawnCloudsPart() 
    {
        Transform chosenCloudsPart = _cloudsPartToSpawn;
        Transform lastCloudsPartTransform = SpawnCloudsPart(chosenCloudsPart , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastCloudsPartTransform.Find("EndPosition");
    }

    private Transform SpawnCloudsPart(Transform cloudsPart , Vector3 spawnPosition) 
    {
        Transform cloudsPartTransform = Instantiate(cloudsPart , new Vector3(spawnPosition.x  , 0.33f , spawnPosition.z) , Quaternion.identity);
        return cloudsPartTransform;
    }
}
