using UnityEngine;

public class LandLevelManager : MonoBehaviour
{
    [SerializeField] GameManagerSO _gameManagerSO;

    void Start()
    {
        _gameManagerSO.GetReferences();   
    }
}
