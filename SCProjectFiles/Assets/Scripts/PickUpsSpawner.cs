using System.Collections;
using UnityEngine;

public class PickUpsSpawner : MonoBehaviour 
{
    GameObject pickUpComboToSpawn;
    int i;

	[SerializeField] ChimpController chimpControlScript;
    [SerializeField] GameObject[] PF_PickUpCombosList;

	void Start() 
	{
		StartCoroutine("Spawning");	
	}

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(1.5f);

		//if(!chimpControlScript.superMode)
		//{
			i = Random.Range(0 , PF_PickUpCombosList.Length);
            pickUpComboToSpawn = GameObject.FindGameObjectWithTag("PickUpCombo");

			if(pickUpComboToSpawn == null)
			{
                pickUpComboToSpawn = Instantiate(PF_PickUpCombosList[i] , new Vector3(15f , 0f , 0f) , transform.rotation) as GameObject;
			}
		//}

		//if(chimpControlScript.superMode)
		//{
  //          pickUpComboToSpawn = GameObject.FindGameObjectWithTag("SMBs");

		//	if(pickUpComboToSpawn == null)
		//	{
  //              pickUpComboToSpawn = Instantiate(PF_PickUpCombosList[0] , new Vector3(5f , 0f , 0f) , transform.rotation) as GameObject;
		//	}
		//}

		StartCoroutine("Spawning");
	}
}
