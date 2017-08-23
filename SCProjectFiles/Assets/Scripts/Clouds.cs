using System.Collections;
using UnityEngine;

public class Clouds : MonoBehaviour 
{
	private Ground groundScript;
	private Rigidbody2D cloudsBody2D;

	void Start() 
	{
		cloudsBody2D = GetComponent<Rigidbody2D>();
		groundScript = FindObjectOfType<Ground>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		cloudsBody2D.velocity = new Vector2(-groundScript.speed/8f , cloudsBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
