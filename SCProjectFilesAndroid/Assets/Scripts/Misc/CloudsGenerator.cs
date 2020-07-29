using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    //TODO Backgrounds are overlapping due to the parallax and the reason is, this logic uses EndPosition of the 1st Part but when these are moving, it's no longer enough
    private const float PLAYER_DISTANCE_SPAWN_CLOUDS_PART = 40.0f;
    [SerializeField] private float _endPositionOffset;

    [SerializeField] private Transform _cloudsEndPosition;
    [SerializeField] private Transform _cloudsPartToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _cloudsEndPosition.transform.position;
        _endPositionOffset = _lastEndPosition.x - _landPuss.GetPosition().x;
        Debug.Log("Last End Position Position Offset : " + _endPositionOffset);
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_CLOUDS_PART) 
        {
            // Spawn another clouds part
            //Debug.Log("Distance between player & Clouds : " + Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition));
            SpawnCloudsPart();
        }
    }

    private void SpawnCloudsPart() 
    {
        Transform chosenCloudsPart = _cloudsPartToSpawn;
        Transform lastCloudsPartTransform = SpawnCloudsPart(chosenCloudsPart , _lastEndPosition);
        _lastEndPosition = lastCloudsPartTransform.Find("EndPosition").position;
        _endPositionOffset = _lastEndPosition.x - _landPuss.GetPosition().x;
        Debug.Log("Last End Position Offset : " + _endPositionOffset);
        //Debug.Log("Last End position : " + _lastEndPosition);
    }

    private Transform SpawnCloudsPart(Transform cloudsPart , Vector3 spawnPosition) 
    {
        Transform cloudsPartTransform = Instantiate(cloudsPart , new Vector3(spawnPosition.x  , 0.33f , spawnPosition.z) , Quaternion.identity);
        return cloudsPartTransform;
    }
}
