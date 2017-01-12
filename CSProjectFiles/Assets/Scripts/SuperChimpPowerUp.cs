using System.Collections;
//using SVGImporter;
using UnityEngine;

public class SuperChimpPowerUp : MonoBehaviour 
{
	BoxCollider2D superchimpCollider2D;
	ChimpController chimpControlScript;
    float maxY;
	float minY;
	float startPos;
    int direction = 1;
	Rigidbody2D nutBody2D;
	ScoreManager scoreManagementScript;
	SpriteRenderer superchimpRenderer;

	public Ground groundScript;

	void Start() 
	{
        nutBody2D = GetComponent<Rigidbody2D>();
		chimpControlScript = GameObject.Find("Chimp").GetComponent<ChimpController>();
		maxY = this.transform.position.y + 0.5f;
		minY = maxY - 1.0f;
		scoreManagementScript = FindObjectOfType<ScoreManager>();
		StartCoroutine("SoundObjectTimer");
		startPos = transform.position.x;
		superchimpCollider2D = GetComponent<BoxCollider2D>();
		superchimpRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		nutBody2D.velocity = new Vector2(-groundScript.speed , nutBody2D.velocity.y);

		if(transform.position.x < -8.5f)
		{
			transform.position = new Vector3(startPos , 2f , 0f);
		}
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y + (direction * 0.05f));

		if(this.transform.position.y > maxY) 
		{
			direction = -1;
		}
			
		if(this.transform.position.y < minY) 
		{
			direction = 1;
		}

		if(chimpControlScript.superMode)
		{
			superchimpCollider2D.enabled = false;
			superchimpRenderer.enabled = false;
		}

		if(!chimpControlScript.superMode)
		{
			superchimpCollider2D.enabled = true; //IAP will make monkeynutTaken false again
			superchimpRenderer.enabled = true;
		}
	}
		
	IEnumerator SoundObjectTimer()
	{
		yield return new WaitForSeconds(0.4f);
		StartCoroutine("SoundObjectTimer");
	}
		
	void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Player"))
		{
			//superchimpCollider2D.enabled = false;
			//superchimpRenderer.enabled = false;
            transform.position = new Vector3(startPos , 2f , 0f);
            scoreManagementScript.superChimpScoreValue++;
		}
	}
}
