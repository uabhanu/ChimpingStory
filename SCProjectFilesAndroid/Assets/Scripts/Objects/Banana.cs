using UnityEngine;

public class Banana : MonoBehaviour
{
    private LandLevelManager _gameManager;
	private LandPuss _landPuss;
	private SoundManager _soundManager;

    void Start()
    {
        _gameManager = GameObject.Find("LandLevelManager").GetComponent<LandLevelManager>();
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            if(_landPuss.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 75;
            }
            else
            {
                ScoreManager.m_scoreValue += 25;
            }

            _gameManager.m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			_soundManager.m_soundsSource.clip = _soundManager.m_bananaCollected;

			if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
