using SVGImporter;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChimpController : MonoBehaviour 
{
    //private BoxCollider2D superchimpCollider2D;
    //private GameObject superChimp;
    BoxCollider2D chimpCollider2D;

	[SerializeField] GameObject bananaCountObj , bananaImageObj , dollarButtonObj , pauseButtonObj , superChimpCountObj , superChimpImageObj , trophyCountObj , trophyImageObj;

    Rigidbody2D chimpBody2D;

    [SerializeField] SVGImage selfieButtonImage;
	//private SVGRenderer ground01Renderer;
	//private SVGRenderer ground02Renderer;
	//private SVGRenderer ground03Renderer;
	//private SVGRenderer superchimpRenderer;

	public Animator chimpAnim;
	public AudioSource deathSound;
	public AudioSource jumpSound;
	public BananaSkin bananaSkinScript;
	public bool canJump , canSlide , chimpInTheHole;
	public bool chimpSlip;
	public bool grounded;
	public bool slide;
	public bool superMode;
	public float chimpSpeed;
	public float groundCheckRadius;
	public float jumpHeight;
	public float slideTime;
	public float slipTime;
	public float superTime;
	public GameObject chimpBlocker;
	public GameManager gameManagementScript;
	//public Image bananaImage;
	//public Image dollarButtonImage;
	//public Image trophyImage;
	public Ground groundScript;
	public LayerMask whatIsGround;
	public ScoreManager scoreManagementScript;
	public SpriteRenderer superChimpRenderer;
	public Transform groundCheck;

	void Start() 
	{
		chimpAnim.SetBool("DefaultSpeed" , true);
		chimpAnim.SetBool("MediumSpeed" , false);
		//chimpAnim.SetBool("HighSpeed" , false);
		chimpBody2D = GetComponent<Rigidbody2D>();
        chimpCollider2D = GetComponent<BoxCollider2D>();
        chimpInTheHole = false;
	}

	void FixedUpdate()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		if(!superMode) 
		{			
			chimpAnim.SetBool("Super" , false);
			grounded = Physics2D.OverlapCircle(groundCheck.position , groundCheckRadius , whatIsGround);

            if(grounded)
            {
                canJump = true;
                canSlide = true;

                if(!chimpAnim.GetBool("Slide"))
                {
                    selfieButtonImage.enabled = false;
                }

                if(chimpAnim.GetBool("Slide"))
                {
                    selfieButtonImage.enabled = true;
                }
            }

            if(!grounded)
            {
                canJump = false;
                canSlide = false;
                selfieButtonImage.enabled = true;
            }
		} 

		else if(superMode) 
		{
            canJump = true;
			chimpAnim.SetBool("Super" , true);
		}

		slide = chimpAnim.GetBool("Slide");
		Run();
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

#if UNITY_EDITOR_64

        if (Input.GetMouseButton(0) && canJump)
		{
			Jump();
		}

		else if(Input.GetMouseButton(1) && canSlide)
		{
			Slide();
		}

		#endif
	}

    IEnumerator ChimpCollider2D()
    {
        yield return new WaitForSeconds(0.5f);
        chimpCollider2D.isTrigger = false;
    }

	IEnumerator ChimpSlip()
	{
		yield return new WaitForSeconds(slipTime);
		chimpSlip = false;
		chimpAnim.SetBool("DefaultSpeed" , true);
		chimpAnim.SetBool("MediumSpeed" , false);
		groundScript.speed = 4f;
	}


	IEnumerator SlideTimer()
	{
		yield return new WaitForSeconds(slideTime);

		if(chimpAnim.GetBool("Slide"))
		{
            //Debug.Log("Slide Time"); Working
			chimpAnim.SetBool("Slide" , false);
		}
	}

	IEnumerator SuperChimpTimer()
	{
		yield return new WaitForSeconds(superTime);
		chimpBlocker.SetActive(false);
		chimpBody2D.gravityScale = 5f;
		superMode = false;
	}
		
	void Death()
	{
		//Debug.Log("Player Died"); Working
		deathSound.Play();
		gameManagementScript.RestartGame();
		bananaCountObj.SetActive(false);
		bananaImageObj.SetActive(false);
		dollarButtonObj.SetActive(false);
		superChimpCountObj.SetActive(false);
		superChimpImageObj.SetActive(false);
		pauseButtonObj.SetActive(false);
		trophyCountObj.SetActive(false);
		trophyImageObj.SetActive(false);
	}

	void Dying()
	{

	}

	public void Jump()
	{
        if(!chimpInTheHole)
        {
            jumpSound.Play(); //Turned off for testing purposes but turn back on for final version
            chimpBody2D.velocity = new Vector2(chimpBody2D.velocity.x, jumpHeight);
            chimpBlocker.SetActive(false);

            if (superMode)
            {
                chimpBody2D.velocity = new Vector2(chimpBody2D.velocity.x, jumpHeight * 1.1f);
            }
        }
	}

	void OnTriggerEnter2D(Collider2D col2D)
	{
        if(col2D.gameObject.name.Equals("BananaSkin"))
        {
            if (!superMode)
            {
                //Debug.Log("Chimp Slip"); //Working
                chimpAnim.SetBool("DefaultSpeed", false);
                chimpAnim.SetBool("MediumSpeed", true);
                chimpSlip = true;
                groundScript.speed += 4f;
                StartCoroutine("ChimpSlip");
            }
        }

        if(col2D.gameObject.tag.Equals("Death")) 
		{
			if(!superMode)
			{
				Debug.Log("Chimp Died");
				Death();
			}
		}

		if(col2D.gameObject.tag.Equals("Enemy") && !superMode) 
		{
			Debug.Log("Chimp Died");
			Death();
		}

		if(col2D.gameObject.tag.Equals("SC"))
		{
			SuperChimp();
		}
	}

	void Run()
	{
		chimpAnim.SetBool("Grounded" , grounded);
		//chimpBody2D.velocity = new Vector2(chimpSpeed , chimpBody2D.velocity.y);

        if(scoreManagementScript.bananasLeft == 0)
        {
            gameManagementScript.WinCard();
        }
	}

	public void Slide()
	{
        if(!chimpInTheHole)
        {
            //Debug.Log("Slide"); //Working
            chimpAnim.SetBool("Slide", true);
            StartCoroutine("SlideTimer");
        }
	}

	void SuperChimp()
	{
		chimpBlocker.SetActive(true);
	
		chimpBody2D.gravityScale = 3.5f;

		if(groundScript.speed == 8f)
		{
			groundScript.speed = 4f;
		}
			
		superMode = true;
		StartCoroutine("SuperChimpTimer");
	}
}
