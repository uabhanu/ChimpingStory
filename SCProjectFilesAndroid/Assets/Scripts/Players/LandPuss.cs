using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LandPuss : MonoBehaviour
{
    Animator _pussAnim;
    [SerializeField] bool _bIsGrounded , _bHighSlip , _bIsJumping , _bLowSlip , _bIsSliding , _bIsUI;
    GameManager _gameManager;
    Rigidbody2D _pussBody2D;
    RockSpawner _rockSpawner;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;

    [SerializeField] float _defaultMoveSpeed , _jumpHeight , _lowSlipMultiplier , _highSlipMultiplier , _slipTime;
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
        _pussAnim = GetComponent<Animator>();
        _pussBody2D = GetComponent<Rigidbody2D>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        Movement();
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

    public Vector3 GetPosition()
    {
        return transform.position;
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
            transform.position = new Vector2(-5.17f , Mathf.Clamp(transform.position.y , -0.98f , 3.25f));
        }
    }

	public void Jump()
    {
        if(_bIsGrounded && !_bIsJumping && !_bIsSliding && !_bIsUI) //This check exists in Update also for extra support as it's slow
        {
            _bIsJumping = true;
            _pussAnim.SetBool("Jump" , true);
            _pussBody2D.velocity = new Vector2(_pussBody2D.velocity.x , _jumpHeight);
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
            _pussBody2D.velocity = new Vector2(_pussBody2D.velocity.x , _jumpHeight);
        }
    }

    void JumpFinished()
    {
        _pussAnim.SetBool("Jump" , false);
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

    void Movement()
    {
        transform.Translate(-Vector2.left * _defaultMoveSpeed * Time.deltaTime , Space.Self);
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
			_pussAnim.SetBool("Jog" , false);
			_pussAnim.SetBool("Slide" , true);
			Invoke("SlideFinished" , 0.75f);
            SelfieAppear();
		}
    }

    void SlideFinished()
    {
        _pussAnim.SetBool("Slide" , false);
		_bIsSliding = false;

        if(!_bIsJumping)
        {
            SelfieDisappear();
        }
    }

    void Slip()
    {
        _pussAnim.SetBool("Slip" , true);
        Invoke("SlipFinished" , _slipTime);
    }

    void SlipFinished()
    {
		_pussAnim.SetBool("Slip" , false);

        if(!m_isSuper)
        {
            if(_bHighSlip)
            {
                _bHighSlip = false;
                _bLowSlip = false;
                _socialmediaManager.GooglePlayGamesAchievements(_slipAchievementID);
            }
            
            else if(_bLowSlip)
            {
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
        _pussAnim.SetBool("Super" , true);
        GameManager.m_polaroidImage.enabled = false;
        GameManager.m_polaroidsCountText.enabled = false;
        _jumpHeight *= 1.5f;
		SelfieAppear();
        SlipFinished();
        _rockSpawner.StartSpawnRoutine();
		Invoke("SuperFinished" , 30.25f);
	}

    void SuperFinished()
    {
        _pussAnim.SetBool("Super" , false);
        GameManager.m_polaroidImage.enabled = true;
        GameManager.m_polaroidsCountText.enabled = true;
        _jumpHeight /= 1.5f;
        m_isSuper = false;	
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
