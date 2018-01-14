using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_rockPrefab , m_tilePos;

	[SerializeField] float m_spawnTime;
    [SerializeField] [Tooltip("Choose number of rocks you want to spawn, ask Bhanu for more info")] [Range(0 , 50)] int m_maxRocks;

	public static int m_rocksSpawnCount = 0;

	void Reset()
	{
		m_spawnTime = 0.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_rockPrefab = Resources.Load("PF_Rock") as GameObject;
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_rocksSpawnCount < m_maxRocks && m_chimpController.m_super)
		{
			GameObject rockObj = Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
			rockObj.transform.position = new Vector2(transform.position.x , Random.Range(-0.99f , 4.44f));
			m_rocksSpawnCount++;
		}

		StartCoroutine("SpawnRoutine");
	}
}
