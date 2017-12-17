using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	float m_startUpPosY;
	GameObject m_rockObj , m_rockPrefab , m_collectedTiles , m_tilePos;

	[SerializeField] float m_spawnTime;

	void Reset()
	{
		m_spawnTime = 39.5f;
	}

	void Start()
	{
		m_rockObj = GameObject.FindGameObjectWithTag("Rock");
		m_rockPrefab = Resources.Load("PF_Rock") as GameObject;
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_rockObj == null)
		{
			m_rockObj = Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
			m_rockObj.transform.parent = m_collectedTiles.transform.Find("Troubles").transform;
			m_rockObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 5);
		}

		StartCoroutine("SpawnRoutine");
	}
}
