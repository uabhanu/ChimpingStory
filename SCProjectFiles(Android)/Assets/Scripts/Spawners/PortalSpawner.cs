using System.Collections;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_gameLayer , m_portalObj;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(15.5f);

    [SerializeField] GameObject m_portalPrefab;

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_gameLayer = GameObject.Find("GameLayer");
		m_portalObj = GameObject.FindGameObjectWithTag("Portal");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return m_spawnRoutineDelay;
		m_portalObj = Instantiate(m_portalPrefab , transform.position , Quaternion.identity);
		m_portalObj.transform.parent = m_gameLayer.transform;
		StartCoroutine("SpawnRoutine");
	}
}
