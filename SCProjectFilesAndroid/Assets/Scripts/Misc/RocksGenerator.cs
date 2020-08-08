using UnityEngine;

public class RocksGenerator : MonoBehaviour 
{
    private float _nextActionTime = 0.0f;

    [SerializeField] private float _period = 1.1f;
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
                if(_nextActionTime > 2.2f) //On the 1st Spawn, too many rocks are spawned which is why I asked update not to spawn until _nextActionTime > 2.2f and it works great now!!
                {
                    SpawnRock();
                }

                _nextActionTime += _period;
            }
        }
    }

    private void SpawnRock() 
    {
        int randomYPositionIndex = Random.Range(0 , _yPositions.Length);
        Instantiate(_rockTransformToSpawn , new Vector3(transform.position.x , _yPositions[randomYPositionIndex] , transform.position.z) , Quaternion.identity);
    }
}

