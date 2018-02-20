using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	LandChimp m_landChimp;
	GameObject m_rockPrefab;

	void Start()
	{
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_rockPrefab = Resources.Load("PF_Rock") as GameObject;
	}

	void SpawnRock()
	{
		if(m_landChimp.m_isSuper)
		{
			Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
            Invoke("SpawnRock" , 0.5f);
		}
	}

    public void StartSpawnRoutine()
    {
        Invoke("SpawnRock" , 0.5f);
    }
}
