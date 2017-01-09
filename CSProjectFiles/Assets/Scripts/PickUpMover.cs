using System.Collections;
using UnityEngine;

public class PickUpMover : MonoBehaviour 
{
	private float startPos;
	private int defaultChildCount;
	private GameSpawner gameSpawnScript;
	private Ground groundScript;
	private Rigidbody2D pickUpBody2D;

	void Start() 
	{
        pickUpBody2D = GetComponent<Rigidbody2D>();
		defaultChildCount = transform.childCount;
		gameSpawnScript = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
		groundScript = FindObjectOfType<Ground>();
		startPos = transform.position.x;
	}

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        pickUpBody2D.velocity = new Vector2(-groundScript.speed , pickUpBody2D.velocity.y);
			
		if(transform.position.x < -24.5f)
		{
			if(transform.childCount < defaultChildCount) 
			{
				Destroy(gameObject);
			} 
			else
			{
				transform.position = new Vector3(startPos , 0f , 0f);
			}
		}
	}
}
