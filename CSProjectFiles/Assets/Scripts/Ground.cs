using System.Collections;
using UnityEngine;

public class Ground : MonoBehaviour 
{
	private Rigidbody2D groundBody2D;

	public float speed;

	void Start()
	{
		groundBody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		groundBody2D.velocity = new Vector2(-speed , groundBody2D.velocity.y);

		if(transform.position.x <= -28.8f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
