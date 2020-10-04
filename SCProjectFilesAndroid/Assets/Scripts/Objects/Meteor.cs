using SelfiePuss.Events;
using UnityEngine;

public class Meteor : MonoBehaviour 
{
    //TODO Write the same script for Super Object
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private ScoreManagerSO _scoreManagerObject;
    [SerializeField] private SoundManagerSO _soundManagerObject;
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            EventsManager.InvokeEvent(SelfiePussEvent.MeteorExplosion);
            SpawnExplosion();
        }
    }

	void SpawnExplosion()
	{
        Explosion.m_explosionType = "Meteor";
        Instantiate(m_explosionPrefab , transform.position , Quaternion.identity);
		_scoreManagerObject.m_ScoreValue += 100;
		BhanuPrefs.SetHighScore(_scoreManagerObject.m_ScoreValue);
        EventsManager.InvokeEvent(SelfiePussEvent.ScoreChanged);
        Destroy(gameObject); //TODO Object Pooling instead of Destroy
	}
}
