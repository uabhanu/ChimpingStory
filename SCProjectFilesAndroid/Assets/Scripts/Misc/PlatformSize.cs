using UnityEngine;

public class PlatformSize : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _platformCollider2D;
    [SerializeField] private SpriteRenderer _platformRenderer;
    [SerializeField] private Vector2[] _platformSizes;

    private void Start()
    {
        PlatformResize();
    }

    public void PlatformResize()
    {
        int i = Random.Range(0 , _platformSizes.Length);
        _platformCollider2D.size = _platformSizes[i];
        _platformCollider2D.offset = new Vector2(_platformCollider2D.size.x / 2 , 0f);
        _platformRenderer.size = _platformSizes[i];
    }
}
