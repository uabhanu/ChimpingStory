using UnityEngine;

public class GUIManager : MonoBehaviour
{
    GameManager m_gameManager;

    [SerializeField] LevelManager levelManager;

    void Start()
    {
        m_gameManager = GameObject.Find("LandLevelManager").GetComponent<GameManager>();
        levelManager.StartLevel();
    }
}