using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMovement : MonoBehaviour
{
    Rigidbody2D bananaBody2D;

    void Start()
    {
        bananaBody2D = GetComponent<Rigidbody2D>();
    }
	
	void Update()
    {
        bananaBody2D.velocity = new Vector2(bananaBody2D.velocity.x , -1f);
    }
}
