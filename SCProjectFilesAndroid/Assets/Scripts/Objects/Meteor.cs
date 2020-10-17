using UnityEngine;

public class Meteor : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private SoundManagerSO _soundManagerObject;
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            SpawnExplosion();
        }
    }

	void SpawnExplosion()
	{
        Explosion.m_explosionType = "Meteor";
        Instantiate(m_explosionPrefab , transform.position , Quaternion.identity);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
