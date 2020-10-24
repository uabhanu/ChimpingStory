using UnityEngine;

public class Meteor : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private MeteorDataSO _meteorDataSO;
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
        Instantiate(_explosionPrefab , transform.position , Quaternion.identity);
        Instantiate(_meteorDataSO.m_MeteorSmashedPointsPrefab , transform.position , Quaternion.identity);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
