using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_WATER_PART = 40.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private Transform _waterEndPosition;
    [SerializeField] private Transform _waterPartToSpawn;
    [SerializeField] private WaterPuss _waterPuss;

    private void Awake() 
    {
        _lastEndPositionTransform = _waterEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_waterPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_WATER_PART) 
        {
            SpawnWaterPart();
        }
    }

    private void SpawnWaterPart() 
    {
        Transform lastWaterPartTransform = SpawnWaterPart(_waterPartToSpawn , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastWaterPartTransform.Find("EndPosition");
    }

    private Transform SpawnWaterPart(Transform waterPart , Vector3 spawnPosition) 
    {
        Transform waterPartTransform = Instantiate(waterPart , new Vector3(spawnPosition.x  , 0.33f , spawnPosition.z) , Quaternion.identity);
        return waterPartTransform;
    }
}
