using UnityEngine;

public class Hurdle : MonoBehaviour
{
    private BoxCollider2D _hurdleCollider2D;
    private LandPuss _landPuss;
    private SpriteRenderer _hurdleRenderer;

    void Start()
    {
        _hurdleCollider2D = GetComponent<BoxCollider2D>();
        _hurdleRenderer = GetComponent<SpriteRenderer>();
        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
    }

    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        if(_landPuss.m_isSuper)
        {
            _hurdleCollider2D.enabled = false;
            _hurdleRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _hurdleCollider2D.enabled = true;
            _hurdleRenderer.enabled = true;
        }
	}
}
