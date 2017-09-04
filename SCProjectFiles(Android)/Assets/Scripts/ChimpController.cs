using UnityEngine;
using System.Collections;

public class ChimpController : MonoBehaviour
{
    Animator m_animator;
	bool m_inAir = false;
	int m_animState = Animator.StringToHash("animState");

    public bool m_jumpPress = false;

	void Start()
    {
		m_animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        } 

		if(!m_inAir && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.05f)
        {
			m_animator.SetInteger(m_animState,1);
			m_inAir =true;
		}
        
        else if(m_inAir && GetComponent<Rigidbody2D>().velocity.y == 0.00f)
        {
			m_animator.SetInteger(m_animState , 0);
			m_inAir =false;

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
}
