using System.Collections;
using UnityEngine;

public class BananaSpawner : MonoBehaviour 
{
	private int i;
	private GameSpawner gameSpawnScript;

	public ChimpController chimpControlScript;
	public float spawnTime;
	public GameObject bananaObj;
	public GameObject[] PF_BananaAndOthers;
	public GameObject[] PF_BananaCombos;

	void Start() 
	{
		gameSpawnScript = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();

		if(gameSpawnScript.level == 1)
		{
			StartCoroutine("Level1Spawner");
		}
			
		if(gameSpawnScript.level == 2)
		{
			StartCoroutine("Level2Spawner");
		}
	}

	IEnumerator Level1Spawner()
	{
		yield return new WaitForSeconds(1.5f);

		if(!chimpControlScript.superMode)
		{
			i = Random.Range(0 , PF_BananaCombos.Length);
			bananaObj = GameObject.FindGameObjectWithTag("BananaCombo");

			if(bananaObj == null && i != 0)
			{
				bananaObj = Instantiate(PF_BananaCombos[i] , new Vector3(15f , 0f , 0f) , transform.rotation) as GameObject;
			}
		}

		if(chimpControlScript.superMode)
		{
			bananaObj = GameObject.FindGameObjectWithTag("SMBs");

			if(bananaObj == null)
			{
				bananaObj = Instantiate(PF_BananaCombos[0] , new Vector3(5f , 0f , 0f) , transform.rotation) as GameObject;
			}
		}

		StartCoroutine("Level1Spawner");
	}

	IEnumerator Level2Spawner()
	{
		yield return new WaitForSeconds(spawnTime);
		i = Random.Range(0 , PF_BananaAndOthers.Length);
		bananaObj = Instantiate(PF_BananaAndOthers[i] , new Vector3(Random.Range(-4f , 2f) , 3f , 0f) , transform.rotation) as GameObject;
		StartCoroutine("Level2Spawner");
	}
}
