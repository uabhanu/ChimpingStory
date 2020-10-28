using UnityEngine;

public class PointsPrefab : MonoBehaviour
{
    [SerializeField] private RectTransform _pointsPrefabRectTransform;
    public void SetStartPosition(Transform explosionPosition)
    {
        _pointsPrefabRectTransform = GameObject.FindGameObjectWithTag("Points").GetComponent<RectTransform>();
        _pointsPrefabRectTransform.anchoredPosition = new Vector2(130.0f , -3.0f);
    }

    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
