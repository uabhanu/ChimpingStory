using UnityEngine;

public class Super : MonoBehaviour 
{
	private BoxCollider2D _superCollider2D;
    private LandPuss _landPuss;
	private SoundManager m_soundManager;
    private SpriteRenderer _superRenderer;

	[SerializeField] private GameObject m_explosionPrefab;

	public static int m_pickedUp;

	void Start() 
	{
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
		//m_pickedUp = BhanuPrefs.GetSuperPickedUp();
		m_pickedUp = 0; //TODO This is for testing only and once done, remove this line and uncomment the one above
		m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _superCollider2D = GetComponent<BoxCollider2D>();
        _superRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }

		if(_landPuss.m_isSuper || m_pickedUp == 1)
        {
            _superCollider2D.enabled = false;
            _superRenderer.enabled = false;
        }
        else
        {
            _superCollider2D.enabled = true;
            _superRenderer.enabled = true;
        }
	}
		
	void OnTriggerEnter2D(Collider2D tri2D)
	{
		if(tri2D.gameObject.tag.Equals("Player"))
		{
            ScoreManager.m_supersCount--;
            BhanuPrefs.SetSuperPickedUp(ScoreManager.m_supersCount);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

			SpawnExplosion();
        }
	}

	void SpawnExplosion()
	{
        Explosion.m_explosionType = "Super";
        Instantiate(m_explosionPrefab , transform.position , Quaternion.identity);
		Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
