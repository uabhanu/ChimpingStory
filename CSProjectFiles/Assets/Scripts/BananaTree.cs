using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaTree : MonoBehaviour
{
    [SerializeField] float randomValue;

    float startXPos , startYPos;
    Ground groundScript;
    Rigidbody2D treeBody2D;
  
	void Start()
    {
        groundScript = FindObjectOfType<Ground>();
        startXPos = transform.position.x;
        startYPos = transform.position.y;
        treeBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        treeBody2D.velocity = new Vector2(-groundScript.speed , treeBody2D.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            transform.position = new Vector2(Random.Range(startXPos - randomValue , startXPos + randomValue) , startYPos);
        }
    }
}
