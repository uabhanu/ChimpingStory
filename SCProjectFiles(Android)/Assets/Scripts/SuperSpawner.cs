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
        m_spawnTime = 55f;
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

		if(m_superObj == null && !m_chimpController.m_super && m_chimpController.m_superPickUpsAvailable > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_collectedTiles.transform.Find("Super").transform;
            m_superObj.transform.position = new Vector2(transform.position.x , m_superObj.transform.position.y + Random.Range(0.5f , 1.5f));
        }

		StartCoroutine("SpawnRoutine");
	}
}
