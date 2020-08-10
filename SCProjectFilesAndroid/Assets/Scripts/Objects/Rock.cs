using UnityEngine;

public class Rock : MonoBehaviour 
{
    private CircleCollider2D _portalCollider2D;
    private LandLevelManager _gameManager;
    private SoundManager _soundManager;
    private SpriteRenderer _portalRenderer;
    
    [SerializeField] private GameObject m_explosionPrefab;

	void Start() 
	{
        _gameManager = GameObject.Find("LandLevelManager").GetComponent<LandLevelManager>();
        _portalCollider2D = GetComponent<CircleCollider2D>();
        _portalRenderer = GetComponent<SpriteRenderer>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _soundManager.m_soundsSource.clip = _soundManager.m_rockExplosion;
			
            if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            SpawnExplosion();
        }
    }

	void SpawnExplosion()
	{
        Explosion.m_explosionType = "Rock";
        Instantiate(m_explosionPrefab , transform.position , Quaternion.identity);
		ScoreManager.m_scoreValue += 100;
        _gameManager.m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
