using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	[SerializeField] GameObject m_rockPrefab;

    private void Update()
    {
        if(Time.timeScale == 0f)
		{
			return;
		}

        SpawnRock();
    }

    void SpawnRock()
	{
		Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
    }
}
