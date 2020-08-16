using UnityEngine;

public class RocksGenerator : MonoBehaviour 
{
    private float _nextActionTime = 0.0f;

    [SerializeField] private float _period;
    [SerializeField] private float[] _yPositions;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Transform _rockTransformToSpawn;

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper) 
        {
            if(Time.time > _nextActionTime)
            {
                _nextActionTime += _period;
                SpawnRock(); //TODO Figure out a way to Spawn only one Rock instead of a bunch when this runs the first time
            }
        }
    }

    private void SpawnRock() 
    {
        int randomYPositionIndex = Random.Range(0 , _yPositions.Length);
        Instantiate(_rockTransformToSpawn , new Vector3(_landPuss.GetPosition().x , _yPositions[randomYPositionIndex] , transform.position.z) , Quaternion.identity);
        Debug.Log("Spawned the Rock");
    }
}

