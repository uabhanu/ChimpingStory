using UnityEngine;

public class PlatformsGenerator : MonoBehaviour 
{

    private const float PLAYER_DISTANCE_SPAWN_LAND_PART = 200.0f;
    private const int MAX_PLATFORMS = 3;

    [SerializeField] private Transform _landEndPosition;
    [SerializeField] private Transform _landPartToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    public static int m_TotalPlatforms;

    private void Awake() 
    {
        _lastEndPosition = _landEndPosition.transform.position;
        m_TotalPlatforms = 0;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LAND_PART) 
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }

    private void SpawnLandPart() 
    {
        if(m_TotalPlatforms < MAX_PLATFORMS)
        {
            Transform chosenLandPart = _landPartToSpawn;
            Transform lastLandPartTransform = SpawnLandPart(chosenLandPart , _lastEndPosition);
            _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
            m_TotalPlatforms++;
        }
    }

    private Transform SpawnLandPart(Transform landPart , Vector3 spawnPosition) 
    {
        float randomYPos = Random.Range(spawnPosition.y - 1.5f , spawnPosition.y + 1.5f);
        Transform landPartTransform = Instantiate(landPart , new Vector3(spawnPosition.x , Mathf.Clamp(randomYPos , -4.90f , 0.70f) , spawnPosition.z) , Quaternion.identity);
        return landPartTransform;
    }

}

