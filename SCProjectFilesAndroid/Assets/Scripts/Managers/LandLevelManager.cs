using UnityEngine;

public class LandLevelManager : MonoBehaviour
{
    [SerializeField] GameManagerSO _gameManagerObj;

    void Start()
    {
        _gameManagerObj.GetLandLevelObjects();   
    }
}
