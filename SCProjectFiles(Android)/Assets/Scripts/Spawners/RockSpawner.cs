using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	LandChimp m_landChimp;
	GameObject m_rockPrefab;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(0.5f);

	void Start()
	{
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_rockPrefab = Resources.Load("PF_Rock") as GameObject;
	}

	IEnumerator SpawnRoutine()
	{
		yield return m_spawnRoutineDelay;

		if(m_landChimp.m_isSuper)
		{
			Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
            StartCoroutine("SpawnRoutine");
		}
	}

    public void StartSpawnRoutine()
    {
        StartCoroutine("SpawnRoutine");
    }
}
