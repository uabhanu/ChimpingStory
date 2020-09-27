using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager _scoreManager;
	
    [SerializeField] private SoundManagerObject _soundManagerObject;

    void Start()
    {
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _scoreManager.m_scoreValue += 25;
            _scoreManager.m_HighScoreValueText.text = _scoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
			_soundManagerObject.m_soundsSource.clip = _soundManagerObject.m_coinCollected;

			if(_soundManagerObject.m_playerMutedSounds == 0)
            {
                _soundManagerObject.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
