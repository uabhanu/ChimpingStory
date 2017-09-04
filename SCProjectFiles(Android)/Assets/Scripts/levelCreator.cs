using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelCreator : MonoBehaviour
{
    bool enemyAdded = false , playerDead = false;
    const float tileWidth = 1.25f;
    float blankCounter = 0 , middleCounter = 0 , outofbounceX , outOfBounceY , startUpPosY , startTime;
    GameObject bgLayer , collectedTiles , gameLayer , m_chimp , tmpTile;
	int heightLevel = 0;
	string lastTile = "right";

    public float gameSpeed = 6.0f;
    public GameObject tilePos;

	void Awake()
    {
		Application.targetFrameRate = 60;
	}

	void Start() 
	{
		gameLayer = GameObject.Find("GameLayer");
		bgLayer = GameObject.Find("BackgroundLayer");
		collectedTiles = GameObject.Find("Tiles");

		for(int i = 0; i < 30; i++)
        {
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left" , typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.Find("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;

			GameObject tmpG2 = Instantiate(Resources.Load("ground_middle" , typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;

			GameObject tmpG3 = Instantiate(Resources.Load("ground_right" , typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.Find("gRight").transform;
			tmpG3.transform.position = Vector2.zero;

			GameObject tmpG4 = Instantiate(Resources.Load("blank" , typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.Find("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}

		for(int i = 0; i < 10; i++)
        {
			GameObject tmpG5 = Instantiate(Resources.Load("enemy" , typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = collectedTiles.transform.Find("Killers").transform;
			tmpG5.transform.position = Vector2.zero;
		}

		collectedTiles.transform.position = new Vector2 (-60.0f , -20.0f);

		tilePos = GameObject.Find("StartTilePosition");
		startUpPosY = tilePos.transform.position.y;
		outofbounceX = tilePos.transform.position.x - 5.0f;
		outOfBounceY = startUpPosY - 3.0f;
		m_chimp = GameObject.FindGameObjectWithTag("Player");
		FillScene();
		startTime = Time.time;
	}

	void FixedUpdate() 
	{
		if(startTime - Time.time % 5 == 0)
        {
			gameSpeed += 0.5f;

		}

		gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime , 0);
		bgLayer.transform.position = new Vector2 (bgLayer.transform.position.x - gameSpeed/4 * Time.deltaTime, 0);

		foreach(Transform child in gameLayer.transform)
        {
			if(child.position.x < outofbounceX)
            {
				switch(child.gameObject.name)
                {
					
				    case "ground_left(Clone)":
					    child.gameObject.transform.position = collectedTiles.transform.Find("gLeft").transform.position;
					    child.gameObject.transform.parent = collectedTiles.transform.Find("gLeft").transform;
				    break;

				    case "ground_middle(Clone)":
					    child.gameObject.transform.position = collectedTiles.transform.Find("gMiddle").transform.position;
					    child.gameObject.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
				    break;

				    case "ground_right(Clone)":
					    child.gameObject.transform.position = collectedTiles.transform.Find("gRight").transform.position;
					    child.gameObject.transform.parent = collectedTiles.transform.Find("gRight").transform;
				    break;

				    case "blank(Clone)":
					    child.gameObject.transform.position = collectedTiles.transform.Find("gBlank").transform.position;
					    child.gameObject.transform.parent = collectedTiles.transform.Find("gBlank").transform;
				    break;

				    case "enemy(Clone)":
					    child.gameObject.transform.position = collectedTiles.transform.Find("Killers").transform.position;
					    child.gameObject.transform.parent = collectedTiles.transform.Find("Killers").transform;
				    break;

				    case "Reward":
					    GameObject.Find("Reward").GetComponent<Pickup>().m_inPlay = false;
				    break;

				    default:
					    Destroy(child.gameObject);
				    break;
				}
			}
		}

		if(gameLayer.transform.childCount < 25)
        {
            SpawnTile();
        }

		if(m_chimp.transform.position.y < outOfBounceY)
        {
            KillPlayer();
        }
	}

	void KillPlayer()
    {
		if(playerDead)
        {
            return;
        } 

        playerDead = true;
		//GetComponent<ScoreHandler> ().sendToHighScore ();
		GetComponent<PlaySound>().SoundToPlay("restart");
		Invoke("ReloadScene" , 1);
	}

	void ReloadScene()
    {
		SceneManager.LoadScene("LandRunner");
	}

	void FillScene()
	{
		for(int i = 0; i < 15; i++)
		{
			SetTile("middle");
		}

		SetTile("right");
	}

	void SetTile(string type)
	{
		switch(type)
        {
		    case "left":
			    tmpTile = collectedTiles.transform.Find("gLeft").transform.GetChild(0).gameObject;
		    break;

		    case "middle":
			    tmpTile = collectedTiles.transform.Find("gMiddle").transform.GetChild(0).gameObject;
		    break;

		    case "right":
			    tmpTile = collectedTiles.transform.Find("gRight").transform.GetChild(0).gameObject;
		    break;

		    case "blank":
			    tmpTile = collectedTiles.transform.Find("gBlank").transform.GetChild(0).gameObject;
		    break;
		}

		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x + (tileWidth) , startUpPosY + (heightLevel * tileWidth) , 0);
		tilePos = tmpTile;
		lastTile = type;
	}

	void SpawnTile()
    {
		if(blankCounter > 0)
        {
			SetTile("blank");
			blankCounter--;
			return;
		}

		if(middleCounter > 0)
        {
			RandomizeEnemy();
			SetTile("middle");
			middleCounter--;
			return;
		}

		enemyAdded = false;

		if(lastTile == "blank")
        {
			ChangeHeight();
			SetTile("left");
			middleCounter = Random.Range(1 , 8);

		}
        
        else if(lastTile =="right")
        {
			//GetComponent<ScoreHandler> ().Points++;
			blankCounter = Random.Range(1 , 3);

		}
        
        else if(lastTile == "middle")
        {
			SetTile("right");
		}
	}

	void ChangeHeight()
	{
		int newHeightLevel = (int)Random.Range(0 , 4);

		if(newHeightLevel<heightLevel)
        {
            heightLevel--;
        }
		
		else if(newHeightLevel>heightLevel)
        {
            heightLevel++;
        }
	}

	void RandomizeEnemy()
    {
		if(enemyAdded)
        {
			return;
		}

		if((int)Random.Range (0 , 4) == 1)
        {
			GameObject newEnemy = collectedTiles.transform.Find("Killers").transform.GetChild(0).gameObject;
			newEnemy.transform.parent = gameLayer.transform;
			newEnemy.transform.position = new Vector2(tilePos.transform.position.x + tileWidth , startUpPosY + (heightLevel * tileWidth + (tileWidth * 2)));
			enemyAdded=true;
		}
	}
}
