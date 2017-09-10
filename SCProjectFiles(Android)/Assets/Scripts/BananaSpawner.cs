using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    GameObject m_banana;
    int m_randomValue;

    [SerializeField] float m_spawnTime;

    [SerializeField] GameObject m_bananaPrefab;

	void Start()
    {
		StartCoroutine("SpawnRoutine");
	}
	
	IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        m_banana = GameObject.FindGameObjectWithTag("Banana");

        if(m_banana == null)
        {
            m_banana = Instantiate(m_bananaPrefab , transform.position , Quaternion.identity);
        }

        StartCoroutine("SpawnRoutine");
    }
}
