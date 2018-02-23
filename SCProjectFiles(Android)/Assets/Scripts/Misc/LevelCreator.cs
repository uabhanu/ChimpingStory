using UnityEngine;


public class LevelCreator : MonoBehaviour
{
    bool m_miscObjAdded;
    LandChimp m_landChimp;
    const float m_tileWidth = 1.25f;
    float m_blankCounter = 0 , m_outofbounceX , m_startUpPosY;
    GameObject m_collectedTiles , m_gameLayer ,  m_tmpTile;
	int m_heightLevel = 0;
	string m_lastTile = "PF_GroundRight";

    public float m_gameSpeed;
    public GameObject m_tilePos;
    public static float m_middleCounter = 0;

	void Start() 
	{
        m_collectedTiles = GameObject.Find("Tiles");
        m_gameLayer = GameObject.Find("GameLayer");
        m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();

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

        for(int i = 0; i < 6; i++)
        {
            GameObject banana = Instantiate(Resources.Load("PF_Banana", typeof(GameObject))) as GameObject;
            banana.transform.parent = m_collectedTiles.transform.Find("Banana").transform;
            banana.transform.position = Vector2.zero;

            GameObject bananaSkin = Instantiate(Resources.Load("PF_BananaSkin", typeof(GameObject))) as GameObject;
            bananaSkin.transform.parent = m_collectedTiles.transform.Find("BananaSkin").transform;
            bananaSkin.transform.position = Vector2.zero;

            GameObject coin = Instantiate(Resources.Load("PF_Coin", typeof(GameObject))) as GameObject;
            coin.transform.parent = m_collectedTiles.transform.Find("Coin").transform;
            coin.transform.position = Vector2.zero;

            GameObject hurdle = Instantiate(Resources.Load("PF_Hurdle", typeof(GameObject))) as GameObject;
            hurdle.transform.parent = m_collectedTiles.transform.Find("Hurdle").transform;
            hurdle.transform.position = Vector2.zero;

            GameObject portal = Instantiate(Resources.Load("PF_Portal", typeof(GameObject))) as GameObject;
            portal.transform.parent = m_collectedTiles.transform.Find("Portal").transform;
            portal.transform.position = Vector2.zero;

            GameObject super = Instantiate(Resources.Load("PF_Super", typeof(GameObject))) as GameObject;
            super.transform.parent = m_collectedTiles.transform.Find("Super").transform;
            super.transform.position = Vector2.zero;
        }

        m_collectedTiles.transform.position = new Vector2 (-60.0f , -20.0f);
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		m_outofbounceX = m_tilePos.transform.position.x - 5.0f;
		FillScene();
        Invoke("GameSpeed" , 5.8f);
    }

	void FixedUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        LevelGeneration();
    }

    void ChangeHeight()
    {
        int newHeightLevel = Random.Range(0 , 2);

        if(newHeightLevel < m_heightLevel)
        {
            m_heightLevel--;
        }

        else if(newHeightLevel > m_heightLevel)
        {
            m_heightLevel++;
        }
    }

    void FillScene()
	{
		for(int i = 0; i < 15; i++)
		{
			SetTile("PF_GroundMiddle");
		}

		SetTile("PF_GroundRight");
	}

    void GameSpeed()
    {
        if(m_gameSpeed < 12f && !m_landChimp.m_isSuper)
        {
            m_gameSpeed += 0.5f;
        }

        if(m_gameSpeed > 12f)
        {
            m_gameSpeed = 12f;
        }
        
        Invoke("GameSpeed" , 5.8f);
    }

    void LevelGeneration()
    {
        
        m_gameLayer.transform.position = new Vector2(m_gameLayer.transform.position.x - m_gameSpeed * Time.deltaTime , 0);

        for(int i = 0; i < m_gameLayer.transform.childCount; i++)
        {
            Transform child  = m_gameLayer.transform.GetChild(i);

            if(child.position.x < m_outofbounceX)
            {
                switch(child.gameObject.name)
                {
                    case "PF_Banana(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("Banana").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("Banana").transform;
                    break;

                    case "PF_BananaSkin(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("BananaSkin").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("BananaSkin").transform;
                    break;

                    case "PF_Coin(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("Coin").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("Coin").transform;
                    break;

                    case "PF_Blank(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("gBlank").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("gBlank").transform;
                    break;

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

                    case "PF_Hurdle(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("Hurdle").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("Hurdle").transform;
                    break;

                    case "PF_Portal(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("Portal").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("Portal").transform;
                    break;

                    case "PF_Super(Clone)":
                        child.gameObject.transform.position = m_collectedTiles.transform.Find("Super").transform.position;
                        child.gameObject.transform.parent = m_collectedTiles.transform.Find("Super").transform;
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
    }

    void RandomizeMiscObject()
    {
        if(m_miscObjAdded)
        {
            return;
        }

        if(Random.Range(0 , 2) == 1)
        {
            GameObject banana = m_collectedTiles.transform.Find("Banana").transform.GetChild(0).gameObject;
            banana.transform.parent = m_gameLayer.transform;
            banana.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f, m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 4.3f)));
            m_miscObjAdded = true;
        }

        else if(Random.Range(0 , 2) == 1)
        {
            GameObject bananaSkin = m_collectedTiles.transform.Find("BananaSkin").transform.GetChild(0).gameObject;
            bananaSkin.transform.parent = m_gameLayer.transform;
            bananaSkin.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 2f)));
            m_miscObjAdded = true;
        }

        else if(Random.Range(0 , 2) == 1)
        {
            GameObject bananaSkin = m_collectedTiles.transform.Find("Coin").transform.GetChild(0).gameObject;
            bananaSkin.transform.parent = m_gameLayer.transform;
            bananaSkin.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 4.3f)));
            m_miscObjAdded = true;
        }

        else if(Random.Range(0 , 2) == 1 && m_middleCounter > 2)
        {

            GameObject hurdle = m_collectedTiles.transform.Find("Hurdle").transform.GetChild(0).gameObject;
            hurdle.transform.parent = m_gameLayer.transform;
            hurdle.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 2.3f)));
            m_miscObjAdded = true;
        }

        else if(Random.Range(0 , 2) == 1)
        {

            GameObject portal = m_collectedTiles.transform.Find("Portal").transform.GetChild(0).gameObject;
            portal.transform.parent = m_gameLayer.transform;
            portal.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 4.3f)));
            m_miscObjAdded = true;
        }

        else if(Random.Range(0 , 2) == 1 && ScoreManager.m_supersCount > 0)
        {

            GameObject super = m_collectedTiles.transform.Find("Super").transform.GetChild(0).gameObject;
            super.transform.parent = m_gameLayer.transform;
            super.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 4.3f)));
            m_miscObjAdded = true;
            ScoreManager.m_supersCount--;
        }
    }

    void SetTile(string type)
	{
		switch(type)
        {
            case "PF_Blank":
			    m_tmpTile = m_collectedTiles.transform.Find("gBlank").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundLeft":
			    m_tmpTile = m_collectedTiles.transform.Find("gLeft").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundMiddle":
			    m_tmpTile = m_collectedTiles.transform.Find("gMiddle").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundRight":
			    m_tmpTile = m_collectedTiles.transform.Find("gRight").transform.GetChild(0).gameObject;
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
            SetTile("PF_GroundMiddle");
            m_middleCounter--;
			return;
		}

        m_miscObjAdded = false;

		if(m_lastTile == "PF_Blank")
        {
			ChangeHeight();
			SetTile("PF_GroundLeft");
            m_middleCounter = Random.Range(1 , 8);

            if(m_middleCounter > 4)
            {
                RandomizeMiscObject();
            }
		}
        
        else if(m_lastTile == "PF_GroundRight")
        {
			m_blankCounter = Random.Range(1 , 3);
		}
        
        else if(m_lastTile == "PF_GroundMiddle")
        {
			SetTile("PF_GroundRight");
		}
	}
}
