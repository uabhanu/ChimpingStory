using UnityEngine;

public class RocksGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_ROCK = 40.0f;
    private float _nextActionTime = 0.0f;
    private Transform _lastEndPositionTransform;

    [SerializeField] private float[] _yPositions; 
    [SerializeField] private float _period;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _rockPrefab;

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper)
        {
            if(Time.time > _nextActionTime && _lastEndPositionTransform == null)
            {
                _nextActionTime += _period;
                _lastEndPositionTransform = _rockPrefab.transform;
            }

            if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPositionTransform.position) < PLAYER_DISTANCE_SPAWN_ROCK) 
            {
                SpawnRock();
            }
        }
        else
        {
            return;
        }
    }

    private void SpawnRock() 
    {
        Transform lastRockTransform = SpawnRock(_rockPrefab , _lastEndPositionTransform.position);
        _lastEndPositionTransform = lastRockTransform.Find("EndPosition");
    }

    private Transform SpawnRock(Transform rock , Vector3 spawnPosition) 
    {
        int i = Random.Range(0 , _yPositions.Length);
        Transform rockTransform = Instantiate(rock , new Vector3(spawnPosition.x  , _yPositions[i] , spawnPosition.z) , Quaternion.identity);
        return rockTransform;
    }
}

