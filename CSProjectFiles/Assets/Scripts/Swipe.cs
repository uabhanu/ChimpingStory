using System.Collections;
using UnityEngine;

public class Swipe : MonoBehaviour 
{
	//private bool hasSwiped = false;
	private float distance = 0f;
	private Touch initialTouch = new Touch();

	public ChimpController chimpControlScript;
	public Rigidbody2D chimpBody2D;
	public float swipeValue;

	void Update() 
	{
		if(Time.deltaTime == 0f)
		{
			return;
		}

		foreach(Touch t in Input.touches)
		{
			if(t.phase == TouchPhase.Began)
			{
				initialTouch = t;
			}

			else if(t.phase == TouchPhase.Moved/* && !hasSwiped*/)
			{
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
				bool swipeHorizontal = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

				if(distance > swipeValue)
				{
					if(swipeHorizontal && deltaX > 0) //Swiped Left
					{

					}

					else if(swipeHorizontal && deltaX <= 0) //Swiped Right
					{

					}

					else if(!swipeHorizontal && deltaY > 0) //Swiped Down
					{
						Debug.Log("Swipe Down");
						chimpControlScript.Slide();
					}

					else if(!swipeHorizontal && deltaY <= 0) //Swiped Up
					{
						Debug.Log("Swipe Up");
						chimpControlScript.Jump();
					}

					//hasSwiped = true;
				}
			}

			else if(t.phase == TouchPhase.Ended)
			{
				initialTouch = new Touch();
			}
		}
	}
}
