﻿using UnityEngine;


public class LevelCreator : MonoBehaviour
{
    bool _bMiscObjAdded;
    const float _tileWidth = 1.25f;
    float _blankCounter = 0 , _outofbounceX , _startUpPosY;
    GameManager _gameManager;
    GameObject _collectedTilesObj , _gameLayerObj , _tmpTileObj;
    int _heightLevel = 0;
    LandChimp _landChimp;
    ScoreManager _scoreManager;  // This line & variable _scoreManager is for testing purposes only
	string _lastTile = "PF_GroundRight";

    [SerializeField] float _minMiddleCounterValue , _playerLevelIncreaseRate;

    public static float m_gameSpeed , m_middleCounter = 0;
    public static GameObject m_bananaObj , m_bananaSkinObj , m_hurdleObj , m_polaroidObj , m_portalObj , m_superObj;

    public GameObject m_tilePosObj;


    void Reset()
    {
        _minMiddleCounterValue = 3.05f;
        _playerLevelIncreaseRate = 7.05f;
        m_tilePosObj = GameObject.Find("StartTilePosition");
    }

	void Start() 
	{
        _collectedTilesObj = GameObject.Find("Tiles");
        _gameLayerObj = GameObject.Find("GameLayer");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_gameSpeed = 5f;
        Invoke("PlayerLevelIncrease" , _playerLevelIncreaseRate + ScoreManager.m_playerLevel);
        _landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();  // This line & variable _scoreManager is for testing purposes only


		for(int i = 0; i < 30; i++)
        {
			GameObject tmpG1 = Instantiate(Resources.Load("PF_GroundLeft" , typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = _collectedTilesObj.transform.Find("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;

			GameObject tmpG2 = Instantiate(Resources.Load("PF_GroundMiddle" , typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = _collectedTilesObj.transform.Find("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;

			GameObject tmpG3 = Instantiate(Resources.Load("PF_GroundRight" , typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = _collectedTilesObj.transform.Find("gRight").transform;
			tmpG3.transform.position = Vector2.zero;

			GameObject tmpG4 = Instantiate(Resources.Load("PF_Blank" , typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = _collectedTilesObj.transform.Find("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}

        for(int i = 0; i < 6; i++)
        {
            //m_bananaObj = GameObject.Find("PF_Banana(Clone)");

            //if(m_bananaObj == null)
            //{
                GameObject banana = Instantiate(Resources.Load("PF_Banana", typeof(GameObject))) as GameObject;
                banana.transform.parent = _collectedTilesObj.transform.Find("Banana").transform;
                banana.transform.position = Vector2.zero;
                //m_bananaObj = banana;
            //}

            //m_bananaSkinObj = GameObject.Find("PF_BananaSkin(Clone)");

            //if(m_bananaSkinObj == null)
            //{
                GameObject bananaSkin = Instantiate(Resources.Load("PF_BananaSkin", typeof(GameObject))) as GameObject;
                bananaSkin.transform.parent = _collectedTilesObj.transform.Find("BananaSkin").transform;
                bananaSkin.transform.position = Vector2.zero;
                //m_bananaSkinObj = bananaSkin;
            //}
            
            //m_hurdleObj = GameObject.Find("PF_Hurdle(Clone)");
            
            //if(m_hurdleObj == null)
            //{
                GameObject hurdle = Instantiate(Resources.Load("PF_Hurdle", typeof(GameObject))) as GameObject;
                hurdle.transform.parent = _collectedTilesObj.transform.Find("Hurdle").transform;
                hurdle.transform.position = Vector2.zero;
                //m_hurdleObj = hurdle;
            //}
            
            //m_polaroidObj = GameObject.Find("PF_LandLevelPolaroid(Clone)");

            //if(m_polaroidObj == null)
            //{
                GameObject polaroid = Instantiate(Resources.Load("PF_LandLevelPolaroid", typeof(GameObject))) as GameObject;
                polaroid.transform.parent = _collectedTilesObj.transform.Find("Polaroid").transform;
                polaroid.transform.position = Vector2.zero;
                //m_polaroidObj = polaroid;
            //}

            //m_portalObj = GameObject.Find("PF_Portal(Clone)");

            //if(m_portalObj == null)
            //{
                GameObject portal = Instantiate(Resources.Load("PF_Portal", typeof(GameObject))) as GameObject;
                portal.transform.parent = _collectedTilesObj.transform.Find("Portal").transform;
                portal.transform.position = Vector2.zero;
                //m_portalObj = portal;
            //}

            //m_superObj = GameObject.Find("PF_Super(Clone)");

            //if(m_superObj == null)
            //{
                GameObject super = Instantiate(Resources.Load("PF_Super", typeof(GameObject))) as GameObject;
                super.transform.parent = _collectedTilesObj.transform.Find("Super").transform;
                super.transform.position = Vector2.zero;
                //m_superObj = super;
            //}
        }

        _collectedTilesObj.transform.position = new Vector2 (-60.0f , -20.0f);
		m_tilePosObj = GameObject.Find("StartTilePosition");
		_startUpPosY = m_tilePosObj.transform.position.y;
		_outofbounceX = m_tilePosObj.transform.position.x - 5.0f;
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
        if(ScoreManager.m_playerLevel >= 6)
        {
            int newHeightLevel = Random.Range(0 , 2);

            if(newHeightLevel < _heightLevel)
            {
                _heightLevel--;
            }

            else if(newHeightLevel > _heightLevel)
            {
                _heightLevel++;
            }
        }
    }

    void FillScene()
	{
		for(int i = 0; i < 15; i++)
		{
			SetTile("PF_GroundMiddle");
		}
	}

    void GameSpeed()
    {
        if(ScoreManager.m_playerLevel >= 6 && !_landChimp.m_isSuper)
        {
            m_gameSpeed += 0.5f;
        }
        
        ScoreManager.m_scoreValue += m_gameSpeed;
        ScoreManager.m_scoreValue = Mathf.Round(ScoreManager.m_scoreValue);
        _gameManager.m_highScoreValueText.text = ScoreManager.m_scoreValue.ToString();
        BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        
        if(ScoreManager.m_scoreValue >= 5000)
        {
            //SocialmediaManager.GooglePlayGamesLeaderboardUpdate();
        }
        
        Invoke("GameSpeed" , 5.8f);
    }

    void LevelGeneration()
    {
        _gameLayerObj.transform.Translate(Vector2.left * m_gameSpeed * Time.deltaTime);

        for(int i = 0; i < _gameLayerObj.transform.childCount; i++)
        {
            Transform child  = _gameLayerObj.transform.GetChild(i);

            if(child.position.x < _outofbounceX)
            {
                switch(child.gameObject.name)
                {
                    case "PF_Banana(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("Banana").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("Banana").transform;
                    break;

                    case "PF_BananaSkin(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("BananaSkin").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("BananaSkin").transform;
                    break;

                    case "PF_Blank(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("gBlank").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("gBlank").transform;
                    break;

                    case "PF_GroundLeft(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("gLeft").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("gLeft").transform;
                    break;

                    case "PF_GroundMiddle(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("gMiddle").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("gMiddle").transform;
                    break;

                    case "PF_GroundRight(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("gRight").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("gRight").transform;
                    break;

                    case "PF_Hurdle(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("Hurdle").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("Hurdle").transform;
                    break;

                    case "PF_LandLevelPolaroid(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("Polaroid").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("Polaroid").transform;
                    break;

                    case "PF_Portal(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("Portal").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("Portal").transform;
                    break;

                    case "PF_Super(Clone)":
                        child.gameObject.transform.position = _collectedTilesObj.transform.Find("Super").transform.position;
                        child.gameObject.transform.parent = _collectedTilesObj.transform.Find("Super").transform;
                    break;

                    default:
                        //Destroy(child.gameObject); //TODO Find out what's destroying child as this is not doing it
                    break;
                }
            }
        }

        if(_gameLayerObj.transform.childCount < 25)
        {
            SpawnTile();
        }
    }

    void PlayerLevelIncrease()
    {
        if(ScoreManager.m_playerLevel < 10)
        {
            ScoreManager.m_playerLevel++;
            BhanuPrefs.SetPPlayerLevel(ScoreManager.m_playerLevel);
            _scoreManager.m_playerLevelValueDisplay = ScoreManager.m_playerLevel;  // This line & variable _scoreManager is for testing purposes only
        }

        Invoke("PlayerLevelIncrease" , _playerLevelIncreaseRate + ScoreManager.m_playerLevel);
    }

    void RandomizeMiscObject()
    {
        if(_landChimp.m_isSuper || _bMiscObjAdded)
        {
            return;
        }

        else if(Random.Range(0 , 6) == 0)
        {
            GameObject banana = _collectedTilesObj.transform.Find("Banana").transform.GetChild(0).gameObject;
            banana.transform.parent = _gameLayerObj.transform;
            banana.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 4.3f)));            
            _bMiscObjAdded = true;
        }

        else if(Random.Range(0 , 6) == 1 && m_gameSpeed < 8 && (m_middleCounter > _minMiddleCounterValue + 1 || (ScoreManager.m_playerLevel < 5 && ScoreManager.m_playerLevel > 3)))
        {
            GameObject bananaSkin = _collectedTilesObj.transform.Find("BananaSkin").transform.GetChild(0).gameObject;
            bananaSkin.transform.parent = _gameLayerObj.transform;
            bananaSkin.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 2.0f)));
            _bMiscObjAdded = true;
        }

        else if(Random.Range(0 , 6) == 3 && (m_middleCounter > _minMiddleCounterValue || (ScoreManager.m_playerLevel < 5 && ScoreManager.m_playerLevel > 2)))
        {
            GameObject hurdle = _collectedTilesObj.transform.Find("Hurdle").transform.GetChild(0).gameObject;
            hurdle.transform.parent = _gameLayerObj.transform;
            hurdle.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 3.3f)));
            _bMiscObjAdded = true;
        }

        else if(Random.Range(0 , 6) == 2)
        {
            GameObject polaroid = _collectedTilesObj.transform.Find("Polaroid").transform.GetChild(0).gameObject;
            polaroid.transform.parent = _gameLayerObj.transform;
            polaroid.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 4.3f)));
            _bMiscObjAdded = true;
        }

        else if(Random.Range(0 , 6) == 4 && (m_middleCounter > _minMiddleCounterValue || (ScoreManager.m_playerLevel < 5 && ScoreManager.m_playerLevel > 3)))
        {
            GameObject portal = _collectedTilesObj.transform.Find("Portal").transform.GetChild(0).gameObject;
            portal.transform.parent = _gameLayerObj.transform;
            portal.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 4.3f)));
            _bMiscObjAdded = true;
        }

        else if(Random.Range(0 , 6) == 5 && ScoreManager.m_supersCount > 0)
        {
            GameObject super = _collectedTilesObj.transform.Find("Super").transform.GetChild(0).gameObject;
            super.transform.parent = _gameLayerObj.transform;
            super.transform.position = new Vector2(m_tilePosObj.transform.position.x + _tileWidth * 3.7f , _startUpPosY + (_heightLevel * _tileWidth + (_tileWidth * 4.3f)));
            _bMiscObjAdded = true;
        }
    }

    void SetTile(string type)
	{
		switch(type)
        {
            case "PF_Blank":
			    _tmpTileObj = _collectedTilesObj.transform.Find("gBlank").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundLeft":
			    _tmpTileObj = _collectedTilesObj.transform.Find("gLeft").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundMiddle":
                _tmpTileObj = _collectedTilesObj.transform.Find("gMiddle").transform.GetChild(0).gameObject;
		    break;

		    case "PF_GroundRight":
                _tmpTileObj = _collectedTilesObj.transform.Find("gRight").transform.GetChild(0).gameObject;
		    break;
        }

		_tmpTileObj.transform.parent = _gameLayerObj.transform;
		_tmpTileObj.transform.position = new Vector3(m_tilePosObj.transform.position.x + (_tileWidth) , _startUpPosY + (_heightLevel * _tileWidth) , 0);
		m_tilePosObj = _tmpTileObj;
		_lastTile = type;
	}

	void SpawnTile()
    {
		if(_blankCounter > 0)
        {
            if(ScoreManager.m_playerLevel >= 5)
            {
                SetTile("PF_Blank");
                _blankCounter--;
                return;
            }
            else
            {
                SetTile("PF_GroundMiddle");
                return;
            }
		}

        if(m_middleCounter > 0)
        {
            RandomizeMiscObject();
            SetTile("PF_GroundMiddle");
            m_middleCounter--;
			return;
		}

        _bMiscObjAdded = false;

		if(_lastTile == "PF_Blank")
        {
            m_middleCounter = Random.Range(1 , 15);
			ChangeHeight();
            SetTile("PF_GroundLeft");
		}

        else if(_lastTile == "PF_GroundMiddle")
        {
            if(ScoreManager.m_playerLevel >= 5)
            {
                SetTile("PF_GroundRight");
            }
            else
            {
                SetTile("PF_GroundMiddle");
            }
		}
        
        else if(_lastTile == "PF_GroundRight")
        {
            _blankCounter = Random.Range(1 , 3);
		}
	}
}
