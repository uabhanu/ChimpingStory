using UnityEngine;

public class Meteor : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private ScoreManagerObject _scoreManagerObject;
    [SerializeField] private SoundManagerObject _soundManagerObject;
		
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
		_scoreManagerObject.m_scoreValue += 100;
        _scoreManagerObject.m_HighScoreValueText.text = _scoreManagerObject.m_scoreValue.ToString();
		BhanuPrefs.SetHighScore(_scoreManagerObject.m_scoreValue);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
