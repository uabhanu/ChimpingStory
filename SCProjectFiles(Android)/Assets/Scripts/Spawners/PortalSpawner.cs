using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	GameObject m_gameLayer , m_portalObj;

    [SerializeField] GameObject m_portalPrefab;

    void Start()
	{
        m_gameLayer = GameObject.Find("GameLayer");
		m_portalObj = GameObject.FindGameObjectWithTag("Portal");
		Invoke("SpawnPortal" , 7.5f);
	}

	void SpawnPortal()
	{
		m_portalObj = Instantiate(m_portalPrefab , transform.position , Quaternion.identity);
		m_portalObj.transform.parent = m_gameLayer.transform;
		Invoke("SpawnPortal" , 7.5f);
	}
}
