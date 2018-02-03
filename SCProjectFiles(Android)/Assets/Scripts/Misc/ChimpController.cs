using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    bool m_jumping , m_sliding;
	BoxCollider2D m_blockerBottomCollider2D , m_chimpCollider2D;
    float m_defaultGravityScale , m_defaultJumpHeight , m_startPos;
	GameManager m_gameManager;
	LevelCreator m_levelCreator;
    Rigidbody2D m_chimpBody2D;
	SoundManager m_soundManager;
    string m_currentScene;

    [SerializeField] bool m_grounded;
    [SerializeField] float m_jumpHeight , m_jumpingTime , m_slideTime , m_slipTime , m_superTime , m_xPosTime;

    public bool m_slip , m_super;

	void Reset()
	{
		m_jumpHeight = 15.5f;
        m_jumpingTime = 0.75f;
		m_slideTime = 0.75f;
		m_slip = false;
		m_slipTime = 5.15f;
		m_super = false;
		m_superTime = 30.25f;
		m_xPosTime = 0.35f;
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

		StartCoroutine("XPosRoutine");
    }

    void FixedUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        Grounded();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

		if(!m_grounded && !m_super)
		{
			m_chimpBody2D.gravityScale = m_defaultGravityScale;
			m_chimpCollider2D.enabled = true;
		}
			
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

        m_chimpAnim.SetBool("Jog" , m_grounded);
        m_chimpAnim.SetBool("Jump" , !m_grounded);
    }

    IEnumerator JumpingRoutine()
    {
        yield return new WaitForSeconds(m_jumpingTime);

		if(!m_super)
		{
			GameManager.m_selfieButtonImage.enabled = false;	
		}

        m_jumping = false;
    }

    IEnumerator SlideRoutine()
    {
        yield return new WaitForSeconds(m_slideTime);

        if(m_chimpAnim.GetBool("Slide"))
		{
            m_chimpAnim.SetBool("Jog" , true);
            m_chimpAnim.SetBool("Slide" , false);
            m_chimpBody2D.gravityScale = m_defaultGravityScale;
            m_chimpCollider2D.enabled = true;
			GameManager.m_selfieButtonImage.enabled = false;
			m_sliding = false;
        }
    }

    IEnumerator SlipRoutine()
    {
        yield return new WaitForSeconds(m_slipTime);

        if(m_chimpAnim.GetBool("Slip"))
		{
			m_chimpAnim.SetBool("Slip" , false);
            m_levelCreator.m_gameSpeed = 6.0f;
            m_slip = false;
		}
    }

	IEnumerator SuperRoutine()
	{
		yield return new WaitForSeconds(m_superTime);
        m_blockerBottomCollider2D.enabled = false;
        m_chimpAnim.SetBool("Super" , false);
        m_chimpBody2D.gravityScale = m_defaultGravityScale;
		GameManager.m_selfieButtonImage.enabled = false;
		m_jumpHeight = m_defaultJumpHeight;
		m_super = false;	
	}

	IEnumerator XPosRoutine()
	{
		yield return new WaitForSeconds (m_xPosTime);
		transform.position = new Vector2(m_startPos , transform.position.y);
		StartCoroutine("XPosRoutine");
	}

    void CheatDeath()
    {
        m_gameManager.Ads();
    }

    void Grounded()
    {
        if(m_super)
        {
            m_grounded = false;
            return;
        }
        else
        {
            Debug.DrawLine(new Vector2(transform.position.x , transform.position.y - 0.7f) , new Vector2(transform.position.x , transform.position.y - 0.95f) , Color.green);
            RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(transform.position.x , transform.position.y - 0.7f) , new Vector2(transform.position.x , transform.position.y - 0.95f));

            if(hit2D)
            {
                if(hit2D.collider.tag.Equals("Ground"))
                {
                    Debug.Log(hit2D.collider.name);
                    m_grounded = true;
                }

                else
                {
                    m_grounded = false;
                }
            }

            else if(!hit2D)
            {
                m_grounded = false;
            }
        }
    }
	
	public void Jump()
    {
        if(m_jumping || m_sliding)
        {
            return;
        }

		if(m_grounded && !m_jumping && !m_sliding)
        {
            m_chimpAnim.SetBool("Jump" , true);
            m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);

			if(!m_super)
			{
				GameManager.m_selfieButtonImage.enabled = true;
			}

			m_jumping = true;
			m_soundManager.m_soundsSource.clip = m_soundManager.m_jump;
			m_soundManager.m_soundsSource.Play();
            StartCoroutine("JumpingRoutine");
        }

        if(m_super)
        {
            m_chimpAnim.SetBool("Jump" , true);
            m_jumping = true;
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
		if(m_jumping) 
		{
			return;
		}

		else if(m_grounded && !m_jumping)
		{
			m_chimpAnim.SetBool("Jog" , false);
			m_chimpAnim.SetBool("Slide" , true);
			m_chimpBody2D.gravityScale = 0;
			m_chimpCollider2D.enabled = false;
			GameManager.m_selfieButtonImage.enabled = true;
			m_sliding = true;
			StartCoroutine("SlideRoutine");	
		}
    }

    void Slip()
    {
        m_chimpAnim.SetBool("Slip" , true);
        m_levelCreator.m_gameSpeed *= 2.1f;
        m_slip = true;
        StartCoroutine("SlipRoutine");
    }

	void Super()
	{
        m_blockerBottomCollider2D.enabled = true;
        m_chimpAnim.SetBool("Super" , true);
        m_chimpBody2D.gravityScale /= 2.5f;
		GameManager.m_selfieButtonImage.enabled = true;
        m_levelCreator.m_gameSpeed = 6.0f;
		m_super = true;
		StartCoroutine("SuperRoutine");
	}
}
