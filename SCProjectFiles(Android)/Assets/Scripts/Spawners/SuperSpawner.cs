using System.Collections;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	const float m_tileWidth = 1.25f;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_gameLayer , m_superObj;
	int m_heightLevel = 0;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_superPrefab , m_tilePos;

    void Reset()
    {
        m_spawnTime = 15.5f;    
    }

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		m_collectedTiles = GameObject.Find("Tiles");
		m_gameLayer = GameObject.Find("GameLayer");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(!m_chimpController.m_super && ScoreManager.m_supersCount > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_gameLayer.transform;

			if(m_tilePos != null) 
			{
				m_superObj.transform.position = new Vector2(m_tilePos.transform.position.x + m_tileWidth * 3.7f , m_startUpPosY + (m_heightLevel * m_tileWidth + (m_tileWidth * 2f)));	
			} 
			else 
			{
				Debug.LogWarning("Sir Bhanu, I don't think Super Object exists anymore");
			}
        }

		StartCoroutine("SpawnRoutine");
	}
}
