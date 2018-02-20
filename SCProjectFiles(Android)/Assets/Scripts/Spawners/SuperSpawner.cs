using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
	LandChimp m_landChimp;
	GameObject m_gameLayer , m_superObj;

    [SerializeField] GameObject m_superPrefab;

    void Start()
	{
        m_gameLayer = GameObject.Find("GameLayer");
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_superObj = GameObject.FindGameObjectWithTag("Super");
		Invoke("SpawnSuper" , 5.5f);
	}

	void SpawnSuper()
	{
		if(!m_landChimp.m_isSuper && ScoreManager.m_supersCount > 0)
		{
			m_superObj = Instantiate(m_superPrefab , transform.position , Quaternion.identity);
			m_superObj.transform.parent = m_gameLayer.transform;
        }

		Invoke("SpawnSuper" , 5.5f);
	}
}
