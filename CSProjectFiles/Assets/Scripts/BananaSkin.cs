using System.Collections;
using UnityEngine;

public class BananaSkin : MonoBehaviour 
{
	private BoxCollider2D skinCollider2D;
	private ChimpController chimpControlScript;
	private Ground groundScript;
	private SpriteRenderer skinRenderer;

	void Start() 
	{
		chimpControlScript = FindObjectOfType<ChimpController>();
		groundScript = FindObjectOfType<Ground>();
		skinCollider2D = GetComponent<BoxCollider2D>();
		skinRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(chimpControlScript.chimpSlip)
		{
			skinCollider2D.enabled = false;
			skinRenderer.enabled = false;
		}

		if(!chimpControlScript.chimpSlip)
		{
			skinCollider2D.enabled = true;
			skinRenderer.enabled = true;
		}
	}
			
	void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Player"))
		{
			skinCollider2D.enabled = false;
			skinRenderer.enabled = false;
		}
	}
}
