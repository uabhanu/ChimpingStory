using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    int i;

    [SerializeField] float spawnTime;
    [SerializeField] GameObject objToSpawn;
    [SerializeField] GameObject[] PF_ObjectsList;
    
    void Start()
    {
        StartCoroutine("TetrisSpawning");
    }
	
    IEnumerator TetrisSpawning()
    {
        yield return new WaitForSeconds(spawnTime);
        i = Random.Range(0, PF_ObjectsList.Length);
        objToSpawn = Instantiate(PF_ObjectsList[i], new Vector3(Random.Range(-4f, 2f), 3f, 0f), transform.rotation) as GameObject;
        StartCoroutine("TetrisSpawning");
    }
}
