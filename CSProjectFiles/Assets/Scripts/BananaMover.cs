using System.Collections;
using UnityEngine;

public class BananaMover : MonoBehaviour 
{
	private float startPos;
	private int defaultChildCount;
	private GameSpawner gameSpawnScript;
	private Ground groundScript;
	private Rigidbody2D bananaBody2D;

	void Start() 
	{
		bananaBody2D = GetComponent<Rigidbody2D>();
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
		
		bananaBody2D.velocity = new Vector2(-groundScript.speed , bananaBody2D.velocity.y);
			
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
