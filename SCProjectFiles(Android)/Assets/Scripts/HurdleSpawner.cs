using System.Collections;
using UnityEngine;

public class HurdleSpawner : MonoBehaviour
{
    bool m_hurdleAdded;
    ChimpController m_chimpController;
    [SerializeField] float m_startUpPosX , m_startUpPosY;
    const float m_tileWidth = 1.25f;
    GameObject m_collectedTiles , m_hurdleObj , m_hurdlePrefab , m_troublesLayer;
    int m_heightLevel = 0;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_tilePos;

    void Reset()
    {
        m_spawnTime = 30.5f;
    }

    void Start()
    {
        m_chimpController = FindObjectOfType<ChimpController>();
        m_hurdleObj = GameObject.FindGameObjectWithTag("Hurdle");
        m_hurdlePrefab = Resources.Load("PF_Hurdle") as GameObject;
        m_collectedTiles = GameObject.Find("Tiles");
        m_startUpPosX = m_tilePos.transform.position.x;
        m_startUpPosY = m_tilePos.transform.position.y;
        m_troublesLayer = GameObject.Find("Troubles");
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        if(m_hurdleObj == null && !m_chimpController.m_super)
        {
            m_hurdleObj = Instantiate(m_hurdlePrefab , transform.position , Quaternion.identity);
            m_hurdleObj.transform.parent = m_troublesLayer.transform;
            m_hurdleObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 5.41f);
        }

        StartCoroutine("SpawnRoutine");
    }
}
