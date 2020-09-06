using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    //TODO Turn all of these generators into one BackgroundGenerator and instead of LandPuss, WaterPuss, etc. use Transform Puss and leave PlatformGenerator as it is
    private const float PLAYER_DISTANCE_SPAWN_CLOUDS_PART = 60.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _cloudsEndPosition;
    [SerializeField] private Transform _cloudsPartToSpawn;

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
