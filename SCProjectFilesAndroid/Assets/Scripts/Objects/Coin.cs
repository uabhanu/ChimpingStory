using SelfiePuss.Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ScoreManagerSO _scoreManagerSO; //TODO Put this in ScoreManager Monobehaviour class and so on	

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            _scoreManagerSO.m_ScoreValue += 25;
            BhanuPrefs.SetHighScore(_scoreManagerSO.m_ScoreValue);
            EventsManager.InvokeEvent(SelfiePussEvent.ScoreChanged);
			EventsManager.InvokeEvent(SelfiePussEvent.CoinCollected);
            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
