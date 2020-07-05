using UnityEngine;

public class Portal : MonoBehaviour 
{
	private Collider2D m_portalCollider2D;
	private LandPuss m_landPuss;
	private SpriteRenderer m_portalRenderer;

	[SerializeField] private float m_respawnTimer;
    [SerializeField] private int m_pickedUp;

	void Start() 
	{
		m_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_pickedUp = BhanuPrefs.GetPortalPickedUp();
		m_portalCollider2D = GetComponent<Collider2D>();	
		m_portalRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

        if(m_pickedUp == 1)
        {
            m_portalCollider2D.enabled = false;
            m_portalRenderer.enabled = false;
            m_respawnTimer = Time.time;
        }
        else
        {
            m_portalCollider2D.enabled = true;
            m_portalRenderer.enabled = true;
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
