using System.Collections;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	ChimpController m_chimpController;
	GameObject m_gameLayer , m_portalObj;

    [SerializeField] float m_spawnTime;
    [SerializeField] GameObject m_portalPrefab;

    void Reset()
    {
        m_spawnTime = 15.5f;    
    }

    void Start()
	{
		m_chimpController = FindObjectOfType<ChimpController>();
        m_gameLayer = GameObject.Find("GameLayer");
		m_portalObj = GameObject.FindGameObjectWithTag("Portal");
		StartCoroutine("SpawnRoutine");
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(m_spawnTime);
		m_portalObj = Instantiate(m_portalPrefab , transform.position , Quaternion.identity);
		m_portalObj.transform.parent = m_gameLayer.transform;
		StartCoroutine("SpawnRoutine");
	}
}
