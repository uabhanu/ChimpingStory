//using GooglePlayGames;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour 
{
    BoxCollider2D chimpCollider2D , holeCollider2D;
	ChimpController chimpControlScript;
    SpriteRenderer holeRenderer;
	//int holeAchievementScore;

	//[SerializeField] string achievementID;

	void Start() 
	{
        chimpCollider2D = GameObject.Find("Chimp").GetComponent<BoxCollider2D>();
		chimpControlScript = GameObject.Find("Chimp").GetComponent<ChimpController>();
        holeCollider2D = GetComponent<BoxCollider2D>();
        holeRenderer = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        if(chimpControlScript.superMode)
        {
            holeCollider2D.enabled = false;
            holeRenderer.enabled = false;
        }
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


            chimpControlScript.canSlide = false;
            chimpControlScript.chimpInTheHole = true;
            chimpCollider2D.isTrigger = true;
            chimpControlScript.StartCoroutine("ChimpCollider2D");
		}
	}
}
