using UnityEngine;

public class TreesGenerator : MonoBehaviour
{
    //TODO Backgrounds are overlapping due to the parallax and the reason is, this logic uses EndPosition of the 1st Part but when these are moving, it's no longer enough
    private const float PLAYER_DISTANCE_SPAWN_TREES_PART = 40.0f;

    [SerializeField] private Transform _treesEndPosition;
    [SerializeField] private Transform _treesPartToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _treesEndPosition.transform.position;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_TREES_PART) 
        {
            // Spawn another trees part
            SpawnTreesPart();
        }
    }

    private void SpawnTreesPart() 
    {
        Transform chosenTreesPart = _treesPartToSpawn;
        Transform lastTreesPartTransform = SpawnTreesPart(chosenTreesPart , _lastEndPosition);
        _lastEndPosition = lastTreesPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnTreesPart(Transform treesPart , Vector3 spawnPosition) 
    {
        Transform treesPartTransform = Instantiate(treesPart , new Vector3(spawnPosition.x , -1.23f , spawnPosition.z) , Quaternion.identity);
        return treesPartTransform;
    }
}
