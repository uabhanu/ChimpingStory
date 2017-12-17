using UnityEngine;
using System.Collections;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    Rigidbody2D m_chimpBody2D;

    [SerializeField] bool m_grounded = true;

    [SerializeField] float m_jumpHeight , m_slideTime , m_slipTime;

    [SerializeField] LevelCreator m_levelCreationScript;

    [SerializeField] Transform m_bottom , m_top;

    public bool m_jumpPress = false , m_slip , m_super;

	void Start()
    {
		m_chimpAnim = GetComponent<Animator>();
        m_chimpBody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
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
            m_levelCreationScript.m_gameSpeed = 6.0f;
            m_slip = false;
		}
    }

    void GroundCheck()
    {
        Debug.DrawLine(m_top.position , m_bottom.position , Color.green);
        m_grounded = Physics2D.Linecast(m_top.position , m_bottom.position);
    }


	public void Jump()
    {
		m_jumpPress = true;

        m_chimpAnim.SetBool("Jump" , true);
        
        if(!m_grounded)
        {
            return;
        } 

		m_chimpBody2D.velocity = new Vector2(m_chimpBody2D.velocity.x , m_jumpHeight);
		GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("Jump");	
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Skin"))
        {
            Slip();
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
        m_levelCreationScript.m_gameSpeed *= 1.5f;
        m_slip = true;
        StartCoroutine("SlipRoutine");
    }
}
