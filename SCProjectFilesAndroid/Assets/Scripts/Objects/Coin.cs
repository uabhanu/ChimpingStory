using UnityEngine;

public class Coin : MonoBehaviour
{
    private CircleCollider2D _coinCollider2D;
    private LandLevelManager _gameManager;
	private LandPuss _landPuss;
	private SoundManager _soundManager;
    private SpriteRenderer _coinRenderer;

    void Start()
    {
        _coinCollider2D = GetComponent<CircleCollider2D>();
        _coinRenderer = GetComponent<SpriteRenderer>();
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
            _coinCollider2D.enabled = false;
            _coinRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _coinCollider2D.enabled = true;
            _coinRenderer.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            ScoreManager.m_scoreValue += 25;
            _gameManager.m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			_soundManager.m_soundsSource.clip = _soundManager.m_coinCollected;

			if(SoundManager.m_playerMutedSounds == 0)
            {
                _soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
