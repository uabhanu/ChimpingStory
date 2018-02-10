using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	LandChimp m_landChimp;
	float m_startUpPosY;
	GameObject m_collectedTiles , m_rockPrefab , m_tilePos;
    WaitForSeconds m_spawnRoutineDelay = new WaitForSeconds(0.5f);

	void Start()
	{
		m_landChimp = GameObject.Find("LandChimp").GetComponent<LandChimp>();
		m_rockPrefab = Resources.Load("PF_Rock") as GameObject;
		m_collectedTiles = GameObject.Find("Tiles");
		m_tilePos = GameObject.Find("StartTilePosition");
		m_startUpPosY = m_tilePos.transform.position.y;
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
