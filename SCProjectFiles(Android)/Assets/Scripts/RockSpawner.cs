using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_rockObj , m_rockPrefab , m_tilePos;

	[SerializeField] float m_spawnTime;

	void Reset()
	{
		m_spawnTime = 3.5f;
	}

	void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
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

		if(m_rockObj == null && m_chimpController.m_super)
		{
			m_rockObj = Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
			m_rockObj.transform.position = new Vector2(transform.position.x , Random.Range(0.86f , 4.44f));
		}

		StartCoroutine("SpawnRoutine");
	}
}
