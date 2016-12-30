using System.Collections;
using UnityEngine;

public class BananaSpawner : MonoBehaviour 
{
	int i;

	public ChimpController chimpControlScript;
	public GameObject bananaComboToSpawn;
	public GameObject[] PF_BananaCombosList;

	void Start() 
	{
		StartCoroutine("Spawning");	
	}

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(1.5f);

		if(!chimpControlScript.superMode)
		{
			i = Random.Range(0 , PF_BananaCombosList.Length);
            bananaComboToSpawn = GameObject.FindGameObjectWithTag("BananaCombo");

			if(bananaComboToSpawn == null && i != 0)
			{
                bananaComboToSpawn = Instantiate(PF_BananaCombosList[i] , new Vector3(15f , 0f , 0f) , transform.rotation) as GameObject;
			}
		}

		if(chimpControlScript.superMode)
		{
            bananaComboToSpawn = GameObject.FindGameObjectWithTag("SMBs");

			if(bananaComboToSpawn == null)
			{
                bananaComboToSpawn = Instantiate(PF_BananaCombosList[0] , new Vector3(5f , 0f , 0f) , transform.rotation) as GameObject;
			}
		}

		StartCoroutine("Spawning");
	}
}
