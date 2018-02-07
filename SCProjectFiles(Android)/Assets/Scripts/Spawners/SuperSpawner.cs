using System.Collections;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_gameLayer , m_superObj;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(5.5f);

    [SerializeField] GameObject m_superPrefab;

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_gameLayer = GameObject.Find("GameLayer");
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return m_spawnRoutineDelay;

		if(!m_chimpController.m_isSuper && ScoreManager.m_supersCount > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_gameLayer.transform;
        }

		StartCoroutine("SpawnRoutine");
	}
}
