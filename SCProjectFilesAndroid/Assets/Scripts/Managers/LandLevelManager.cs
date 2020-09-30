using UnityEngine;

public class LandLevelManager : MonoBehaviour
{
    [SerializeField] GameManagerObject _gameManagerObj;

    void Start()
    {
        _gameManagerObj.GetLandLevelObjects();   
    }
}
