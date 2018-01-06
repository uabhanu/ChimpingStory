using UnityEngine;

public class SMSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_scoreManagerObj , m_scoreManagerPrefab;

    void Start()
    {
        m_scoreManagerObj = GameObject.FindGameObjectWithTag("ScoreManager");

        if(m_scoreManagerObj == null)
        {
            m_scoreManagerObj = Instantiate(m_scoreManagerPrefab);
        }
    }
}
