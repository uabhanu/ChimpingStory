using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_superObj , m_superPrefab , m_tilePos;

	[SerializeField] float m_spawnTime;

	void Reset()
	{
		m_spawnTime = 80.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		m_superPrefab = Resources.Load("PF_Super") as GameObject;
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(m_superObj == null && !m_chimpController.m_super)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_collectedTiles.transform.Find("Goodies").transform;
			m_superObj.transform.position = new Vector2(transform.position.x , m_startUpPosY + 6);
		}

		StartCoroutine("SpawnRoutine");
	}
}
