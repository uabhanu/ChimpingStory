using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour 
{
    float objYPos;
    int i;

	public GameObject objToSpawn;
	public GameObject[] PF_ObjectsList;

	void Start()
	{
		StartCoroutine("SpawnTimer");
	}

	IEnumerator SpawnTimer()
	{
		yield return new WaitForSeconds(2.5f);
		i = Random.Range(0 , PF_ObjectsList.Length);
        objYPos = PF_ObjectsList[i].transform.position.y;
        objToSpawn = GameObject.FindGameObjectWithTag("OBJ");

		if(objToSpawn == null) 
		{
            objToSpawn = Instantiate(PF_ObjectsList[i] , new Vector2(transform.position.x , objYPos) , transform.rotation) as GameObject;
		} 

		StartCoroutine("SpawnTimer");
	}
}
