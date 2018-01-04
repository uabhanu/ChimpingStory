using System.Collections;
using UnityEngine;

public class BananasSpawner : MonoBehaviour
{
    ChimpController m_chimpController;
    float m_startUpPosY;
    GameObject m_collectedTiles , m_gameLayer , m_tilePos;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject[] m_bananasPrefabs;

    public static int m_bananasCount = 0;

    void Reset()
    {
        m_spawnTime = 0.5f;
    }

    void Start()
    {
        //m_chimpController = FindObjectOfType<ChimpController>();
        //m_collectedTiles = GameObject.Find("Tiles");
        //m_tilePos = GameObject.Find("StartTilePosition");
        //m_startUpPosY = m_tilePos.transform.position.y;
        //StartCoroutine("SpawnRoutine");

        m_gameLayer = GameObject.Find("GameLayer");

        for(int i = 0; i < 10; i++)
        {
            GameObject bananas = GameObject.FindGameObjectWithTag("Bananas");

            if(bananas == null)
            {
                bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)]);
                bananas.transform.parent = m_collectedTiles.transform.Find("Hurdle").transform;
                bananas.transform.position = Vector2.zero;
            }
        }
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        if(m_bananasCount < 2)
        {
            GameObject bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)] , transform.position , Quaternion.identity);
            bananas.transform.parent = m_collectedTiles.transform.Find("Bananas").transform;
            bananas.transform.position = new Vector2(transform.position.x , m_startUpPosY + 6);
            m_bananasCount++;
        }

        StartCoroutine("SpawnRoutine");
    }
}
