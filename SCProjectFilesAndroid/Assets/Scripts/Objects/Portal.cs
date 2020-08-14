using UnityEngine;

public class Portal : MonoBehaviour 
{
	private CircleCollider2D _portalCollider2D;
	private LandPuss _landPuss;
	private SpriteRenderer _portalRenderer;
    
    public static int _pickedUp;

	void Start() 
	{
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _pickedUp = BhanuPrefs.GetPortalPickedUp();
		_portalCollider2D = GetComponent<CircleCollider2D>();	
		_portalRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper || _pickedUp == 1)
        {
            _portalCollider2D.enabled = false;
            _portalRenderer.enabled = false;
        }
        else
        {
            _portalCollider2D.enabled = true;
            _portalRenderer.enabled = true;
        }
    }
}
