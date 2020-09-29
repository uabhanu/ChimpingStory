using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameManagerObject _gameManagerObj;

    void Start()
    {
        _gameManagerObj.GetBhanuObjects();   
    }
}
