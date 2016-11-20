using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	private Rigidbody2D cameraBody2D;

	public ChimpController chimpControlScript;
	public float acceptableError;
	public float moveOffset;

	void Start() 
	{
		cameraBody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		cameraBody2D.velocity = new Vector2(chimpControlScript.chimpSpeed , cameraBody2D.velocity.y);
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if((transform.position.x - chimpControlScript.transform.position.x) > (moveOffset + acceptableError) || (transform.position.x - chimpControlScript.transform.position.x) < (moveOffset - acceptableError))
		{
			//Debug.Log("If Correction");
			transform.position = new Vector3(transform.position.x + acceptableError , transform.position.y , transform.position.z);
		}

		else if((transform.position.x - chimpControlScript.transform.position.x) < (moveOffset - acceptableError) || (transform.position.x - chimpControlScript.transform.position.x) > (moveOffset + acceptableError))
		{
			//Debug.Log("Else If Correction");
			transform.position = new Vector3(transform.position.x - acceptableError , transform.position.y , transform.position.z);
		}
	}
}
