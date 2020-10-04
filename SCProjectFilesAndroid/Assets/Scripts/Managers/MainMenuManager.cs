using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameManagerSO _gameManagerSO;

    private void Start()
    {
        _gameManagerSO.GetMainMenuLevelObjects();
    }
}
