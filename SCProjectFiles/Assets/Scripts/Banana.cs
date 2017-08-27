//using GooglePlayGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Banana : MonoBehaviour 
{
	AudioSource m_bananaSound;
	bool m_clickSafeZone;
	BoxCollider2D m_bananaCollider2D;
	ChimpController m_chimpControlScript;
    GameObject m_bananaSoundObj;
    Ground m_groundScript;
    Rigidbody2D m_bananaBody2D;
	ScoreManager m_scoreManagementScript;
	SpriteRenderer m_bananaRenderer;

    [SerializeField] Sprite m_normalSprite , m_superSprite;

	//public int bananaAchievementScore;
	//public string bananaType;
	//public string achievementID;

	void Start() 
	{
        m_bananaBody2D = GetComponent<Rigidbody2D>();
        m_bananaCollider2D = GetComponent<BoxCollider2D>();
        //bananaParticle = GetComponent<ParticleSystem>();
		m_bananaRenderer = GetComponent<SpriteRenderer>();
		m_chimpControlScript = FindObjectOfType<ChimpController>();
        m_groundScript = FindObjectOfType<Ground>();
		m_scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
        //startXPos = transform.position.x;
        //startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        m_bananaBody2D.velocity = new Vector2(-m_groundScript.speed , m_bananaBody2D.velocity.y);
    }

    void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		//bananaAchievementScore = scoreManagementScript.bananaScoreValue;

		if(m_chimpControlScript != null)
		{
            if(m_chimpControlScript.m_superMode)
            {
                m_bananaRenderer.sprite = m_superSprite;
            }

            if(!m_chimpControlScript.m_superMode)
            {
                m_bananaRenderer.sprite = m_normalSprite;
            }
		}
	}

	IEnumerator SoundObjectTimer()
	{
		yield return new WaitForSeconds(0.4f);

		m_bananaSoundObj = GameObject.Find("BananaSound");

		if(m_bananaSoundObj != null) 
		{
			m_bananaSound = m_bananaSoundObj.GetComponent<AudioSource>();
		}

		StartCoroutine("SoundObjectTimer");
	}

    void OnTriggerEnter2D(Collider2D col2D)
	{
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            Destroy(gameObject);
        }

        if(col2D.gameObject.tag.Equals("CSZ"))
        {
            m_clickSafeZone = true;
        }

        if (col2D.gameObject.tag.Equals("Player"))
		{
            //Debug.Log("Banana & Player Collision");// Working

            //bananaParticle.Play();

			if(m_bananaRenderer.sprite == m_normalSprite)
			{
                //bananaAchievementScore++;
                m_scoreManagementScript.m_bananasCollected++;
                
//				if(Social.localUser.authenticated)
//				{
//					PlayGamesPlatform.Instance.IncrementAchievement(achievementID , 1 , (bool success) => //Working and enable this code for final version
//					{
//						Debug.Log("Incremental Achievement");
//					});
//				}
			}

			if(m_bananaRenderer.sprite == m_superSprite)
			{
                //bananaAchievementScore += 5;
                m_scoreManagementScript.m_bananasCollected += 5;
                
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

			if(m_bananaSound != null)
			{
				if(!m_bananaSound.isPlaying) 
				{
					m_bananaSound.Stop();
					m_bananaSound.Play();
				} 
				else
				{
					m_bananaSound.Play();
				}
			}

            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag.Equals("CSZ"))
        {
            m_clickSafeZone = false;
        }
    }
}
