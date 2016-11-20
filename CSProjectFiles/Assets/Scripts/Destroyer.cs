using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{
	public Transform destructionPoint;

	void Start() 
	{
		destructionPoint = GameObject.Find("DestructionPoint").transform;
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

		if(transform.position.x < destructionPoint.position.x)
		{
			gameObject.SetActive(false);
		}
	}
}
