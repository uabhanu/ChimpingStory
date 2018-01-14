using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingChimp : MonoBehaviour 
{
	public static float m_moveAmount = 5.5f;

	void Start() 
	{
		
	}

	public void Move(float amount)
	{
		float xPos = Mathf.Clamp(transform.position.x + amount , -8.0f , 8.22f);
		float yPos = transform.position.y;
		float zPos = transform.position.z;
		transform.position = new Vector3(xPos , yPos , zPos);
	}
}
