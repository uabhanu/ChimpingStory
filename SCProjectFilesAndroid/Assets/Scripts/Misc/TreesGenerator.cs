using UnityEngine;

public class TreesGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_TREES_PART = 40.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _treesEndPosition;
    [SerializeField] private Transform _treesPartToSpawn;

    private void Awake() 
    {
        _lastEndPositionTransform = _treesEndPosition;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_TREES_PART) 
        {
            // Spawn another trees part
            SpawnTreesPart();
        }
    }

    private void SpawnTreesPart() 
    {
        Transform chosenTreesPart = _treesPartToSpawn;
        Transform lastTreesPartTransform = SpawnTreesPart(chosenTreesPart , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastTreesPartTransform.Find("EndPosition");
    }

    private Transform SpawnTreesPart(Transform treesPart , Vector3 spawnPosition) 
    {
        Transform treesPartTransform = Instantiate(treesPart , new Vector3(spawnPosition.x , -1.23f , spawnPosition.z) , Quaternion.identity);
        return treesPartTransform;
    }
}
