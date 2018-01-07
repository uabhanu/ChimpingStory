using UnityEngine;
using System.Collections;

public class ResolutionManager : MonoBehaviour 
{
    public Transform[] toScale;                     //Elements to scale
    public Transform[] toReposition;                //Elements to reposition

    public Renderer sand;                           //The renderer of the sand

    private float scaleFactor;                      //The current scale factor

    void Start()
    {
		scaleFactor = Camera.main.aspect / 1.28f;

        //Rescale elements
        foreach (Transform item in toScale)
            item.localScale = new Vector3(item.localScale.x * scaleFactor, item.localScale.y, item.localScale.z);

        //Reposition Elements
        foreach (Transform item in toReposition)
            item.position = new Vector3(item.position.x * scaleFactor, item.position.y, item.position.z);

        //Rescale sand 
        sand.material.mainTextureScale = new Vector2(scaleFactor, 1);
    }
}
