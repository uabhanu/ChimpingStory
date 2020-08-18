using UnityEngine;

public class Polaroid : MonoBehaviour
{
    private BoxCollider2D _polaroidCollider2D;
    private LandLevelManager _gameManager;
    private LandPuss _landPuss;
	private SoundManager _soundManager;
    private SpriteRenderer _polaroidRenderer;

    void Start()
    {
        _gameManager = GameObject.Find("LandLevelManager").GetComponent<LandLevelManager>();
        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _polaroidCollider2D = GetComponent<BoxCollider2D>();
        _polaroidRenderer = GetComponent<SpriteRenderer>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        if(_landPuss.m_isSuper)
        {
            _polaroidCollider2D.enabled = false;
            _polaroidRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _polaroidCollider2D.enabled = true;
            _polaroidRenderer.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            ScoreManager.m_polaroidsCount++;
            BhanuPrefs.SetPolaroidsCount(ScoreManager.m_polaroidsCount);
            _gameManager.m_PolaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
            ScoreManager.m_scoreValue += 25;
            _gameManager.m_HighScoreValueText.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			_soundManager.m_soundsSource.clip = _soundManager._coinCollected;
			
            if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
