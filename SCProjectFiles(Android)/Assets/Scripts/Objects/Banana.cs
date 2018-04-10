using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Achievement[] _bananaAchievements;
    bool _bGotAllAchievements;
    BoxCollider2D _bananaCollider2D;
    Camera _mainCamera;
	LandChimp _landChimp;
    SocialmediaManager _socialmediaManager;
	SoundManager _soundManager;
	SpriteRenderer _bananaRenderer;
    Vector3 _positionOnScreen;

    [SerializeField] string[] _bananaCollectionAchievements;

    void Start()
    {
		_bananaCollider2D = GetComponent<BoxCollider2D>();
		_bananaRenderer = GetComponent<SpriteRenderer>();
        _bGotAllAchievements = false;
		_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _socialmediaManager = GameObject.Find("SocialmediaManager").GetComponent<SocialmediaManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Invoke("GetAchievements" , 0.5f);
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

    void GetAchievements()
    {
        //TODO For some reason, these are null
        _bananaAchievements[0] = PlayGamesPlatform.Instance.GetAchievement(_bananaCollectionAchievements[0]);
        _bananaAchievements[1] = PlayGamesPlatform.Instance.GetAchievement(_bananaCollectionAchievements[1]);
        _bananaAchievements[2] = PlayGamesPlatform.Instance.GetAchievement(_bananaCollectionAchievements[2]);
        _bananaAchievements[3] = PlayGamesPlatform.Instance.GetAchievement(_bananaCollectionAchievements[3]);
        _bananaAchievements[4] = PlayGamesPlatform.Instance.GetAchievement(_bananaCollectionAchievements[4]);

        if(_bananaAchievements[0] != null && _bananaAchievements[1] != null && _bananaAchievements[2] != null && _bananaAchievements[3] != null && _bananaAchievements[4] != null)
        {
            _bGotAllAchievements = true;
        }

        if(!_bGotAllAchievements)
        {
            Invoke("GetAchievements" , 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            //if(_bGotAllAchievements)
            //{
                _socialmediaManager.GooglePlayGamesIncrementalAchievements(_bananaCollectionAchievements[0] , 1);

                if(_bananaAchievements[0].IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(_bananaCollectionAchievements[1] , 1);
                }

                if(_bananaAchievements[1].IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(_bananaCollectionAchievements[2] , 1);
                }

                if(_bananaAchievements[2].IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(_bananaCollectionAchievements[3] , 1);
                }

                if(_bananaAchievements[3].IsUnlocked)
                {
                    _socialmediaManager.GooglePlayGamesIncrementalAchievements(_bananaCollectionAchievements[4] , 1);
                }
            //}

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
