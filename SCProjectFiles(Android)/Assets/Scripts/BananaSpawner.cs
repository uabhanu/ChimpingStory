using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    float m_startUpPosY;
    GameObject m_banana , m_collectedTiles;
    int m_heightLevel = 0 , m_totalBananas = 6;

    [SerializeField] float m_spawnTime;

    [SerializeField] GameObject m_bananaPrefab , m_tilePos;

    void Start()
    {
        m_banana = GameObject.FindGameObjectWithTag("Banana");
        m_collectedTiles = GameObject.Find("Tiles");
        m_tilePos = GameObject.Find("StartTilePosition");
        m_startUpPosY = m_tilePos.transform.position.y;
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        if(m_banana == null)
        {
            for(int i = 0 ; i < m_totalBananas + 2 ; i += 2)
            {
                m_banana = Instantiate(m_bananaPrefab , transform.position , Quaternion.identity);
                m_banana.transform.parent = m_collectedTiles.transform.Find("Goodies").transform;
                m_banana.transform.position = new Vector2(transform.position.x + i , m_startUpPosY + m_heightLevel + Random.Range(2 , 6));
            }
        }

        StartCoroutine("SpawnRoutine");
    }
}
