using UnityEngine;

[CreateAssetMenu]
public class MeteorDataSO : ScriptableObject
{
    [SerializeField] private GameObject _meteorSmashedPointsPrefab;
    [SerializeField] private RectTransform _pointsPrefabPosition;

    public GameObject GetMeteorSmashedPointsPrefab()
    {
        return _meteorSmashedPointsPrefab;
    }

    public RectTransform GetPointsPrefabPosition()
    {
        return _pointsPrefabPosition;
    }
}
