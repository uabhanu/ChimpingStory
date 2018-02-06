using System.Collections;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_gameLayer , m_superObj;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_superPrefab;

    void Reset()
    {
        m_spawnTime = 5.5f;    
    }

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_gameLayer = GameObject.Find("GameLayer");
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);

		if(!m_chimpController.m_super && ScoreManager.m_supersCount > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_gameLayer.transform;
        }

		StartCoroutine("SpawnRoutine");
	}
}
