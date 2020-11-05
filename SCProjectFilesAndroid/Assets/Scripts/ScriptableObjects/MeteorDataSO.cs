using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Meteor Data", fileName = "NewPMeteorData")]
public class MeteorDataSO : ScriptableObject
{
    [SerializeField] private GameObject _meteorSmashedPointsPrefab;

    public GameObject GetMeteorSmashedPointsPrefab()
    {
        return _meteorSmashedPointsPrefab;
    }
}
