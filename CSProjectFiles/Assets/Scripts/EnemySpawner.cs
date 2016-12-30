using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    float enemyYPos;
    int i;

	public GameObject enemyObj;
	public GameObject[] PF_EnemyCombos;

	void Start()
	{
		StartCoroutine("SpawnTimer");
	}

	IEnumerator SpawnTimer()
	{
		yield return new WaitForSeconds(2.5f);
		i = Random.Range(0 , PF_EnemyCombos.Length);
        enemyYPos = PF_EnemyCombos[i].transform.position.y;
        enemyObj = GameObject.FindGameObjectWithTag("EnemyCombo");

		if(enemyObj == null) 
		{
			enemyObj = Instantiate(PF_EnemyCombos[i] , new Vector2(transform.position.x , enemyYPos) , transform.rotation) as GameObject;
		} 

		StartCoroutine("SpawnTimer");
	}
}
