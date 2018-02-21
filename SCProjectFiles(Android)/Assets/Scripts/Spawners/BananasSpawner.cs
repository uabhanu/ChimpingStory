using UnityEngine;

public class BananasSpawner : MonoBehaviour
{
    GameObject m_collectedTiles;
    LandChimp m_landChimp;
    LevelCreator m_levelCreator;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(0.16f);

    [SerializeField] [Tooltip("This is max number of banana prefabs to spawn, Ask Bhanu for more info :)")] [Range(0 , 10)] int m_maxBananas; // Unable to use prefabs.Length for some reason
    [SerializeField] GameObject[] m_bananasPrefabs;

    public static int m_bananasCount = 0;

    void Start()
    {
        m_collectedTiles = GameObject.Find("Tiles");
        m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
        m_levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        Invoke("SpawnBananas" , 0.5f);
    }

    //TODO if necessary, try to use a delay between spawns without using Coroutines and increase max bananas in the inspector
    void SpawnBananas()
    {
        if(m_bananasCount < m_maxBananas && GameManager.m_currentScene == 1 && !m_landChimp.m_isSuper/* && LevelCreator.m_middleCounter > 2*/)
        {
            GameObject bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)] , transform.position , Quaternion.identity);
            bananas.transform.parent = m_collectedTiles.transform.Find("Bananas").transform;
            bananas.transform.position = new Vector2(bananas.transform.position.x , bananas.transform.position.y);
            m_bananasCount++;
            Invoke("SpawnBananas" , 5.5f);
        }
    }
}
