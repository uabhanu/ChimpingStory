using UnityEngine;

public class Portal : MonoBehaviour 
{
	private CircleCollider2D _portalCollider2D;
	private LandPuss _landPuss;
	private SpriteRenderer _portalRenderer;

	[SerializeField] private float m_respawnTimer;
    [SerializeField] private int m_pickedUp;

	void Start() 
	{
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_pickedUp = BhanuPrefs.GetPortalPickedUp(); //TODO Do this for Super also to enable them every 24 hours after you finish doing this for Portals here
		_portalCollider2D = GetComponent<CircleCollider2D>();	
		_portalRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper)
        {
            _portalCollider2D.enabled = false;
            _portalRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _portalCollider2D.enabled = true;
            _portalRenderer.enabled = true;
        }

        if(m_pickedUp == 1)
        {
            _portalCollider2D.enabled = false;
            _portalRenderer.enabled = false;
            m_respawnTimer = Time.time;
        }
        else
        {
            _portalCollider2D.enabled = true;
            _portalRenderer.enabled = true;
        }

        if(m_respawnTimer > 200.0f)
        {
            m_pickedUp = 0;
            m_respawnTimer = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col2D)
    {
        if(col2D.gameObject.tag == "Player")
        {
            m_pickedUp++;
            BhanuPrefs.SetPortalPickedUp(m_pickedUp);
        }
    }
}
