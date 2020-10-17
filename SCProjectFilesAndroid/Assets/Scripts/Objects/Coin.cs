using SelfiePuss.Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _scoreIncrementValue;

    public int GetScoreIncrementValue()
    {
        return _scoreIncrementValue;
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
			EventsManager.InvokeEvent(SelfiePussEvent.CoinCollected);
            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
