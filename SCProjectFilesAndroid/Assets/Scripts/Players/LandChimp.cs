using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LandChimp : MonoBehaviour
{
    Animator _chimpAnim;
    bool _bIsGrounded , _bHighSlip , _bIsJumping , _bLowSlip , _bIsSliding , _bIsUI;
    float _defaultGameSpeed , _yPosInSuperMode;
	GameManager _gameManager;
    Rigidbody2D _chimpBody2D;
    RockSpawner _rockSpawner;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;

    [SerializeField] float _jumpHeight , _lowSlipMultiplier , _highSlipMultiplier , _slipTime;
    [SerializeField] string _holeAchievementID , _slipAchievementID , _superAchievementID;
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
        _defaultGameSpeed = LevelCreator.m_gameSpeed;
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LevelCreator.m_middleCounter = 0;
        _rockSpawner = GameObject.Find("RockSpawner").GetComponent<RockSpawner>();
        _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();
        //_socialmediaManager.GooglePlayGamesLeaderboardPlayerRank();
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
        if(GameManager.b_isUnityEditorTestingMode)
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            if(Input.GetMouseButtonDown(0))
            {
                if((_bIsGrounded && !_bIsJumping && !_bIsSliding && !_bIsUI) || m_isSuper)
                {
                    Jump();
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                if((_bIsGrounded && !_bIsJumping && !_bIsSliding && !_bIsUI) || m_isSuper)
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
                if(hit2D.collider.gameObject.GetComponent<Ground>())
                {
                    _bIsGrounded = true;
                }

                else if(!hit2D.collider.gameObject.GetComponent<Ground>())
                {
                    _bIsGrounded = false;
                    SlideFinished();
                }

                else
                {
                    _bIsGrounded = false;
                    SlideFinished();
                }
            }

            else if(!hit2D)
            {
                _bIsGrounded = false;
                SlideFinished();
            }

            else
            {
                _bIsGrounded = false;
                SlideFinished();
            }
        }

        else if(m_isSuper)
        {
            LevelCreator.m_middleCounter = 0.5f;
            transform.position = new Vector2(-5.17f , Mathf.Clamp(transform.position.y , -0.98f , 3.25f));
        }
    }

	public void Jump()
    {
        if(_bIsGrounded && !_bIsJumping && !_bIsSliding && !_bIsUI) //This check exists in Update also for extra support as it's slow
        {
            _bIsJumping = true;
            _chimpAnim.SetBool("Jump" , true);
            _chimpBody2D.velocity = new Vector2(_chimpBody2D.velocity.x , _jumpHeight);
            Invoke("JumpFinished" , 0.55f);
            SelfieAppear();
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
        _bIsJumping = false;      
        
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
            //_socialmediaManager.GooglePlayGamesIncrementalAchievements(_holeAchievementID , 1);
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
            _socialmediaManager.GooglePlayGamesAchievements(_superAchievementID);
			Super();
		}
    }

    void SelfieAppear()
    {
        GameManager.m_selfieButtonImage.enabled = true;
        _socialmediaManager.GooglePlayGamesLeaderboardTestMenuDisappear();
    }

    void SelfieDisappear()
    {
        GameManager.m_selfieButtonImage.enabled = false;
        _socialmediaManager.GooglePlayGamesLeaderboardTestMenuAppear();
    }

    public void Slide()
    {
		if(_bIsGrounded && !_bIsJumping && !_bIsUI)
		{
            _bIsSliding = true;
			_chimpAnim.SetBool("Jog" , false);
			_chimpAnim.SetBool("Slide" , true);
			Invoke("SlideFinished" , 0.75f);
            SelfieAppear();
		}
    }

    void SlideFinished()
    {
        _chimpAnim.SetBool("Slide" , false);
		_bIsSliding = false;

        if(!_bIsJumping)
        {
            SelfieDisappear();
        }
    }

    void Slip()
    {
        _chimpAnim.SetBool("Slip" , true);

        if(LevelCreator.m_gameSpeed <= 8)
        {
            _bHighSlip = true;
            LevelCreator.m_gameSpeed *= _highSlipMultiplier;
        }
        else
        {
            _bLowSlip = true;
            LevelCreator.m_gameSpeed *= _lowSlipMultiplier;
        }
        
        m_isSlipping = true;
        Invoke("SlipFinished" , _slipTime);
    }

    void SlipFinished()
    {
		_chimpAnim.SetBool("Slip" , false);

        if(!m_isSuper)
        {
            if(_bHighSlip)
            {
                LevelCreator.m_gameSpeed /= _highSlipMultiplier;
                _bHighSlip = false;
                _bLowSlip = false;
                _socialmediaManager.GooglePlayGamesAchievements(_slipAchievementID);
            }
            
            else if(_bLowSlip)
            {
                LevelCreator.m_gameSpeed /= _lowSlipMultiplier;
                _bHighSlip = false;
                _bLowSlip = false;
                _socialmediaManager.GooglePlayGamesAchievements(_slipAchievementID);
            }
        }
        
        m_isSlipping = false;
    }

	void Super()
	{
        _bIsGrounded = false;
        m_isSuper = true;
        _chimpAnim.SetBool("Super" , true);
        GameManager.m_polaroidImage.enabled = false;
        GameManager.m_polaroidsCountText.enabled = false;
        _jumpHeight *= 1.5f;
        LevelCreator.m_gameSpeed = _defaultGameSpeed;
		SelfieAppear();
        SlipFinished();
        _rockSpawner.StartSpawnRoutine();
		Invoke("SuperFinished" , 30.25f);
	}

    void SuperFinished()
    {
        _chimpAnim.SetBool("Super" , false);
        GameManager.m_polaroidImage.enabled = true;
        GameManager.m_polaroidsCountText.enabled = true;
        _jumpHeight /= 1.5f;
        m_isSuper = false;	
        LevelCreator.m_gameSpeed = _defaultGameSpeed;
        LevelCreator.m_middleCounter = 0;
    }

    void UICheck()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            _bIsUI = true;
        }

        else if(EventSystem.current.currentSelectedGameObject == null)
        {
            _bIsUI = false;
        }

        if(SocialmediaManager.b_gpgsLoggedIn)
        {
            SocialmediaManager.b_gpgsAchievementsButtonAvailable = true;
            SocialmediaManager.b_gpgsLeaderboardButtonAvailable = true;
        }
        else
        {
            SocialmediaManager.b_gpgsAchievementsButtonAvailable = false;
            SocialmediaManager.b_gpgsLeaderboardButtonAvailable = false;
        }

        if(SocialmediaManager.b_gpgsAchievementsButtonAvailable)
        {
            SocialmediaManager.m_gpgsAchievementsButtonImage.sprite = _socialmediaManager.m_gpgsAchievementsButtonSprites[1];
        }

        if(!SocialmediaManager.b_gpgsAchievementsButtonAvailable)
        {
            SocialmediaManager.m_gpgsAchievementsButtonImage.sprite = _socialmediaManager.m_gpgsAchievementsButtonSprites[0];
        }

        if(SocialmediaManager.b_gpgsLeaderboardButtonAvailable)
        {
            SocialmediaManager.m_gpgsLeaderboardButtonImage.sprite = _socialmediaManager.m_gpgsLeaderboardButtonSprites[1];
        }

        if(!SocialmediaManager.b_gpgsLeaderboardButtonAvailable)
        {
            SocialmediaManager.m_gpgsLeaderboardButtonImage.sprite = _socialmediaManager.m_gpgsLeaderboardButtonSprites[0];
        }
    }
}
