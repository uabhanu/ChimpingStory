using UnityEngine;
using System.Collections;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
    Rigidbody2D m_chimpBody2D;

    [SerializeField] bool m_grounded = true;

    [SerializeField] float m_jumpForward , m_jumpHeight;

    [SerializeField] Transform m_bottom , m_top;

    public bool m_jumpPress = false;

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

        GroundCheck();

        m_chimpAnim.SetBool("Jog" , m_grounded);
        m_chimpAnim.SetBool("Jump" , !m_grounded);

        if(m_grounded)
        { 
            if(m_jumpPress)
            {
                Jump();
            }
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
        
        if(!m_grounded)
        {
            return;
        } 

		m_chimpBody2D.AddForce(new Vector2(m_jumpForward * Time.deltaTime , m_jumpHeight * Time.deltaTime));
		GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("Jump");	
	}
}
