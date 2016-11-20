using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	private int i;

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
		enemyObj = GameObject.FindGameObjectWithTag("EnemyCombo");

		if(enemyObj == null) 
		{
			enemyObj = Instantiate(PF_EnemyCombos[i], transform.position, transform.rotation) as GameObject;
		} 

		StartCoroutine("SpawnTimer");
	}
}
