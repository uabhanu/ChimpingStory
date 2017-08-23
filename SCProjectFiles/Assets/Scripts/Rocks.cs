using System.Collections;
using UnityEngine;

public class Rocks : MonoBehaviour 
{
	private Ground groundScript;
	private Rigidbody2D rocksBody2D;

	void Start() 
	{
		groundScript = FindObjectOfType<Ground>();
		rocksBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		rocksBody2D.velocity = new Vector2(-groundScript.speed , rocksBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
