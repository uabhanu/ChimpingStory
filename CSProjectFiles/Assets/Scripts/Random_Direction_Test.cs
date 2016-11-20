using UnityEngine;
using System.Collections;
//This script was designed to be tested with everything collider object. (not only Rebekka)
//Default: rando ((-4.5f,1f),(-3f,3f)), randomDirection (-30f,30f) and gravity = 1
public class Random_Direction_Test: MonoBehaviour 
{
	public GameObject objToBeDestroyed; //Usually the player
	//private bool go = false;
	private Vector3 randomDirection;
	private Vector2 rando;
	//private bool aux = false;
	
	public void Start() 
	{
		Init();
	}

	void Init()
	{
		objToBeDestroyed = this.gameObject.transform.parent.GetComponent<Activator>().objToBeDestroyedBy; //collided object
		gameObject.layer = 4;
		Physics2D.IgnoreLayerCollision(4, 4); 
		randomDirection = new Vector3(0, 0, Random.Range(-30f,30f));

		if(objToBeDestroyed)
		{
			if(on_right(objToBeDestroyed)==true)
			{
				rando = new Vector3(Random.Range(-4.5f,1f), Random.Range(-3f,3f)); 
			}
			else
			{
				rando = new Vector3(Random.Range(4.5f,1f), Random.Range(-3f,3f)); 
			}
		}
		else
		{
			rando = new Vector3(Random.Range(-4.5f,1f), Random.Range(-3f,3f)); 
		}

		//Invoke ("Mov",0);
		//Invoke("OnDestroy" , 3);
		Explosion();
	}

	bool on_right(GameObject go)
	{
		bool aux=true;
	
		if(this.gameObject.transform.position.x < go.transform.position.x)
		{
			aux=true;
		}
		else
		{
			aux=false;
		}
		return aux;
	}
	
//	void Mov ()
//	{
//		go=true;
//	}
	
	void Explosion()
	{
		transform.Rotate(randomDirection);
		Add_Complement(this.gameObject);
		GetComponent<Rigidbody2D>().velocity = rando;
	}
	
//	void OnDestroy()
//	{
//		//Destroy (this.gameObject.transform.parent.transform.parent.gameObject);//Destroy parent/parent
//	}
	
	void Add_Complement(GameObject n)
	{
		Rigidbody2D _rb = (Rigidbody2D)n.AddComponent(typeof(Rigidbody2D));
		n.GetComponent<Rigidbody2D>().gravityScale =  1;
		BoxCollider2D _bc = (BoxCollider2D)n.AddComponent(typeof(BoxCollider2D));
		_bc.offset = Vector3.zero;
		_bc.size = new Vector2 (0.05f,0.05f);
	}
}
