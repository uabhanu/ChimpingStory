using UnityEngine;

[CreateAssetMenu]
public class MeteorDataSO : ScriptableObject
{
    [SerializeField] private GameObject _meteorSmashedPointsPrefab;

    public GameObject GetMeteorSmashedPointsPrefab()
    {
        return _meteorSmashedPointsPrefab;
    }
}
