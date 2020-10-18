using SelfiePuss.Events;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Color _coinSpriteColour;
    [SerializeField] private int _scoreIncrementValue;
    [SerializeField] private List<CoinDataSO> _coinDataSOs;
    [SerializeField] private SpriteRenderer _coinRenderer;

    private void Start()
    {
        int i = Random.Range(0 , _coinDataSOs.Count);
        _coinSpriteColour = _coinDataSOs[i].m_CoinSpriteColour;
        _coinRenderer.color = _coinSpriteColour;
        _scoreIncrementValue = _coinDataSOs[i].m_ScoreIncrementValue;
        EventsManager.InvokeEvent(SelfiePussEvent.IncrementValueReceived , _scoreIncrementValue);
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
