using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Achievement _collect3BananasAchievement , _collect6BananasAchievement;
    BoxCollider2D _bananaCollider2D;
    Camera _mainCamera;
	LandChimp _landChimp;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;
	SpriteRenderer _bananaRenderer;
    Vector3 _positionOnScreen;

    [SerializeField] string[] m_bananaCollectionAchievements;

    void Start()
    {
		_bananaCollider2D = GetComponent<BoxCollider2D>();
		_bananaRenderer = GetComponent<SpriteRenderer>();
        _collect3BananasAchievement = PlayGamesPlatform.Instance.GetAchievement("CgkInMKFu8wYEAIQBw");
        _collect6BananasAchievement = PlayGamesPlatform.Instance.GetAchievement("CgkInMKFu8wYEAIQCA");
		_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        _positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        if(_landChimp.m_isSuper)
        {
            _bananaCollider2D.enabled = false;
            _bananaRenderer.enabled = false;
        }
        else
        {
            if(_positionOnScreen.x >= 972)
            {
                _bananaCollider2D.enabled = true;
                _bananaRenderer.enabled = true;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            if(SocialmediaManager.b_gpgsLoggedIn)
            {
                if(!_collect3BananasAchievement.IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(m_bananaCollectionAchievements[0] , 1);
                }

                if(_collect3BananasAchievement.IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(m_bananaCollectionAchievements[1] , 1);
                }
            }

            if(_landChimp.m_isSlipping)
            {
                ScoreManager.m_scoreValue += 75;
            }
            else
            {
                ScoreManager.m_scoreValue += 25;
            }

            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
			_soundManager.m_soundsSource.clip = _soundManager.m_bananaCollected;

			if(_soundManager.m_soundsSource.enabled)
            {
                _soundManager.m_soundsSource.Play();
            }

            _bananaCollider2D.enabled = false;
			_bananaRenderer.enabled = false;
        }
    }
}
