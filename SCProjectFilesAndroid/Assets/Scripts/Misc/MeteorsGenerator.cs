using UnityEngine;

public class MeteorsGenerator : MonoBehaviour 
{
    private float _nextActionTime = 0.0f;

    [SerializeField] [Tooltip ("Lower the value, more meteors and vice versa")] private float _period;
    [SerializeField] private float[] _yPositions;
    [SerializeField] private SuperPuss _superPuss;
    [SerializeField] private Transform _meteorTransformToSpawn;

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if((Time.time - _nextActionTime) > _period)
        {
            _nextActionTime = Time.time;
            SpawnMeteor();
        }        
    }

    private void SpawnMeteor() 
    {
        int randomYPositionIndex = Random.Range(0 , _yPositions.Length);
        Instantiate(_meteorTransformToSpawn , new Vector3(_superPuss.GetPosition().x , _yPositions[randomYPositionIndex] , transform.position.z) , Quaternion.identity);
    }
}

