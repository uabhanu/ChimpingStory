using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    AudioSource m_soundsSource;
    bool m_sliding;
	BoxCollider2D m_blockerCollider2D , m_chimpCollider2D;
    float m_defaultGravityScale , m_startPos;
	GameManager m_gameManager;
	LevelCreator m_levelCreator;
    Rigidbody2D m_chimpBody2D;
	SoundsContainer m_soundsContainer;
    string m_currentScene;

    [SerializeField] bool m_grounded;
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
        m_chimpCollider2D = GetComponent<BoxCollider2D>();
        m_currentScene = SceneManager.GetActiveScene().name;
        m_defaultGravityScale = m_chimpBody2D.gravityScale;
		m_gameManager = FindObjectOfType<GameManager>();
		m_levelCreator = FindObjectOfType<LevelCreator>();
		m_soundsContainer = FindObjectOfType<SoundsContainer>();
        m_soundsSource = GetComponent<AudioSource>();
        m_startPos = transform.position.x;
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
        Grounded();
    }

    IEnumerator SlideRoutine()
    {
        yield return new WaitForSeconds(m_slideTime);

        if(m_chimpAnim.GetBool("Slide"))
		{
            m_sliding = false;
            m_chimpAnim.SetBool("Jog" , true);
            m_chimpAnim.SetBool("Slide" , false);
            m_chimpBody2D.gravityScale = m_defaultGravityScale;
            m_chimpCollider2D.enabled = true;
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
		m_blockerCollider2D.enabled = false;
		m_chimpAnim.SetBool("Super" , false);
		m_jumpHeight = m_defaultJumpHeight;
		m_super = false;	
	}

    void CheatDeath()
    {
        m_gameManager.Ads();
    }

    void Grounded()
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
	
	public void Jump()
    {
        if(!m_grounded || m_sliding)
        {
            return;
        }
        else
        {
            m_chimpAnim.SetBool("Jump" , true);
            m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);
            m_soundsSource.clip = m_soundsContainer.m_jumpSound;
            m_soundsSource.Play();
        }
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
        m_sliding = true;
        m_chimpAnim.SetBool("Jog" , false);
        m_chimpAnim.SetBool("Slide" , true);
        m_chimpBody2D.gravityScale = 0;
        m_chimpCollider2D.enabled = false;
        StartCoroutine("SlideRoutine");
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
		transform.position = new Vector2(transform.position.x , m_blockerCollider2D.gameObject.transform.position.y + 1.5f);
		m_blockerCollider2D.enabled = true;
		m_chimpAnim.SetBool("Super" , true);
        m_levelCreator.m_gameSpeed = 6.0f;
		m_super = true;
        m_superPickUpsAvailable--;
		StartCoroutine("SuperRoutine");
	}
}
