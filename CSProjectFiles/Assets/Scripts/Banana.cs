//using GooglePlayGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Banana : MonoBehaviour 
{
	AudioSource bananaSound;
	bool clickSafeZone;
	BoxCollider2D bananaCollider2D;
	ChimpController chimpControlScript;
    float startXPos , startYPos;

    [SerializeField] float randomXValue , randomYValue;

    GameObject bananaSoundObj;

    [SerializeField] Ground groundScript;

    //ParticleSystem bananaParticle;
    Rigidbody2D bananaBody2D;
	ScoreManager scoreManagementScript;

    [SerializeField] Sprite normalSprite , superSprite;

	SpriteRenderer bananaRenderer;

	//public int bananaAchievementScore;
	public string bananaType;
	//public string achievementID;

	void Start() 
	{
        bananaBody2D = GetComponent<Rigidbody2D>();
        bananaCollider2D = GetComponent<BoxCollider2D>();
        //bananaParticle = GetComponent<ParticleSystem>();
		bananaRenderer = GetComponent<SpriteRenderer>();
		chimpControlScript = FindObjectOfType<ChimpController>();
		scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
        startXPos = transform.position.x;
        startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        bananaBody2D.velocity = new Vector2(-groundScript.speed , bananaBody2D.velocity.y);
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
            if(chimpControlScript.superMode)
            {
                bananaRenderer.sprite = superSprite;
            }

            if(!chimpControlScript.superMode)
            {
                bananaRenderer.sprite = normalSprite;
            }
			//if(chimpControlScript.superMode && bananaType == "normal")
			//{
			//	bananaCollider2D.enabled = false;
			//	bananaRenderer.enabled = false;
			//}

			//if(chimpControlScript.superMode && bananaType == "super")
			//{
			//	bananaCollider2D.enabled = true;
			//	bananaRenderer.enabled = true;
			//}

			//if(!chimpControlScript.superMode && bananaType == "normal")
			//{
			//	bananaCollider2D.enabled = true;
			//	bananaRenderer.enabled = true;
			//}

			//if(!chimpControlScript.superMode && bananaType == "super")
			//{
			//	bananaCollider2D.enabled = false;
			//	bananaRenderer.enabled = false;
			//}
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
                    bananaSound.Stop();
                    bananaSound.Play();
                }
                else
                {
                    bananaSound.Play();
                }
            }

            if(scoreManagementScript.bananasLeft > 0)
            {
                scoreManagementScript.bananasLeft--;
            }

            Destroy(gameObject);
        }

        if(!clickSafeZone)
        {
            if(bananaSound != null)
            {
                if(!bananaSound.isPlaying)
                {
                    bananaSound.Stop();
                    bananaSound.Play();
                }
                else
                {
                    bananaSound.Play();
                }
            }

            if(scoreManagementScript.bananasLeft < 500)
            {
                scoreManagementScript.bananasLeft++;
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col2D)
	{
        if (col2D.gameObject.tag.Equals("BananaMissed"))
        {
            if (scoreManagementScript.bananasLeft < 500)
            {
                scoreManagementScript.bananasLeft++;
            }

            Destroy(gameObject);
        }

        if (col2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(Random.Range(startXPos , startXPos + randomXValue) , Random.Range(startYPos , startYPos + randomYValue));
        }

        if (col2D.gameObject.tag.Equals("CSZ"))
        {
            clickSafeZone = true;
        }

        if (col2D.gameObject.tag.Equals("Player"))
		{
            //Debug.Log("Banana & Player Collision");// Working

            //bananaParticle.Play();

			if(bananaRenderer.sprite == normalSprite)
			{
                //bananaAchievementScore++;

                if(scoreManagementScript.bananasLeft > 0)
                {
                    scoreManagementScript.bananasLeft--;
                }

//				if(Social.localUser.authenticated)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , 1 , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//					});
//				}
			}

			if(bananaRenderer.sprite == superSprite)
			{
                //bananaAchievementScore += 5;

                if(scoreManagementScript.bananasLeft > 10)
                {
                    scoreManagementScript.bananasLeft -= 10;
                }

                if(scoreManagementScript.bananasLeft <= 10)
                {
                    scoreManagementScript.bananasLeft = 0;
                }
                
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
					bananaSound.Stop();
					bananaSound.Play();
				} 
				else
				{
					bananaSound.Play();
				}
			}

            transform.position = new Vector2(Random.Range(startXPos , startXPos + randomXValue) , Random.Range(startYPos , startYPos + randomYValue));
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
