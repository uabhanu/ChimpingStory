//using GooglePlayGames;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	Ground groundScript;

	[SerializeField] Rigidbody2D objBody2D;

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		groundScript = FindObjectOfType<Ground>();
        objBody2D.velocity = new Vector2(-groundScript.speed , objBody2D.velocity.y);

		if(transform.position.x < -17f)
		{
			Destroy(gameObject);
		}
	}
}
