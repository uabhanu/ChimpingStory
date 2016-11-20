using System.Collections;
using UnityEngine;

public class GameSpawner : MonoBehaviour 
{
	//private int playerLevel = 1;

	public int level; //Only for testing
	public GameObject gameObj;
	public GameObject[] PF_GameAreas;

	void Start() 
	{
		gameObj = GameObject.FindGameObjectWithTag("GameArea");

		if(gameObj == null)
		{
			gameObj = Instantiate(PF_GameAreas[level - 1] , transform.position , transform.rotation) as GameObject;
		}
	}
}
