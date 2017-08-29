using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    GameObject m_pillarObj;

    [SerializeField] float m_checkTime;

    [SerializeField] GameObject m_pillarPrefab;

	void Start()
    {
		StartCoroutine("SpawnRoutine");
	}
	
	IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_checkTime);

        m_pillarObj = GameObject.Find("PF_Pillar(Clone)");

        if(m_pillarObj == null)
        {
            m_pillarObj = Instantiate(m_pillarPrefab , transform.position , Quaternion.identity);
        }

        StartCoroutine("SpawnRoutine");
    
    }
}
