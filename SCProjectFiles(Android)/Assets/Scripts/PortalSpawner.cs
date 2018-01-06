using System.Collections;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_collectedTiles , m_portalObj;

	[SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_portalPrefab;

	void Reset()
	{
        m_spawnTime = 27.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_portalObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_portalObj == null && !m_chimpController.m_super)
		{
			m_portalObj = Instantiate(m_portalPrefab , transform.position , Quaternion.identity);
			m_portalObj.transform.parent = m_collectedTiles.transform.Find("Portal").transform;
			m_portalObj.transform.position = new Vector2(transform.position.x , m_portalObj.transform.position.y + Random.Range(0.5f , 1.5f));
		}

		StartCoroutine("SpawnRoutine");
	}
}
