using System.Collections;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_collectedTiles , m_superObj;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_superPrefab;

    void Reset()
    {
        m_spawnTime = 15.5f;    
    }

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(!m_chimpController.m_super && ScoreManager.m_supersCount > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_collectedTiles.transform.Find("Super").transform;
            m_superObj.transform.position = new Vector2(transform.position.x , m_superObj.transform.position.y + Random.Range(0.5f , 1.25f));
        }

		StartCoroutine("SpawnRoutine");
	}
}
