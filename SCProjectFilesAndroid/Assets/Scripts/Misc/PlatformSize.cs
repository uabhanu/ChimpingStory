using System.Collections.Generic;
using UnityEngine;

public class PlatformSize : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _platformCollider2D;
    [SerializeField] private SpriteRenderer _platformRenderer;
    [SerializeField] private Vector2[] _platformSizes;

    private void Awake()
    {
        PlatformResize();    
    }

    void PlatformResize()
    {
        int i = Random.Range(0 , _platformSizes.Length);
        _platformCollider2D.size = _platformSizes[i];
        _platformRenderer.size = _platformSizes[i];
    }
}
