using System.Collections;
using UnityEngine;
//This script is used to activate 'Disintegration_Test_Algorithm' and identify the collided object.
public class Activator : MonoBehaviour 
{
	private ChimpController chimpControlScript;
	private ParticleSystem enemyParticleSystem;
	private ScoreManager scoreManagementScript;
	private Vector2 rando;
	private Vector3 randomDirection;

	public GameObject objToBeDestroyedBy;

	void Start() 
	{
		chimpControlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimpController>();
		enemyParticleSystem = GetComponent<ParticleSystem>();
		scoreManagementScript = FindObjectOfType<ScoreManager>();
	}

	void AddComplement(GameObject n)
	{
		//Rigidbody2D _rb = (Rigidbody2D)n.AddComponent(typeof(Rigidbody2D));
		n.GetComponent<Rigidbody2D>().gravityScale =  1;
		BoxCollider2D _bc = (BoxCollider2D)n.AddComponent(typeof(BoxCollider2D));
		_bc.offset = Vector3.zero;
		_bc.size = new Vector2 (0.05f,0.05f);
	}

	void DestroyDelay()
	{
		Destroy (this.gameObject.transform.gameObject);
		//gameObject.transform.gameObject.SetActive(false);
	}

	void Explosion()
	{
		transform.Rotate(randomDirection);
		AddComplement(this.gameObject);
		GetComponent<Rigidbody2D>().velocity = rando;
	}

	void Init()
	{
		objToBeDestroyedBy = this.gameObject.transform.parent.GetComponent<Activator>().objToBeDestroyedBy; //collided object
		gameObject.layer = 4;
		Physics2D.IgnoreLayerCollision(4 , 4); 
		randomDirection = new Vector3(0 , 0 , Random.Range(-30f , 30f));

		if (objToBeDestroyedBy)
		{
			if(OnRight(objToBeDestroyedBy)==true)
			{
				rando = new Vector3(Random.Range(-4.5f,1f), Random.Range(-3f,3f)); 
			}else
			{
				rando = new Vector3(Random.Range(4.5f,1f), Random.Range(-3f,3f)); 
			}
		}
		else
		{
			rando = new Vector3(Random.Range(-4.5f,1f), Random.Range(-3f,3f)); 
		}

		//Invoke("Mov" , 0);
		Invoke("OnDestroy" , 3f);
		Explosion();
	}

	void OnDestroy()
	{
		GetComponent<Rigidbody2D>().isKinematic=true;
		GetComponent<SpriteRenderer>().enabled=false;//Disable actual spriterenderer
		GetComponent<Collider2D>().enabled=false;
		GetComponent<Disintegration_Test_Algorithm>().enabled = true;
		Invoke("DestroyDelay" , 1f);
		//DestroyDelay();
	}

	bool OnRight(GameObject go)
	{
		bool aux=true;
		if(this.gameObject.transform.position.x < go.transform.position.x)
		{
			aux = true;
		}
		else
		{
			aux = false;
		}
		return aux;
	}

	void OnTriggerEnter2D(Collider2D col2D) 
	{
		if(col2D.gameObject.name=="Bhanu")
		{
			
		}

		else if(chimpControlScript.superMode)
		{
			enemyParticleSystem.Play();
			objToBeDestroyedBy = col2D.gameObject;
			Invoke("TrophyScore" , 0.5f);
			OnDestroy();
		}
	}

	void TrophyScore()
	{
		scoreManagementScript.trophiesScoreValue++;

		if(Social.localUser.authenticated)
		{
			Social.ReportScore(scoreManagementScript.trophiesScoreValue , scoreManagementScript.trophiesLeaderboard , (bool success) =>
			{
				Debug.Log("Send Score to Leaderboard");
			});
		}
	}
}
