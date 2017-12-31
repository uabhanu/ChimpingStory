using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    AudioSource m_soundsSource;
	BoxCollider2D m_blockerCollider2D;
    float m_startPos;
	GameManager m_gameManager;
	LevelCreator m_levelCreator;
    Rigidbody2D m_chimpBody2D;
	SoundsContainer m_soundsContainer;
    string m_currentScene;
	Transform m_groundCheckBottom , m_groundCheckTop , m_holeCheckBottom , m_holeCheckTop;

	[SerializeField] bool m_grounded = true;
    [SerializeField] float m_defaultJumpHeight = 15.5f , m_jumpHeight , m_slideTime , m_slipTime , m_superTime;

    public bool m_slip , m_super;
    [HideInInspector] public int m_superPickUpsAvailable = 1;

	void Reset()
	{
		m_jumpHeight = 15.5f;
		m_slideTime = 1.1f;
		m_slip = false;
		m_slipTime = 5.15f;
		m_super = false;
		m_superTime = 30.25f;
	}

	void Start()
    {
		m_blockerCollider2D = GameObject.Find("BlockerBottom").GetComponent<BoxCollider2D>();
		m_chimpAnim = GetComponent<Animator>();
        m_chimpBody2D = GetComponent<Rigidbody2D>();
        m_currentScene = SceneManager.GetActiveScene().name;
		m_gameManager = FindObjectOfType<GameManager>();
		m_groundCheckBottom = GameObject.Find("GroundCheckBottom").transform;
		m_groundCheckTop = GameObject.Find("GroundCheckTop").transform;
		m_holeCheckBottom = GameObject.Find("HoleCheckBottom").transform;
		m_holeCheckTop = GameObject.Find("HoleCheckTop").transform;
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_soundsContainer = FindObjectOfType<SoundsContainer>();
        m_soundsSource = GetComponent<AudioSource>();
        m_startPos = transform.position.x;
    }
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(transform.position.x < m_startPos - 2.7f)
        {
            m_soundsSource.clip = m_soundsContainer.m_fallDeathSound;
            m_soundsSource.Play();
            CheatDeath();
        }

        #if UNITY_EDITOR_64 || UNITY_STANDALONE_WIN

            if(Input.GetMouseButton(0))
		    {
			    Jump();
		    }

		    else if(Input.GetMouseButton(1))
		    {
			    Slide();
		    }

		#endif

        GroundCheck();
		HoleCheck();

        if(m_grounded)
        {
            m_chimpAnim.SetBool("Jump" , false);
        }

        m_chimpAnim.SetBool("Jog" , m_grounded);
	}

    IEnumerator SlideRoutine()
    {
        yield return new WaitForSeconds(m_slideTime);

        if(m_chimpAnim.GetBool("Slide"))
		{
			m_chimpAnim.SetBool("Slide" , false);
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

		if(HoleCheck()) //Tried !HoleCheck() at first but reverse seems to work instead of the actual one. but fine for now
		{
			m_blockerCollider2D.enabled = false;
			m_chimpAnim.SetBool("Super" , false);
			m_jumpHeight = m_defaultJumpHeight;
			m_super = false;	
		}
	}

    void CheatDeath()
    {
        m_gameManager.Ads();
    }

    void GroundCheck()
    {
        Debug.DrawLine(m_groundCheckTop.position , m_groundCheckBottom.position , Color.green);
        m_grounded = Physics2D.Linecast(m_groundCheckTop.position , m_groundCheckBottom.position);
    }

	bool HoleCheck()
	{
		Debug.DrawLine(m_holeCheckTop.position , m_holeCheckBottom.position , Color.red);
		return(Physics2D.Linecast(m_holeCheckTop.position , m_holeCheckBottom.position)); //This should return true if line collided with Hole
	}
		
	public void Jump()
    {
        m_chimpAnim.SetBool("Jump" , true);
        
        if(!m_grounded)
        {
            return;
        } 
			
		m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);
		m_soundsSource.clip = m_soundsContainer.m_jumpSound;
		m_soundsSource.Play();
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("BPortal"))
        {
            SceneManager.LoadScene("BananaDestroyer");
        }

        if(tri2D.gameObject.tag.Equals("Fall"))
        {
            m_soundsSource.clip = m_soundsContainer.m_fallDeathSound;
            m_soundsSource.Play();
            CheatDeath();
        }

        if(tri2D.gameObject.tag.Equals("Hurdle"))
        {
            m_soundsSource.clip = m_soundsContainer.m_enemyDeathSound;
            m_soundsSource.Play();
            CheatDeath();
        }

        if(tri2D.gameObject.tag.Equals("Skin"))
        {
            Slip();
        }

		if(tri2D.gameObject.tag.Equals("Super"))
		{
			Super();
		}

        if(tri2D.gameObject.tag.Equals("WPortal"))
        {
            SceneManager.LoadScene("WaterSwimmer");
        }
    }

    public void Slide()
    {
        m_chimpAnim.SetBool("Slide" , true);
        StartCoroutine("SlideRoutine");   
    }

    void Slip()
    {
        m_chimpAnim.SetBool("Slip" , true);
        m_levelCreator.m_gameSpeed *= 2.5f;
        m_slip = true;
        StartCoroutine("SlipRoutine");
    }

	void Super()
	{
		transform.position = new Vector2(transform.position.x , m_blockerCollider2D.gameObject.transform.position.y + 1.5f);
		m_blockerCollider2D.enabled = true;
		m_chimpAnim.SetBool("Super" , true);
        m_levelCreator.m_gameSpeed = 6.0f;
		m_super = true;
        m_superPickUpsAvailable--;
		StartCoroutine("SuperRoutine");
	}
}
