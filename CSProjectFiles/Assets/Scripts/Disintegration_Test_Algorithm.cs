using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//Only tested with sprites with size 100 x 100 and 200 x 200.
public class Disintegration_Test_Algorithm : MonoBehaviour {

	//--Public
	public int pic_width = 100;  	//Picture width, this example 100x100 or 200 x 200.	
	public int subdivisions = 20;	//Better between (5 <-> 20), by default 20x20, Different values can cause errors.(For android I recommend (5<->10)).

	//--Private
	private Texture2D source;		//Sprite source used for subidivision process.
	private int aux = 0;
	private float unit_size = 0;  	//Aux var
	private int pixel_size;       	//Relative pixel size
	private float auto_place = 0;
	private float aux_euler = 0;

	// Use this for initialization
	void Start() 
	{
		source = GetComponent<SpriteRenderer>().sprite.texture;
		Init();
	}

	void Init()
	{
		aux_euler = this.gameObject.transform.eulerAngles.z;
		this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);//Reset rotation before subdivision
		pixel_size = pic_width / subdivisions;
		unit_size = GetComponent<Renderer>().bounds.size.x/subdivisions;
		auto_place = (GetComponent<Renderer>().bounds.size.x/2) - unit_size/2;
		Sub_Division();
		GetComponent<Renderer>().enabled=false;
		this.gameObject.transform.rotation = Quaternion.Euler(0, 0, aux_euler);
	}

	void Sub_Division()
	{
		for(int i = 0; i < pic_width; i++)
		{
			for(int j = 0; j < pic_width ; j++)
			{
				if((i%pixel_size == 0)&&(j%pixel_size==0))
				{
					Create_Sprite(i,j);
					aux++;
				}
			}
		} 
	}
	
	void Create_Sprite(int i, int j)
	{
		Sprite newSprite = Sprite.Create(source, new Rect(i, j, pic_width/subdivisions, (pic_width/pic_width)*pixel_size), new Vector2(0.5f, 0.5f));
		GameObject n = new GameObject();//Subdivision gameobject
		n.transform.localScale*=this.gameObject.transform.localScale.x;
		SpriteRenderer sr = n.AddComponent<SpriteRenderer>();
		sr.sortingLayerName = "Enemy";
		/*if(use_diffuse == true){
			sr.material = diffuse_;
		}*/
		Random_Direction_Test rd = n.AddComponent<Random_Direction_Test>();
		n.name = ""+aux;
		sr.sprite = newSprite;

		if(n.name=="0")
		{
			n.transform.position = new Vector3(0 + this.gameObject.transform.position.x - auto_place, (j*unit_size/pixel_size) + this.gameObject.transform.position.y - auto_place, 0);
		}
		else
		{
			n.transform.position = new Vector3((unit_size*i/pixel_size) + this.gameObject.transform.position.x - auto_place, (j*unit_size/pixel_size) + this.gameObject.transform.position.y - auto_place , 0);
		}

		n.transform.parent = this.gameObject.transform;
	}
}
