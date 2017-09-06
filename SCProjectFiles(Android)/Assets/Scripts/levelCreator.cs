using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelCreator : MonoBehaviour
{
    bool m_PF_EnemyAdded = false , m_playerDead = false;
    const float m_tileWidth = 1.25f;
    float m_blankCounter = 0 , m_middleCounter = 0 , m_outofbounceX , m_outOfBounceY , m_startUpPosY , m_startTime;
    GameObject m_bgLayer , m_chimp , m_collectedTiles , m_gameLayer ,  m_tmpTile;
	int m_heightLevel = 0;
	string m_lastTile = "right";

    public float m_gameSpeed = 6.0f;
    public GameObject m_tilePos;

	void Awake()
    {
		Application.targetFrameRate = 60;
	}

	void Start() 
	{
		m_gameLayer = GameObject.Find("GameLayer");
		m_bgLayer = GameObject.Find("BackgroundLayer");
		m_collectedTiles = GameObject.Find("Tiles");

		for(int i = 0; i < 30; i++)
        {
			GameObject tmpG1 = Instantiate(Resources.Load("PF_GroundLeft" , typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = m_collectedTiles.transform.Find("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;

			GameObject tmpG2 = Instantiate(Resources.Load("PF_GroundMiddle" , typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = m_collectedTiles.transform.Find("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;

			GameObject tmpG3 = Instantiate(Resources.Load("PF_GroundRight" , typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = m_collectedTiles.transform.Find("gRight").transform;
			tmpG3.transform.position = Vector2.zero;

			GameObject tmpG4 = Instantiate(Resources.Load("PF_Blank" , typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = m_collectedTiles.transform.Find("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}

		for(int i = 0; i < 10; i++)
        {
			GameObject tmpG5 = Instantiate(Resources.Load("PF_Enemy" , typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = m_collectedTiles.transform.Find("Killers").transform;
			tmpG5.transform.position = Vector2.zero;
		}

		m_collectedTiles.transform.position = new Vector2 (-60.0f , -20.0f);

		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		m_outofbounceX = m_tilePos.transform.position.x - 5.0f;
		m_outOfBounceY = m_startUpPosY - 3.0f;
		m_chimp = GameObject.FindGameObjectWithTag("Player");
		FillScene();
		m_startTime = Time.time;
	}

	void FixedUpdate() 
	{
		if(m_startTime - Time.time % 5 == 0)
        {
			m_gameSpeed += 0.5f;

		}

		m_gameLayer.transform.position = new Vector2 (m_gameLayer.transform.position.x - m_gameSpeed * Time.deltaTime , 0);

		foreach(Transform child in m_gameLayer.transform)
        {
			if(child.position.x < m_outofbounceX)
            {
				switch(child.gameObject.name)
                {
					
				    case "PF_GroundLeft(Clone)":
					    child.gameObject.transform.position = m_collectedTiles.transform.Find("gLeft").transform.position;
					    child.gameObject.transform.parent = m_collectedTiles.transform.Find("gLeft").transform;
				    break;

				    case "PF_GroundMiddle(Clone)":
					    child.gameObject.transform.position = m_collectedTiles.transform.Find("gMiddle").transform.position;
					    child.gameObject.transform.parent = m_collectedTiles.transform.Find("gMiddle").transform;
				    break;

				    case "PF_GroundRight(Clone)":
					    child.gameObject.transform.position = m_collectedTiles.transform.Find("gRight").transform.position;
					    child.gameObject.transform.parent = m_collectedTiles.transform.Find("gRight").transform;
				    break;

				    case "PF_Blank(Clone)":
					    child.gameObject.transform.position = m_collectedTiles.transform.Find("gBlank").transform.position;
					    child.gameObject.transform.parent = m_collectedTiles.transform.Find("gBlank").transform;
				    break;

				    case "PF_Enemy(Clone)":
					    child.gameObject.transform.position = m_collectedTiles.transform.Find("Killers").transform.position;
					    child.gameObject.transform.parent = m_collectedTiles.transform.Find("Killers").transform;
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

		if(m_gameLayer.transform.childCount < 25)
        {
            SpawnTile();
        }

		if(m_chimp.transform.position.y < m_outOfBounceY)
        {
            KillPlayer();
        }
	}

	void KillPlayer()
    {
		if(m_playerDead)
        {
            return;
        } 

        m_playerDead = true;
		GetComponent<PlaySound>().SoundToPlay("FallDeath");
		Invoke("ReloadScene" , 0.5f);
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
			    m_tmpTile = m_collectedTiles.transform.Find("gLeft").transform.GetChild(0).gameObject;
		    break;

		    case "middle":
			    m_tmpTile = m_collectedTiles.transform.Find("gMiddle").transform.GetChild(0).gameObject;
		    break;

		    case "right":
			    m_tmpTile = m_collectedTiles.transform.Find("gRight").transform.GetChild(0).gameObject;
		    break;

		    case "PF_Blank":
			    m_tmpTile = m_collectedTiles.transform.Find("gBlank").transform.GetChild(0).gameObject;
		    break;
		}

		m_tmpTile.transform.parent = m_gameLayer.transform;
		m_tmpTile.transform.position = new Vector3(m_tilePos.transform.position.x + (m_tileWidth) , m_startUpPosY + (m_heightLevel * m_tileWidth) , 0);
		m_tilePos = m_tmpTile;
		m_lastTile = type;
	}

	void SpawnTile()
    {
		if(m_blankCounter > 0)
        {
			SetTile("PF_Blank");
			m_blankCounter--;
			return;
		}

		if(m_middleCounter > 0)
        {
			RandomizeEnemy();
			SetTile("middle");
			m_middleCounter--;
			return;
		}

		m_PF_EnemyAdded = false;

		if(m_lastTile == "PF_Blank")
        {
			ChangeHeight();
			SetTile("left");
			m_middleCounter = Random.Range(1 , 8);

		}
        
        else if(m_lastTile =="right")
        {
			//GetComponent<ScoreHandler> ().Points++;
			m_blankCounter = Random.Range(1 , 3);

		}
        
        else if(m_lastTile == "middle")
        {
			SetTile("right");
		}
	}

	void ChangeHeight()
	{
		int newHeightLevel = Random.Range(0 , 4);

		if(newHeightLevel < m_heightLevel)
        {
            m_heightLevel--;
        }
		
		else if(newHeightLevel > m_heightLevel)
        {
            m_heightLevel++;
        }
	}

	void RandomizeEnemy()
    {
		if(m_PF_EnemyAdded)
        {
			return;
		}

		if(Random.Range (0 , 4) == 1 && m_middleCounter > 3)
        {
			GameObject newEnemy = m_collectedTiles.transform.Find("Killers").transform.GetChild(0).gameObject;
			newEnemy.transform.parent = m_gameLayer.transform;
			newEnemy.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 2)));
			m_PF_EnemyAdded = true;
		}
	}
}
