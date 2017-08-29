using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
	GameObject m_holeObj;
    int m_index;

    [SerializeField] float m_checkTime;

    [SerializeField] GameObject[] m_holePrefabs;

	void Start()
    {
		StartCoroutine("SpawnRoutine");
	}
	
	IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_checkTime);

        m_index = Random.Range(0 , m_holePrefabs.Length);

        m_holeObj = GameObject.FindGameObjectWithTag("Hole");

        if(m_holeObj == null)
        {
            m_holeObj = Instantiate(m_holePrefabs[m_index] , transform.position , Quaternion.identity);
        }

        StartCoroutine("SpawnRoutine");
    
    }
}
