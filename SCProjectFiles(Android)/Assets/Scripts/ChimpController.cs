using UnityEngine;
using System.Collections;

public class ChimpController : MonoBehaviour
{
    Animator m_chimpAnim;
	bool m_grounded = true , m_inAir = false;

    public bool m_jumpPress = false;

	void Start()
    {
		m_chimpAnim = GetComponent<Animator>();
        m_chimpAnim.SetBool("Jog" , true);
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        
        if(m_grounded)
        {
            m_chimpAnim.SetBool("Jump" , true);
        }

        if(!m_grounded)
        {
            m_chimpAnim.SetBool("Jump" , false);
        }

		if(!m_inAir && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.00f)
        {
            m_inAir = true;
		}
        
        else if(m_inAir && GetComponent<Rigidbody2D>().velocity.y == 0.00f)
        {
            m_inAir = false;

			if(m_jumpPress)
            {
                Jump();
            } 
		}
	}

	public void Jump()
    {
		m_jumpPress = true;
        
        if(m_inAir)
        {
            return;
        } 

		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3000);
		GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("Jump");	
	}

    void OnCollisionEnter2D(Collision2D col2D) //This sucks so try Raycast or Layermask method
    {
        if(col2D.gameObject.tag.Equals("Ground"))
        {
            m_grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col2D) //This sucks so try Raycast or Layermask method
    {
        if(col2D.gameObject.tag.Equals("Ground"))
        {
            m_grounded = false;
        }
    }
}
