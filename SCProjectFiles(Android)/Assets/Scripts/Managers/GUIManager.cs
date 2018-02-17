using UnityEngine;

public class GUIManager : MonoBehaviour
{
    GameManager m_gameManager;

    [SerializeField] LevelManager levelManager;

    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelManager.StartLevel();
    }
}