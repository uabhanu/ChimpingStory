#pragma strict

public class ScrollingLayerJS extends MonoBehaviour 
{
    public var mainRenderer 		: Renderer;			//The main renderer
    public var startingSpeed		: float;			//The starting scrolling speed

    private var speedMultiplier		: float;			//The current speed multiplier
    private var paused				: boolean;			//True, if the level is paused

    private var offset				: Vector2;

	// Use this for initialization
	function Start () 
    {
        speedMultiplier = 1;
        paused = true;
	}
	// Update is called once per frame
	function Update () 
    {
        if (!paused)
        {
            offset = mainRenderer.material.mainTextureOffset;
            offset.x += startingSpeed * speedMultiplier * Time.deltaTime;
            mainRenderer.material.mainTextureOffset = offset;
        }
	}

    //Sets scrolling state
    public function SetPauseState(state : boolean)
    {
        paused = state;
    }
    //Updates speed multiplier
    public function UpdateSpeedMultiplier(n : float)
    {
        speedMultiplier = n;
    }
}
