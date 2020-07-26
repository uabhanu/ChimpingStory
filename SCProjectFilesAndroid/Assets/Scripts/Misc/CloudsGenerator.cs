using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    //TODO Backgrounds are overlapping due to the parallax and the reason is, EndPosition now constantly changing so we now need a min offset check before spawning this part
    private const float PLAYER_DISTANCE_SPAWN_CLOUDS_PART = 40.0f;

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
            // Spawn another clouds part
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
        Transform cloudsPartTransform = Instantiate(cloudsPart , new Vector3(spawnPosition.x , 0.33f , spawnPosition.z) , Quaternion.identity);
        return cloudsPartTransform;
    }
}
