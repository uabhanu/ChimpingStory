using UnityEngine;

public class Rock : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    private ScoreManager _scoreManager;
    private SoundManager _soundManager;
    
    [SerializeField] private GameObject m_explosionPrefab;

	void Start() 
	{
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _soundManager.m_soundsSource.clip = _soundManager.m_rockExplosion;
			
            if(SoundManager.m_playerMutedSounds == 0)
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
		_scoreManager.m_scoreValue += 100;
        _scoreManager.m_HighScoreValueText.text = _scoreManager.m_scoreValue.ToString();
		BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
