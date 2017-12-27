using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_powerUpObj , m_tilePos;

	[SerializeField] float m_spawnTime;
    [SerializeField] GameObject[] m_powerUpPrefabs;

	void Reset()
	{
        m_spawnTime = 27.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_powerUpObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_powerUpObj == null && !m_chimpController.m_super && m_chimpController.m_superPickUpsAvailable > 0)
		{
			m_powerUpObj = Instantiate(m_powerUpPrefabs[Random.Range(0 , 3)] , transform.position , Quaternion.identity);
			m_powerUpObj.transform.parent = m_collectedTiles.transform.Find("PowerUp").transform;
			m_powerUpObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 6);
		}

		StartCoroutine("SpawnRoutine");
	}
}
