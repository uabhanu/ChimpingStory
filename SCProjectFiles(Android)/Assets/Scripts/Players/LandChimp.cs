using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LandChimp : MonoBehaviour
{
    Animator _chimpAnim;
    bool _isGrounded , _isJumping , _isSliding , _isUI;
    float _defaultGameSpeed , _yPosInSuperMode;
	GameManager _gameManager;
	LevelCreator _levelCreator;
    Rigidbody2D _chimpBody2D;
    RockSpawner _rockSpawner;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;

    [SerializeField] float _jumpHeight , _lowSlipMultiplier , _highSlipMultiplier , _slipTime;
    [SerializeField] Transform _raycastBottom , _raycastTop;

    public bool m_isSlipping , m_isSuper;

	void Reset()
	{
        _highSlipMultiplier = 1.75f;
        _lowSlipMultiplier = 1.54f;
        m_isSlipping = false;
		m_isSuper = false;
        _jumpHeight = 15.5f;
        _raycastBottom = GameObject.Find("RaycastBottom").transform;
        _raycastTop = GameObject.Find("RaycastTop").transform;
        _slipTime = 30f;
	}

	void Start()
    {
        _chimpAnim = GetComponent<Animator>();
        _chimpBody2D = GetComponent<Rigidbody2D>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        _defaultGameSpeed = _levelCreator.m_gameSpeed;
        LevelCreator.m_middleCounter = 0;
        _rockSpawner = GameObject.Find("RockSpawner").GetComponent<RockSpawner>();
        _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();
        _socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
		_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        BhanuInput();
        Grounded();
        UICheck();
    }

    void BhanuInput()
    {
        if(GameManager.m_isTestingUnityEditor)
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            if(Input.GetMouseButtonDown(0))
            {
                if((_isGrounded && !_isJumping && !_isSliding && !_isUI) || m_isSuper)
                {
                    Jump();
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                if((_isGrounded && !_isJumping && !_isSliding && !_isUI) || m_isSuper)
                {
                    Slide();
                }
            }
            #endif
        }
        
        if(SwipeManager.Instance.IsSwiping(SwipeDirection.UP))
        {
            Jump();
        }

        if(SwipeManager.Instance.IsSwiping(SwipeDirection.DOWN))
        {
            Slide();
        }
    }

    void CheatDeath()
    {
        _gameManager.Ads();
    }

    void Grounded()
    {
        if(!m_isSuper)
        {
            //Debug.DrawLine(m_raycastTop.position , m_raycastBottom.position , Color.red);
            RaycastHit2D hit2D = Physics2D.Raycast(_raycastTop.position , _raycastBottom.position);

            if(hit2D)
            {
                if(hit2D.collider.gameObject.GetComponent<Ground>()) //No Garbage but just like string comparison, m_isGrounded becoming false a little late for your liking
                {
                    _isGrounded = true;
                }

                else if(!hit2D.collider.gameObject.GetComponent<Ground>())
                {
                    _isGrounded = false;
                    SlideFinished();
                }

                else
                {
                    _isGrounded = false;
                    SlideFinished();
                }
            }

            else if(!hit2D)
            {
                _isGrounded = false;
                SlideFinished();
            }

            else
            {
                _isGrounded = false;
                SlideFinished();
            }
        }

        else if(m_isSuper)
        {
            LevelCreator.m_middleCounter = 0.5f;
            transform.position = new Vector2(-5.17f , Mathf.Clamp(transform.position.y , -0.98f , 3.25f));
        }
    }

    public static bool IsChimpion()
    {
        if(SocialmediaManager.m_playerRank == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	public void Jump()
    {
        if(_isGrounded && !_isJumping && !_isSliding && !_isUI) //This check exists in Update also for extra support as it's slow
        {
            _chimpAnim.SetBool("Jump" , true);
            _chimpBody2D.velocity = new Vector2(_chimpBody2D.velocity.x , _jumpHeight);
            SelfieAppear();
            _isJumping = true;
            Invoke("JumpFinished" , 0.55f);
		    _soundManager.m_soundsSource.clip = _soundManager.m_jump;

            if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }
        }
        
        if(m_isSuper)
        {
            _chimpBody2D.velocity = new Vector2(_chimpBody2D.velocity.x , _jumpHeight);
        }
    }

    void JumpFinished()
    {
        _chimpAnim.SetBool("Jump" , false);
        _isJumping = false;      
        
        if(!m_isSuper)
        {
            SelfieDisappear();
        }

        else if(m_isSuper)
        {
            Invoke("SelfieDisappear" , 0.75f);
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Fall"))
        {
			_soundManager.m_soundsSource.clip = _soundManager.m_fallDeath;

			if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            CheatDeath();
        }

        if(tri2D.gameObject.tag.Equals("Hurdle"))
        {
			_soundManager.m_soundsSource.clip = _soundManager.m_hurdleDeath;

			if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            CheatDeath();
        }

		if(tri2D.gameObject.tag.Equals("Portal"))
		{
            SceneManager.LoadScene("WaterSwimmer");
		}

        if(tri2D.gameObject.tag.Equals("Skin"))
        {
            Slip();
        }

		if(tri2D.gameObject.tag.Equals("Super"))
		{
			Super();
		}
    }

    void SelfieAppear()
    {
        GameManager.m_selfieButtonImage.enabled = true;
        SocialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
    }

    void SelfieDisappear()
    {
        GameManager.m_selfieButtonImage.enabled = false;
        SocialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
    }

    public void Slide()
    {
		if(_isGrounded && !_isJumping && !_isUI)
		{
			_chimpAnim.SetBool("Jog" , false);
			_chimpAnim.SetBool("Slide" , true);
			SelfieAppear();
			_isSliding = true;
			Invoke("SlideFinished" , 0.75f);
		}
    }

    void SlideFinished()
    {
        _chimpAnim.SetBool("Slide" , false);
		_isSliding = false;

        if(!_isJumping)
        {
            SelfieDisappear();
        }
    }

    void Slip()
    {
        _chimpAnim.SetBool("Slip" , true);

        if(_levelCreator.m_gameSpeed <= 8)
        {
            _levelCreator.m_gameSpeed *= _highSlipMultiplier;
        }
        else
        {
            _levelCreator.m_gameSpeed *= _lowSlipMultiplier;
        }
        
        m_isSlipping = true;
        Invoke("SlipFinished" , _slipTime);
    }

    void SlipFinished()
    {
		_chimpAnim.SetBool("Slip" , false);

        if(!m_isSuper)
        {
            _levelCreator.m_gameSpeed /= _highSlipMultiplier;
        }
        
        m_isSlipping = false;
    }

	void Super()
	{
        _isGrounded = false;
        m_isSuper = true;
        _chimpAnim.SetBool("Super" , true);
        _jumpHeight *= 1.5f;
		SelfieAppear();
        _levelCreator.m_gameSpeed = _defaultGameSpeed;
        SlipFinished();
        _rockSpawner.StartSpawnRoutine();
		Invoke("SuperFinished" , 30.25f);
	}

    void SuperFinished()
    {
        _chimpAnim.SetBool("Super" , false);
        _jumpHeight /= 1.5f;
        m_isSuper = false;	
        LevelCreator.m_middleCounter = 0;
    }

    void UICheck()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            _isUI = true;
        }

        else if(EventSystem.current.currentSelectedGameObject == null)
        {
            _isUI = false;
        }

        _gameManager.ChimpionshipBelt();

        if(SocialmediaManager.b_googlePlayGamesLoggedIn)
        {
            
            SocialmediaManager.b_googlePlayGamesAchievementsButtonAvailable = true;
            SocialmediaManager.b_googlePlayGamesLeaderboardButtonAvailable = true;

            if(ScoreManager.m_scoreValue >= ScoreManager.m_minHighScore)
            {
                SocialmediaManager.b_googlePlayGamesLeaderboardAvailable = true;
            }

            if(SocialmediaManager.b_scoresExist)
            {
                SocialmediaManager.b_googlePlayGamesLeaderboardAvailable = true;
            }
        }
        else
        {
            SocialmediaManager.b_googlePlayGamesAchievementsButtonAvailable = false;
            SocialmediaManager.b_googlePlayGamesLeaderboardAvailable = false;
            SocialmediaManager.b_googlePlayGamesLeaderboardButtonAvailable = false;
        }

        if(SocialmediaManager.b_googlePlayGamesAchievementsButtonAvailable)
        {
            SocialmediaManager.m_googlePlayGamesAchievementsButtonImage.sprite = _socialmediaManager.m_googlePlayGamessAchievementsButtonSprites[1];
        }

        if(!SocialmediaManager.b_googlePlayGamesAchievementsButtonAvailable)
        {
            SocialmediaManager.m_googlePlayGamesAchievementsButtonImage.sprite = _socialmediaManager.m_googlePlayGamessAchievementsButtonSprites[0];
        }

        if(SocialmediaManager.b_googlePlayGamesLeaderboardAvailable)
        {
            SocialmediaManager.m_googlePlayGamesLeaderboardButtonImage.sprite = _socialmediaManager.m_googlePlayGamesLeaderboardButtonSprites[1];
        }

        if(!SocialmediaManager.b_googlePlayGamesLeaderboardAvailable)
        {
            SocialmediaManager.m_googlePlayGamesLeaderboardButtonImage.sprite = _socialmediaManager.m_googlePlayGamesLeaderboardButtonSprites[0];
        }
    }
}
