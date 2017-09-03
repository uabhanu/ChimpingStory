﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{	
	void OnTriggerEnter2D(Collider2D tri2D)
    {
		if (tri2D.gameObject.tag == "Player")
        {
			GameObject tmpPlayer = GameObject.FindGameObjectWithTag("Player");
			tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.right*2000);
			tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.up*2000);
			tmpPlayer.GetComponent<Collider2D>().enabled =false;

			GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("die");

		}
	}
}
