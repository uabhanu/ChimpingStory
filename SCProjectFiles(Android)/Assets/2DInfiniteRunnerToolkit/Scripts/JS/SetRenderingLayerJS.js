#pragma strict

public class SetRenderingLayerJS extends MonoBehaviour 
{
	public var sortingLayer		: String;			//The sorting layer
	public var sortingOrder		: int;				//The sorting order

	function Start()
	{
		this.GetComponent.<Renderer>().sortingLayerName = sortingLayer;
		this.GetComponent.<Renderer>().sortingOrder = sortingOrder;
	}
}