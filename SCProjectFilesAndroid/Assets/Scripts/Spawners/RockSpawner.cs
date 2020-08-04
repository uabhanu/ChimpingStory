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
        float randomYPosition = Random.Range(transform.position.y - 2.5f , transform.position.y + 2.5f);
		Instantiate(m_rockPrefab , new Vector3(transform.position.x , Mathf.Clamp(randomYPosition , -1.47f , 4.53f) , transform.position.z) , Quaternion.identity);
    }
}
