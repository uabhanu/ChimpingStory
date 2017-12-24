using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    float m_startUpPosY;
	GameObject m_bananaObj , m_bananaPrefab , m_collectedTiles , m_tilePos;
    int m_heightLevel = 0 , m_totalBananas;

    [SerializeField] float m_spawnTime;

    [SerializeField] int m_minDistance , m_maxDistance , m_maxSpawnHeight , m_minSpawnHeight;

    void Reset()
    {
		m_minDistance = 10;
		m_maxDistance = 20;
		m_maxSpawnHeight = 7;
        m_minSpawnHeight = 5;
        m_spawnTime = 15.5f;
    }

    void Start()
    {
        m_bananaObj = GameObject.FindGameObjectWithTag("Banana");
        m_bananaPrefab = Resources.Load("PF_Banana") as GameObject;
        m_collectedTiles = GameObject.Find("Tiles");
        m_tilePos = GameObject.Find("StartTilePosition");
        m_startUpPosY = m_tilePos.transform.position.y;
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        m_totalBananas = Random.Range(0 , 10);

        if(m_bananaObj == null)
        {
            for(int i = 0 ; i < m_totalBananas; i++)
            {
                m_bananaObj = Instantiate(m_bananaPrefab , transform.position , Quaternion.identity);
                m_bananaObj.transform.parent = m_collectedTiles.transform.Find("Goodies").transform;
				m_bananaObj.transform.position = new Vector2(transform.position.x + i + Random.Range(m_minDistance , m_maxDistance) , m_startUpPosY + Random.Range(m_minSpawnHeight , m_maxSpawnHeight));
            }
        }

        StartCoroutine("SpawnRoutine");
    }
}
