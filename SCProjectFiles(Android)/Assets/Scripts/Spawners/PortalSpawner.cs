using System.Collections;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
    const float m_tileWidth = 1.25f;
    float m_startUpPosY;
	GameObject m_collectedTiles , m_gameLayer , m_portalObj;
    int m_heightLevel = 0;

	[SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_portalPrefab , m_tilePos;

	void Reset()
	{
        m_spawnTime = 16.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_gameLayer = GameObject.Find("GameLayer");
		m_portalObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
        m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		m_portalObj = Instantiate(m_portalPrefab , transform.position , Quaternion.identity);
		m_portalObj.transform.parent = m_gameLayer.transform;

		if(m_tilePos != null) 
		{
			m_portalObj.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 2f)));	
		} 
		else 
		{
			Debug.LogWarning("Sir Bhanu, I don't think Super Object exists anymore");
		}

		StartCoroutine("SpawnRoutine");
	}
}
