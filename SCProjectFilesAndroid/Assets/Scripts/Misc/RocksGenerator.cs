using UnityEngine;

public class RocksGenerator : MonoBehaviour 
{
    private float _nextActionTime = 0.0f;

    [SerializeField] private float _period;
    [SerializeField] private float _maxXPosOffset;
    [SerializeField] private float _minXPosOffset;
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
                SpawnRock(); //TODO When this runs first time, the rocks number is a lot higher than after that which may need to be fixed
            }
        }
    }

    private void SpawnRock() 
    {
        float _pussXPosition = _landPuss.GetPosition().x;
        int randomYPositionIndex = Random.Range(0 , _yPositions.Length);
        Instantiate(_rockTransformToSpawn , new Vector3(_pussXPosition + Random.Range(_minXPosOffset , _maxXPosOffset) , _yPositions[randomYPositionIndex] , transform.position.z) , Quaternion.identity);
    }
}

