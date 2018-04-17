using UnityEngine;

public class Hurdle : MonoBehaviour
{
    Camera _mainCamera;
    LandChimp _landChimp;
    Collider2D _hurdleCollider2D;
    SpriteRenderer _hurdleRenderer;
    Transform _gameLayer;
    Vector3 _positionOnScreen;

    void Start()
    {
        _gameLayer = GameObject.Find("GameLayer").transform;
        _hurdleCollider2D = GetComponent<Collider2D>();
        _hurdleRenderer = GetComponent<SpriteRenderer>();
        _landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	void Update()
    {
		if(Time.timeScale == 0)
        {
            return;
        }

        _positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        if(_positionOnScreen.x < 700 && transform.IsChildOf(_gameLayer))
        {
            GameManager.FirstTimeSlideTutorial();
        }

        if(_landChimp.m_isSlipping || _landChimp.m_isSuper)
        {
            _hurdleCollider2D.enabled = false;
            _hurdleRenderer.enabled = false;
        }
        else
        {
            if(_positionOnScreen.x >= 972)
            {
                _hurdleCollider2D.enabled = true;
                _hurdleRenderer.enabled = true;
            }
        }
    }
}
