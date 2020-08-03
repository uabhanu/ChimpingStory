using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    private int nextCheck = 1;

	[SerializeField] GameObject m_rockPrefab;

    private void Update()
    {
        if(Time.timeScale == 0f)
		{
			return;
		}

        if(Time.time >= nextCheck)
        {
            nextCheck = Mathf.FloorToInt(Time.time) + nextCheck;
            SpawnRock();
        }
    }

    void SpawnRock()
	{
		Instantiate(m_rockPrefab , transform.position , Quaternion.identity);
    }
}
