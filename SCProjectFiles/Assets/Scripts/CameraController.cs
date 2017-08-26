using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	Rigidbody2D m_cameraBody2D;

	[SerializeField] ChimpController m_chimpControlScript;

    public float acceptableError;
	public float moveOffset;

	void Start() 
	{
		m_cameraBody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		m_cameraBody2D.velocity = new Vector2(m_chimpControlScript.m_chimpSpeed , m_cameraBody2D.velocity.y);
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if((transform.position.x - m_chimpControlScript.transform.position.x) > (moveOffset + acceptableError) || (transform.position.x - m_chimpControlScript.transform.position.x) < (moveOffset - acceptableError))
		{
			//Debug.Log("If Correction");
			transform.position = new Vector3(transform.position.x + acceptableError , transform.position.y , transform.position.z);
		}

		else if((transform.position.x - m_chimpControlScript.transform.position.x) < (moveOffset - acceptableError) || (transform.position.x - m_chimpControlScript.transform.position.x) > (moveOffset + acceptableError))
		{
			//Debug.Log("Else If Correction");
			transform.position = new Vector3(transform.position.x - acceptableError , transform.position.y , transform.position.z);
		}
	}
}
