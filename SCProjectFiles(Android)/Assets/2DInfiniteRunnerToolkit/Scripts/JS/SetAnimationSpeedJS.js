#pragma strict

public class SetAnimationSpeedJS extends MonoBehaviour 
{
    public var animator			: Animator;			//The target animatior
    public var animatorSpeed	: float;			//The speed of the animation [0..1]

	// Use this for initialization
	function Start ()
    {
        animator.speed = animatorSpeed;
	}
}