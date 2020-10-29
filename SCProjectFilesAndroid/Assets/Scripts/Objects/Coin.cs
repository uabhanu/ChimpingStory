using SelfiePuss.Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinDataSO _coinDataSO;

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
			EventsManager.InvokeEvent(SelfiePussEvent.CoinCollected , _coinDataSO.GetScoreIncrementValue());
            EventsManager.InvokeEvent(SelfiePussEvent.SpawnCoinPointsPrefab , transform.localPosition);
            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }    
}
