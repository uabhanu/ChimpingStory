using UnityEngine;

[ExecuteInEditMode()]  
public class SetRenderingLayer : MonoBehaviour 
{
	public string sortingLayer;             //The sorting layer
	public int sortingOrder;                //The sorting order

	void Start()
	{
        GetComponent<Renderer>().sortingLayerName = sortingLayer;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
}
