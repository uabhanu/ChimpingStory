#pragma strict

public class ResolutionManagerJS extends MonoBehaviour 
{
    public var toScale			: Transform[];		//Elements to scale
    public var toReposition		: Transform[];		//Elements to reposition

    public var sand				: Renderer;			//The renderer of the sand
    
    private var scaleFactor		: float;			//The current scale factor

    function Start()
    {
        scaleFactor = Camera.main.aspect / 1.28f;

        //Rescale elements
        for (var item : Transform in toScale)
            item.localScale = new Vector3(item.localScale.x * scaleFactor, item.localScale.y, item.localScale.z);

        //Reposition Elements
        for (var item : Transform in toReposition)
            item.position = new Vector3(item.position.x * scaleFactor, item.position.y, item.position.z);

        //Rescale sand 
        sand.material.mainTextureScale = new Vector2(scaleFactor, 1);
    }
}
