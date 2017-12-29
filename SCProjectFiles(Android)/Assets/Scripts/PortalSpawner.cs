using System.Collections;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_portalObj , m_tilePos;

	[SerializeField] float m_spawnTime;
    [SerializeField] GameObject[] m_portalPrefabs;

	void Reset()
	{
        m_spawnTime = 27.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_portalObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_portalObj == null && !m_chimpController.m_super)
		{
			m_portalObj = Instantiate(m_portalPrefabs[Random.Range(0 , m_portalPrefabs.Length - 1)] , transform.position , Quaternion.identity);
			m_portalObj.transform.parent = m_collectedTiles.transform.Find("Portal").transform;
			m_portalObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 6);
		}

		StartCoroutine("SpawnRoutine");
	}
}
