//using GooglePlayGames;
using System.Collections;
using UnityEngine;

public class Pillar : MonoBehaviour 
{
	private Ground groundScript;

	public Rigidbody2D enemyBody2D;

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		groundScript = FindObjectOfType<Ground>();
		enemyBody2D.velocity = new Vector2(-groundScript.speed , enemyBody2D.velocity.y);

		if(transform.position.x < -17f)
		{
			Destroy(gameObject);
		}
	}
}
