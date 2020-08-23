using UnityEngine;

public class RocksGenerator : MonoBehaviour 
{
    private float _nextActionTime = 0.0f;

    [SerializeField] private float _period;
    [SerializeField] private float[] _yPositions;
    [SerializeField] private SuperPuss _superPuss;
    [SerializeField] private Transform _rockTransformToSpawn;

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if((Time.time - _nextActionTime) > _period)
        {
            _nextActionTime = Time.time;
            SpawnRock();
        }        
    }

    private void SpawnRock() 
    {
        int randomYPositionIndex = Random.Range(0 , _yPositions.Length);
        Instantiate(_rockTransformToSpawn , new Vector3(_superPuss.GetPosition().x , _yPositions[randomYPositionIndex] , transform.position.z) , Quaternion.identity);
        Debug.Log("Spawned the Rock");
    }
}

