using System.Collections;
using UnityEngine;

public class PickUpsSpawner : MonoBehaviour 
{
    GameObject m_pickUpComboToSpawn;
    int m_index;

    [SerializeField] GameObject[] m_pickUpComboPrefabs;

	void Start() 
	{
		StartCoroutine("Spawning");	
	}

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(1.5f);

		
		m_index = Random.Range(0 , m_pickUpComboPrefabs.Length);
        m_pickUpComboToSpawn = GameObject.FindGameObjectWithTag("PickUpCombo");

		if(m_pickUpComboToSpawn == null)
		{
            m_pickUpComboToSpawn = Instantiate(m_pickUpComboPrefabs[m_index] , new Vector3(15f , 0f , 0f) , transform.rotation) as GameObject;
		}
		
		StartCoroutine("Spawning");
	}
}
