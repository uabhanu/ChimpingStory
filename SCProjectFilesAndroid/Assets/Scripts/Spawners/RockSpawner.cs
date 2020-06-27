using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	LandPuss m_landChimp;
	GameObject m_rockPrefab;

	void Start()
	{
		m_landChimp = GameObject.Find("LandPuss").GetComponent<LandPuss>();
		m_rockPrefab = Resources.Load("PF_SuperModeRock") as GameObject;
	}

	void SpawnRock()
	{
		if(m_landChimp.m_isSuper)
		{
			Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
            Invoke("SpawnRock" , 0.8f);
		}
	}

    public void StartSpawnRoutine()
    {
        Invoke("SpawnRock" , 0.8f);
    }
}
