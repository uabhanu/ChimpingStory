using System.Collections;
using UnityEngine;

public class BananasSpawner : MonoBehaviour
{
    LandChimp m_landChimp;
    LevelCreator m_levelCreator;
    GameObject m_collectedTiles;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(0.16f);

    [SerializeField] [Tooltip("This is max number of banana prefabs to spawn, Ask Bhanu for more info :)")] [Range(0 , 10)] int m_maxBananas; // Unable to use prefabs.Length for some reason
    [SerializeField] GameObject[] m_bananasPrefabs;

    public static int m_bananasCount = 0;

    void Start()
    {
        m_collectedTiles = GameObject.Find("Tiles");
        m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        m_levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
    }

    IEnumerator SpawnRoutine()
    {
        yield return m_spawnRoutineDelay;

        if(m_bananasCount < m_maxBananas && GameManager.m_currentScene == 1 && !m_landChimp.m_isSuper && m_levelCreator.m_middleCounter > 1)
        {
            GameObject bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)] , transform.position , Quaternion.identity);
            bananas.transform.parent = m_collectedTiles.transform.Find("Bananas").transform;
            bananas.transform.position = new Vector2(bananas.transform.position.x + 2.5f , bananas.transform.position.y - 0.2f);
            m_bananasCount++;
        }
    }

    public void StartBananaSpawnRoutine()
    {
        StartCoroutine("SpawnRoutine");
    }

    public void StopBananaSpawnRoutine()
    {
        StopCoroutine("SpawnRoutine");
    }
}
