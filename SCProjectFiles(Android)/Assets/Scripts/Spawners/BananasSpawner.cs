using System.Collections;
using UnityEngine;

public class BananasSpawner : MonoBehaviour
{
    ChimpController m_chimpController;
    GameObject m_collectedTiles;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(3.5f);

    [SerializeField] [Tooltip("This is max number of banana prefabs to spawn at once, Ask Bhanu for more info :)")] [Range(0 , 10)] int m_maxBananas; // Unable to use prefabs.Length for some reason
    [SerializeField] GameObject[] m_bananasPrefabs;

    public static int m_bananasCount = 0;

    void Start()
    {
        m_chimpController = FindObjectOfType<ChimpController>();
        m_collectedTiles = GameObject.Find("Tiles");
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return m_spawnRoutineDelay;

        if(m_bananasCount < m_maxBananas)
        {
            GameObject bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)] , transform.position , Quaternion.identity);
            bananas.transform.parent = m_collectedTiles.transform.Find("Bananas").transform;
            bananas.transform.position = new Vector2(transform.position.x + 2f , bananas.transform.position.y - 0.5f);
            m_bananasCount++;
        }

        StartCoroutine("SpawnRoutine");
    }
}
