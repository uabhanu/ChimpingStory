//using GooglePlayGames;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour 
{
	private BoxCollider2D ground01Collider2D;
	private BoxCollider2D ground02Collider2D;
	private BoxCollider2D ground03Collider2D;
	private ChimpController chimpControlScript;
	//private int holeAchievementScore;

	//public string achievementID;

	void Start() 
	{
		chimpControlScript = GameObject.Find("Chimp").GetComponent<ChimpController>();
		ground01Collider2D = GameObject.Find("Ground01").GetComponent<BoxCollider2D>();
		ground02Collider2D = GameObject.Find("Ground02").GetComponent<BoxCollider2D>();
		ground03Collider2D = GameObject.Find("Ground03").GetComponent<BoxCollider2D>();
	}
		
	void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Player"))
		{
			Debug.Log("Player in the Hole"); //Working

//			holeAchievementScore++; //All working fit and fine and beware that the achievement progress won't be updated now as the code is commented

//			if(Social.localUser.authenticated)
//			{
//				PlayGamesPlatform.Instance.IncrementAchievement(achievementID , 1 , (bool success) => //Working and enable this code for final version
//				{
//					Debug.Log("Incremental Achievement");
//				});
//
//				if(holeAchievementScore == 50)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , holeAchievementScore , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//						holeAchievementScore = 0;
//					});
//				}
//			}

			chimpControlScript.canJump = false;
			ground01Collider2D.isTrigger = true;
			ground02Collider2D.isTrigger = true;
			ground03Collider2D.isTrigger = true;
		}
	}
}
