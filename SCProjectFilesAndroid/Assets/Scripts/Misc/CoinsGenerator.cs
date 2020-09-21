using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    private float _nextActionTime = 0.0f;

    [SerializeField] [Tooltip ("Lower the value, more meteors and vice versa")] private float _period;
    [SerializeField] private List<Transform> _coinTransformsList;
    [SerializeField] private Transform _waterPuss;

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if((Time.time - _nextActionTime) > _period)
        {
            _nextActionTime = Time.time;
            SpawnCoins();
        }        
    }

    private void SpawnCoins() 
    {
        Transform chosenCoinsSetToSpawn = _coinTransformsList[Random.Range(0 , _coinTransformsList.Count)];
        Instantiate(chosenCoinsSetToSpawn , new Vector3(_waterPuss.position.x - 20f , transform.position.y , transform.position.z) , Quaternion.identity);
    }
}
