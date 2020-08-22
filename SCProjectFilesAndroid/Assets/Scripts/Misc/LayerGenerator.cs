using UnityEngine;

public class LayerGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LAYER_PART = 40.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private Transform _layerEndPosition;
    [SerializeField] private Transform _layerPartToSpawn;
    [SerializeField] private WaterPuss _waterPuss;

    private void Awake() 
    {
        _lastEndPositionTransform = _layerEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_waterPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_LAYER_PART) 
        {
            SpawnLayerPart();
        }
    }

    private void SpawnLayerPart() 
    {
        Transform lastLayerPartTransform = SpawnLayerPart(_layerPartToSpawn , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastLayerPartTransform.Find("EndPosition");
    }

    private Transform SpawnLayerPart(Transform layerPart , Vector3 spawnPosition) 
    {
        Transform waterPartTransform = Instantiate(layerPart , new Vector3(spawnPosition.x , 0f , spawnPosition.z) , Quaternion.identity);
        return waterPartTransform;
    }
}
