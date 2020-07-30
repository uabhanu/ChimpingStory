using UnityEngine;

public class FarTreesGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_FAR_TREES_PART = 40.0f;

    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _farTreesEndPosition;
    [SerializeField] private Transform _farTreesPartToSpawn;
    [SerializeField] private Transform _lastEndPositionTransform;

    private void Awake() 
    {
        _lastEndPositionTransform = _farTreesEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_FAR_TREES_PART) 
        {
            SpawnFarTreesPart();
        }
    }

    private void SpawnFarTreesPart() 
    {
        Transform chosenFarTreesPart = _farTreesPartToSpawn;
        Transform lastFarTreesPartTransform = SpawnFarTreesPart(chosenFarTreesPart , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastFarTreesPartTransform.Find("EndPosition");
    }

    private Transform SpawnFarTreesPart(Transform farTreesPart , Vector3 spawnPosition) 
    {
        Transform farTreesPartTransform = Instantiate(farTreesPart , new Vector3(spawnPosition.x , -0.66f , spawnPosition.z) , Quaternion.identity);
        return farTreesPartTransform;
    }
}
