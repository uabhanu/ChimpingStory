using UnityEngine;

public class Banana : MonoBehaviour
{
    private BoxCollider2D _bananaCollider2D;
    private LandLevelManager _gameManager;
	private LandPuss _landPuss;
	private SoundManager _soundManager;
    private SpriteRenderer _bananaRenderer;

    void Start()
    {
        _bananaCollider2D = GetComponent<BoxCollider2D>();
        _bananaRenderer = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.Find("LandLevelManager").GetComponent<LandLevelManager>();
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper)
        {
            _bananaCollider2D.enabled = false;
            _bananaRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _bananaCollider2D.enabled = true;
            _bananaRenderer.enabled = true;
        }
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

			if(SoundManager.m_playerMutedSounds == 0)
            {
                _soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
