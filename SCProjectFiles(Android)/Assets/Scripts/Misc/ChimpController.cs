using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    bool m_isJumping , m_isSliding;
	BoxCollider2D m_blockerBottomCollider2D , m_chimpCollider2D;
    float m_defaultGravityScale , m_defaultJumpHeight , m_startPos;
	GameManager m_gameManager;
    [SerializeField] Ground m_ground;
	LevelCreator m_levelCreator;
    Rigidbody2D m_chimpBody2D;
	SoundManager m_soundManager;
    string m_currentScene;
    WaitForSeconds m_getGroundRoutine = new WaitForSeconds(0.95f);
    WaitForSeconds m_jumpRoutineDelay = new WaitForSeconds(0.55f);
    WaitForSeconds m_slideRoutineDelay = new WaitForSeconds(0.75f);
    WaitForSeconds m_slipRoutineDelay = new WaitForSeconds(5.15f);
    WaitForSeconds m_superRoutineDelay = new WaitForSeconds(30.25f);
    WaitForSeconds m_xPosRoutineDelay = new WaitForSeconds(0.35f);


    [SerializeField] bool m_grounded;
    [SerializeField] float m_jumpHeight , m_jumpingTime , m_slideTime , m_slipTime , m_superTime , m_xPosTime;
    [SerializeField] Transform m_raycastOrigin;

    public bool m_isSlipping , m_isSuper;

	void Reset()
	{
		m_jumpHeight = 15.5f;
		m_isSlipping = false;
		m_isSuper = false;
	}

	void Start()
    {
        m_blockerBottomCollider2D = GameObject.Find("BlockerBottom").GetComponent<BoxCollider2D>();
        m_chimpAnim = GetComponent<Animator>();
        m_chimpBody2D = GetComponent<Rigidbody2D>();
        m_chimpCollider2D = GetComponent<BoxCollider2D>();
        m_currentScene = SceneManager.GetActiveScene().name;
        m_defaultGravityScale = m_chimpBody2D.gravityScale;
        m_defaultJumpHeight = m_jumpHeight;
		m_gameManager = FindObjectOfType<GameManager>();
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_soundManager = FindObjectOfType<SoundManager>();
        m_startPos = transform.position.x;

		StartCoroutine("GetGroundRoutine");
        StartCoroutine("XPosRoutine");
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        Grounded();
	
        #if UNITY_EDITOR_64 || UNITY_STANDALONE_WIN

            if(Input.GetMouseButtonDown(0))
		    {
			    Jump();
		    }

		    if(Input.GetMouseButtonDown(1))
		    {
			    Slide();
		    }

        #endif
    }

    IEnumerator GetGroundRoutine()
    {
        yield return m_getGroundRoutine;
        m_ground = FindObjectOfType<Ground>();
        StartCoroutine("GetGroundRoutine");
    }

    IEnumerator JumpingRoutine()
    {
        yield return m_jumpRoutineDelay;

		if(!m_isSuper)
		{
			GameManager.m_selfieButtonImage.enabled = false;	
		}

        m_chimpAnim.SetBool("Jump" , false);
        m_isJumping = false;
    }

    IEnumerator SlideRoutine()
    {
        yield return m_slideRoutineDelay;

        if(m_chimpAnim.GetBool("Slide"))
		{
            m_chimpAnim.SetBool("Jog" , true);
            m_chimpAnim.SetBool("Slide" , false);
            m_chimpBody2D.gravityScale = m_defaultGravityScale;
            m_chimpCollider2D.enabled = true;
			GameManager.m_selfieButtonImage.enabled = false;
			m_isSliding = false;
        }
    }

    IEnumerator SlipRoutine()
    {
        yield return m_slipRoutineDelay;

        if(m_chimpAnim.GetBool("Slip"))
		{
			m_chimpAnim.SetBool("Slip" , false);
            m_levelCreator.m_gameSpeed = 6.0f;
            m_isSlipping = false;
		}
    }

	IEnumerator SuperRoutine()
	{
		yield return m_superRoutineDelay;
        m_blockerBottomCollider2D.enabled = false;
        m_chimpAnim.SetBool("Super" , false);
        m_chimpBody2D.gravityScale = m_defaultGravityScale;
		GameManager.m_selfieButtonImage.enabled = false;
		m_jumpHeight = m_defaultJumpHeight;

        if(LevelCreator.m_middleCounter == 5.5f)
        {
            LevelCreator.m_middleCounter -= 5.5f;
        }

		m_isSuper = false;	
	}

	IEnumerator XPosRoutine()
	{
		yield return m_xPosRoutineDelay;
		transform.position = new Vector2(m_startPos , transform.position.y);
		StartCoroutine("XPosRoutine");
	}

    void CheatDeath()
    {
        m_gameManager.Ads();
    }

    void Grounded()
    {
        if(!m_isSuper)
        {
            //Debug.DrawLine(new Vector2(transform.position.x , transform.position.y - 0.7f) , new Vector2(transform.position.x , transform.position.y - 0.95f) , Color.green);
            RaycastHit2D hit2D = Physics2D.Raycast(m_raycastOrigin.position , -Vector2.down);

            if(hit2D)
            {
                if(!hit2D.collider.isTrigger)
                {
                    //Debug.Log("Grounded");
                    m_grounded = true;
                }
                else
                {
                    //Debug.Log("Not Grounded because fell in the hole");
                    m_grounded = false;
                }
            }

            else if(!hit2D)
            {
                //Debug.Log("Not Grounded because Jumped");
                m_grounded = false;
            }
        }
    }
	
	public void Jump()
    {
        if(m_isJumping || m_isSliding)
        {
            return;
        }

		if(m_grounded && !m_isJumping && !m_isSliding)
        {
            m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);

			if(!m_isSuper)
			{
				GameManager.m_selfieButtonImage.enabled = true;
			}
            
            m_chimpAnim.SetBool("Jump" , true);
            m_isJumping = true;
			m_soundManager.m_soundsSource.clip = m_soundManager.m_jump;
			m_soundManager.m_soundsSource.Play();
            StartCoroutine("JumpingRoutine");
        }

        if(m_isSuper)
        {
            m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_jump;
			m_soundManager.m_soundsSource.Play();
            StartCoroutine("JumpingRoutine");
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Fall"))
        {
			m_soundManager.m_soundsSource.clip = m_soundManager.m_fallDeath;
			m_soundManager.m_soundsSource.Play();
            CheatDeath();
        }

        if(tri2D.gameObject.tag.Equals("Hurdle"))
        {
			m_soundManager.m_soundsSource.clip = m_soundManager.m_hurdleDeath;
			m_soundManager.m_soundsSource.Play();
            CheatDeath();
        }

		if(tri2D.gameObject.tag.Equals("Portal"))
		{
			int randomValue = Random.Range(0 , 4);
			string randomLevel = randomValue.ToString();

			switch(randomLevel)
			{
				case "0":
                    SceneManager.LoadScene("WaterSwimmer");
				break;

				case "1":
					SceneManager.LoadScene("WaterSwimmer");
				break;

				case "2":
					Screen.orientation = ScreenOrientation.Portrait;
					SceneManager.LoadScene("FallingDown");
				break;

				case "3":
					Screen.orientation = ScreenOrientation.Portrait;
					SceneManager.LoadScene("FallingDown");
				break;
			}
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

    public void Slide()
    {
		if(m_isJumping) 
		{
			return;
		}

		else if(m_grounded && !m_isJumping)
		{
			m_chimpAnim.SetBool("Jog" , false);
			m_chimpAnim.SetBool("Slide" , true);
			m_chimpBody2D.gravityScale = 0;
			m_chimpCollider2D.enabled = false;
			GameManager.m_selfieButtonImage.enabled = true;
			m_isSliding = true;
			StartCoroutine("SlideRoutine");	
		}
    }

    void Slip()
    {
        m_chimpAnim.SetBool("Slip" , true);
        m_levelCreator.m_gameSpeed *= 2.1f;
        m_isSlipping = true;
        StartCoroutine("SlipRoutine");
    }

	void Super()
	{
        m_blockerBottomCollider2D.enabled = true;
        m_chimpAnim.SetBool("Super" , true);
        m_chimpBody2D.gravityScale /= 2.5f;
		GameManager.m_selfieButtonImage.enabled = true;
        m_levelCreator.m_gameSpeed = 6.0f;

        if(LevelCreator.m_middleCounter == 0)
        {
            LevelCreator.m_middleCounter += 5.5f;
        }
        
		m_isSuper = true;
		StartCoroutine("SuperRoutine");
	}
}
