using System.Collections;
using UnityEngine;

public class HurdleSpawner : MonoBehaviour
{
    ChimpController m_chimpController;
    float m_startUpPosY;
    GameObject m_collectedTiles, m_hurdleObj, m_hurdlePrefab, m_tilePos;

    [SerializeField] float m_spawnTime;

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
        m_tilePos = GameObject.Find("StartTilePosition");
        m_startUpPosY = m_tilePos.transform.position.y;
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        if(m_hurdleObj == null && !m_chimpController.m_super && m_chimpController.m_superPickUpsAvailable > 0)
        {
            m_hurdleObj = Instantiate(m_hurdlePrefab , transform.position , Quaternion.identity);
            m_hurdleObj.transform.parent = m_collectedTiles.transform.Find("Troubles").transform;
            m_hurdleObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 4.5f);
        }

        StartCoroutine("SpawnRoutine");
    }
}
