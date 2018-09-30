using UnityEngine;

public class Ground : MonoBehaviour 
{
	[SerializeField] BoxCollider2D _groundCollider2D;
	[SerializeField] LandChimp _landChimp;
	[SerializeField] SpriteRenderer _groundRenderer;

	void Start() 
	{
		_groundCollider2D = GetComponent<BoxCollider2D>();
		_groundRenderer = GetComponent<SpriteRenderer>();
		_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
	}

	void Update()
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(_landChimp.m_isSuper)
		{
			_groundCollider2D.enabled = false;
			_groundRenderer.enabled = false;
		}

		if(!_landChimp.m_isSuper)
		{
			_groundCollider2D.enabled = true;
			_groundRenderer.enabled = true;
		}
	}
}
