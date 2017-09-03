using UnityEngine;
using System.Collections;

public class crateScript : MonoBehaviour {

	private float maxY;
	private float minY;
	private int direction = 1;
	
	public bool inPlay = true;
	private bool releaseCrate = false;


	private SpriteRenderer crateRender;

	// Use this for initialization
	void Start () {

		maxY = this.transform.position.y + .5f;
		minY = maxY - 1.0f;

		crateRender = this.transform.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y +(direction * 0.05f));
		if (this.transform.position.y > maxY)
						direction = -1;
		if (this.transform.position.y < minY)
						direction = 1;

		if (!inPlay && !releaseCrate)
						respawn ();

	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") {

			switch(crateRender.sprite.name){

			case "crates_0":
				GameObject.Find("Main Camera").GetComponent<levelCreator>().gameSpeed -=1.0f; // CHANGE V4 BEGINNING 8
				break;
			case "crates_1":
				GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(Vector2.up*6000);
				break;
			case "crates_2":
				GameObject.Find("Main Camera").GetComponent<scoreHandler>().Points +=10;

				break;
			}
			inPlay = false;
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 30.0f);

			GameObject.Find("Main Camera").GetComponent<playSound>().PlaySound("power");

		}


	}

	void respawn(){

		releaseCrate = true;
		Invoke ("placeCrate", (float)Random.Range (3, 10));
	}

	void placeCrate(){

		inPlay = true;
		releaseCrate = false;

		GameObject tmpTile = GameObject.Find ("Main Camera").GetComponent<levelCreator> ().tilePos;
		this.transform.position = new Vector2 (tmpTile.transform.position.x, tmpTile.transform.position.y +5.5f); 
		maxY = this.transform.position.y + .5f;
		minY = maxY - 1.0f;
	}











}
