using System.Collections;
using UnityEngine;

public class GameSpawner : MonoBehaviour 
{
	public int playerLevel = 1;

	//public int level; //For Testing
	public GameObject gameObj;
	public GameObject[] PF_GameAreas;

	void Start() 
	{
		gameObj = GameObject.FindGameObjectWithTag("GameArea");

		if(gameObj == null)
		{
			gameObj = Instantiate(PF_GameAreas[playerLevel - 1] , transform.position , transform.rotation) as GameObject;
		}
	}
}
