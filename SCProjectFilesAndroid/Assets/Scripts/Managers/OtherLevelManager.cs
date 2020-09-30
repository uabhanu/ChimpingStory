using UnityEngine;

public class OtherLevelManager : MonoBehaviour
{
    [SerializeField] GameManagerObject _gameManagerObj;

    void Start()
    {
        _gameManagerObj.GetOtherLevelObjects();   
    }
}
