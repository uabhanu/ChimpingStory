using System.Collections;
using UnityEngine;

public class BananaSkin : MonoBehaviour 
{
	BoxCollider2D skinCollider2D;
	ChimpController chimpControlScript;

    [SerializeField] float randomValue;

    float startXPos , startYPos;
    Ground groundScript;
    Rigidbody2D skinBody2D;
	SpriteRenderer skinRenderer;

	void Start() 
	{
		chimpControlScript = FindObjectOfType<ChimpController>();
		groundScript = FindObjectOfType<Ground>();
        skinBody2D = GetComponent<Rigidbody2D>();
		skinCollider2D = GetComponent<BoxCollider2D>();
		skinRenderer = GetComponent<SpriteRenderer>();
        startXPos = transform.position.x;
        startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        skinBody2D.velocity = new Vector2(-groundScript.speed , skinBody2D.velocity.y);
    }

    void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(chimpControlScript.chimpSlip || chimpControlScript.superMode)
		{
			skinCollider2D.enabled = false;
			skinRenderer.enabled = false;
		}

		if(!chimpControlScript.chimpSlip && !chimpControlScript.superMode)
		{
			skinCollider2D.enabled = true;
			skinRenderer.enabled = true;
		}
	}
			
	void OnTriggerEnter2D(Collider2D col2D)
	{
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(Random.Range(startXPos - randomValue , startXPos + randomValue) , startYPos);
        }

		if(col2D.gameObject.tag.Equals("Player"))
		{
			skinCollider2D.enabled = false;
			skinRenderer.enabled = false;
            transform.position = new Vector2(Random.Range(startXPos - randomValue , startXPos + randomValue) , startYPos);
        }
	}
}
