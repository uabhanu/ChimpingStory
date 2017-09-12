using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    float m_startUpPosY;
    GameObject m_banana , m_bananaPrefab , m_collectedTiles , m_tilePos;
    int m_heightLevel = 0 , m_totalBananas;

    [SerializeField] float m_spawnTime;

    [SerializeField] int m_maxBananaSpawnHeight , m_minBananaSpawnHeight;

    void Reset()
    {
        m_maxBananaSpawnHeight = 7;
        m_minBananaSpawnHeight = 4;
        m_spawnTime = 3.5f;
    }

    void Start()
    {
        m_banana = GameObject.FindGameObjectWithTag("Banana");
        m_bananaPrefab = Resources.Load("PF_Banana") as GameObject;
        m_collectedTiles = GameObject.Find("Tiles");
        m_tilePos = GameObject.Find("StartTilePosition");
        m_startUpPosY = m_tilePos.transform.position.y;
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        m_totalBananas = Random.Range(0 , 20);

        if(m_banana == null)
        {
            for(int i = 0 ; i < m_totalBananas; i += 2)
            {
                m_banana = Instantiate(m_bananaPrefab , transform.position , Quaternion.identity);
                m_banana.transform.parent = m_collectedTiles.transform.Find("Goodies").transform;
                //m_banana.transform.position = new Vector2(transform.position.x + i , m_startUpPosY + m_heightLevel + Random.Range(6 , 11));
                int distanceBetween = 20;
                m_banana.transform.position = new Vector2(i + transform.position.x + distanceBetween , m_startUpPosY + Random.Range(m_minBananaSpawnHeight , m_maxBananaSpawnHeight));
            }
        }

        StartCoroutine("SpawnRoutine");
    }
}
