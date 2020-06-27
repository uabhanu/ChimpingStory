/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour 
{

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform _levelPartStart;
    [SerializeField] private List<Transform> _levelPartList;
    [SerializeField] private LandPuss _landPuss;

    private Vector3 lastEndPosition;

    private void Awake() 
    {
        lastEndPosition = _levelPartStart.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;

        for(int i = 0; i < startingSpawnLevelParts; i++) 
        {
            SpawnLevelPart();
        }
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) 
        {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() 
    {
        Transform chosenLevelPart = _levelPartList[Random.Range(0 , _levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart , lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart , Vector3 spawnPosition) 
    {
        Transform levelPartTransform = Instantiate(levelPart , spawnPosition , Quaternion.identity);
        return levelPartTransform;
    }

}
