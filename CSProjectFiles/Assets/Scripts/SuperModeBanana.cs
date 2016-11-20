using System.Collections;
using UnityEngine;

public class SuperModeBanana : MonoBehaviour 
{
	private AudioSource superModeBananaSound;
	private BoxCollider2D bananaCollider2D;
	private ChimpController chimpControlScript;
	private GameObject superModeBananaSoundObj;
	private ScoreManager scoreManagementScript;
	private SpriteRenderer bananaRenderer;

	void Start() 
	{
		bananaCollider2D = GetComponent<BoxCollider2D>();
		bananaRenderer = GetComponent<SpriteRenderer>();
		chimpControlScript = GameObject.Find("Chimp").GetComponent<ChimpController>();
		scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
	}
		
	IEnumerator SoundObjectTimer()
	{
		yield return new WaitForSeconds(0.4f);

		if(chimpControlScript.superMode)
		{
			bananaCollider2D.enabled = true;
			bananaRenderer.enabled = true;

			superModeBananaSoundObj = GameObject.Find("SuperModeBananaSound");

			if(superModeBananaSoundObj != null) 
			{
				superModeBananaSound = superModeBananaSoundObj.GetComponent<AudioSource>();
			}
		}

		StartCoroutine("SoundObjectTimer");
	}

	void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Player"))
		{
			bananaCollider2D.enabled = false;
			bananaRenderer.enabled = false;
			scoreManagementScript.bananaScoreValue++;
			PlayerPrefs.SetInt("BananaScore" , scoreManagementScript.bananaScoreValue);

			if(superModeBananaSound != null)
			{
				if(!superModeBananaSound.isPlaying) 
				{
					superModeBananaSound.Stop ();
					superModeBananaSound.Play ();
				} 
				else
				{
					superModeBananaSound.Play();
				}
			}
		}

		if(col2D.gameObject.tag.Equals("Cleaner"))
		{
			bananaCollider2D.enabled = false;
			bananaRenderer.enabled = false;
		}
	}
}
