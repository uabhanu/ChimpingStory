using UnityEngine;
using System.Collections;

public class ChimpController : MonoBehaviour
{
    Animator _animator;
	bool inAir = false;
	int _animState = Animator.StringToHash("animState");

    public bool jumpPress = false;

	void Start()
    {
		_animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        } 

		if(!inAir && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.05f)
        {
			_animator.SetInteger(_animState,1);
			inAir =true;
		}
        
        else if(inAir && GetComponent<Rigidbody2D>().velocity.y == 0.00f)
        {
			_animator.SetInteger(_animState , 0);
			inAir =false;

			if(jumpPress)
            {
                Jump();
            } 
		}
	}

	public void Jump()
    {
		jumpPress = true;

        if(inAir)
        {
            return;
        } 

		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3000);
		GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("jump");	
	}
}
