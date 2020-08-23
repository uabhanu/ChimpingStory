using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager _scoreManager;
	private SoundManager _soundManager;

    void Start()
    {
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _scoreManager.m_scoreValue += 25;
            _scoreManager.m_HighScoreValueText.text = _scoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(_scoreManager.m_scoreValue);
			_soundManager.m_soundsSource.clip = _soundManager.m_coinCollected;

			if(SoundManager.m_playerMutedSounds == 0)
            {
                _soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
