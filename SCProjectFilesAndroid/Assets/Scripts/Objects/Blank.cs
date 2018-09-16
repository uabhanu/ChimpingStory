using UnityEngine;

public class Blank : MonoBehaviour 
{
    Camera _mainCamera;
    GameManager _gameManager;
    Transform _gameLayer;
    Vector3 _positionOnScreen;

    void Start()
    {
        _gameLayer = GameObject.Find("GameLayer").transform;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();  
    }

    void Update() 
    {
		if(Time.timeScale == 0f)
		{
			return;
		}

        _positionOnScreen = _mainCamera.ScreenToWorldPoint(transform.position);

        if(_positionOnScreen.x <= -8.81f && transform.IsChildOf(_gameLayer))
        {
            //_gameManager.FirstTimeJumpTutorial();
        }
    }
}
