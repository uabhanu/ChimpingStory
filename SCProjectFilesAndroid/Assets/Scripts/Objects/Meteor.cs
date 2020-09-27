using UnityEngine;

public class Meteor : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    private ScoreManager _scoreManager;
    
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private SoundManagerObject _soundManagerObject;

	void Start() 
	{
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _soundManagerObject.m_soundsSource.clip = _soundManagerObject.m_rockExplosion;
			
            if(_soundManagerObject.m_playerMutedSounds == 0)
            {
                _soundManagerObject.m_soundsSource.Play();
            }

            SpawnExplosion();
        }
    }

	void SpawnExplosion()
	{
        Explosion.m_explosionType = "Meteor";
        Instantiate(m_explosionPrefab , transform.position , Quaternion.identity);
		_scoreManager.m_scoreValue += 100;
        _scoreManager.m_HighScoreValueText.text = _scoreManager.m_scoreValue.ToString();
		BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
