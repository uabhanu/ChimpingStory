using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D _platformCollider2D;
    private LandPuss _landPuss;
    private SpriteRenderer _platformRenderer;
    void Start()
    {
        _landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        _platformCollider2D = GetComponent<BoxCollider2D>();
        _platformRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_landPuss.m_isSuper)
        {
            _platformCollider2D.enabled = false;
            _platformRenderer.enabled = false;
        }

        else if(!_landPuss.m_isSuper)
        {
            _platformCollider2D.enabled = true;
            _platformRenderer.enabled = true;
        }
    }
}
