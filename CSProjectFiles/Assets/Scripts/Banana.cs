//using GooglePlayGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Banana : MonoBehaviour 
{
	private AudioSource bananaSound;
	private bool clickSafeZone;
	private BoxCollider2D bananaCollider2D;
	private ChimpController chimpControlScript;
	private GameObject bananaSoundObj;
	private ScoreManager scoreManagementScript;
	private SpriteRenderer bananaRenderer;

	//public int bananaAchievementScore;
	public string bananaType;
	//public string achievementID;

	void Start() 
	{
		bananaCollider2D = GetComponent<BoxCollider2D>();
		bananaRenderer = GetComponent<SpriteRenderer>();
		chimpControlScript = FindObjectOfType<ChimpController>();
		scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
	}

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		//bananaAchievementScore = scoreManagementScript.bananaScoreValue;

		if(chimpControlScript != null)
		{
			if(chimpControlScript.superMode && bananaType == "normal")
			{
				bananaCollider2D.enabled = false;
				bananaRenderer.enabled = false;
			}

			if(chimpControlScript.superMode && bananaType == "super")
			{
				bananaCollider2D.enabled = true;
				bananaRenderer.enabled = true;
			}

			if(!chimpControlScript.superMode && bananaType == "normal")
			{
				bananaCollider2D.enabled = true;
				bananaRenderer.enabled = true;
			}

			if(!chimpControlScript.superMode && bananaType == "super")
			{
				bananaCollider2D.enabled = false;
				bananaRenderer.enabled = false;
			}
		}
	}

	IEnumerator SoundObjectTimer()
	{
		yield return new WaitForSeconds(0.4f);

		bananaSoundObj = GameObject.Find("BananaSound");

		if(bananaSoundObj != null) 
		{
			bananaSound = bananaSoundObj.GetComponent<AudioSource>();
		}

		StartCoroutine("SoundObjectTimer");
	}

	void OnMouseDown()
	{
		if(clickSafeZone)
		{
			if(bananaSound != null)
			{
				if(!bananaSound.isPlaying) 
				{
					bananaSound.Stop ();
					bananaSound.Play ();
				} 
				else
				{
					bananaSound.Play();
				}
			}

			scoreManagementScript.bananaScoreValue++;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Player"))
		{
			//Debug.Log("Banana & Player Collision");// Working

			if(bananaType == "normal")
			{
				//bananaAchievementScore++;
				scoreManagementScript.bananaScoreValue++;

//				if(Social.localUser.authenticated)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , 1 , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//					});
//				}
			}

			if(bananaType == "super")
			{
				//bananaAchievementScore += 5;
				scoreManagementScript.bananaScoreValue += 5;

//				if(Social.localUser.authenticated)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , 5 , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//					});
//				}
			}

//			if(Social.localUser.authenticated)
//			{
//				if(bananaAchievementScore == 100)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , bananaAchievementScore , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//						bananaAchievementScore = 0;
//					});
//				}
//			}

			if(bananaSound != null)
			{
				if(!bananaSound.isPlaying) 
				{
					bananaSound.Stop ();
					bananaSound.Play ();
				} 
				else
				{
					bananaSound.Play();
				}
			}

			Destroy(gameObject);
		}

		if(col2D.gameObject.tag.Equals("Cleaner"))
		{
			if(scoreManagementScript.bananaScoreValue > 0)
			{
				scoreManagementScript.bananaScoreValue--;
			}

			Destroy(gameObject);
		}

		if(col2D.gameObject.tag.Equals("CSZ"))
		{
			clickSafeZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("CSZ"))
		{
			clickSafeZone = false;
		}
	}
}
