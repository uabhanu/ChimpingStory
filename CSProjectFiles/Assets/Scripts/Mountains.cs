using System.Collections;
using UnityEngine;

public class Mountains : MonoBehaviour 
{
	private Ground groundScript;
	private Rigidbody2D mountainsBody2D;

	void Start() 
	{
		groundScript = FindObjectOfType<Ground>();
		mountainsBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		mountainsBody2D.velocity = new Vector2(-groundScript.speed/4f , mountainsBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
