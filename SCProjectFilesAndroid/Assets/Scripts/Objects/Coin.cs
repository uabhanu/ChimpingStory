using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ScoreManagerObject _scoreManagerObject;	
    [SerializeField] private SoundManagerObject _soundManagerObject;

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _scoreManagerObject.m_scoreValue += 25;
            _scoreManagerObject.m_HighScoreValueText.text = _scoreManagerObject.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(_scoreManagerObject.m_scoreValue);
			_soundManagerObject.m_soundsSource.clip = _soundManagerObject.m_coinCollected;

			if(_soundManagerObject.m_playerMutedSounds == 0)
            {
                _soundManagerObject.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
