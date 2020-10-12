using SelfiePuss.Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _scoreIncrementValue = 0;

    [SerializeField] private Color m_CoinSpriteColour;
    [SerializeField] private SpriteRenderer _coinRenderer;

    private void Start()
    {
        m_CoinSpriteColour = _coinRenderer.color;

        if(m_CoinSpriteColour.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000")
        {
            _scoreIncrementValue = 25;
            //Debug.Log("Start() Score Increment Value : " + _scoreIncrementValue);
        }

        else if(m_CoinSpriteColour.ToString() == "RGBA(0.000, 1.000, 0.000, 1.000)")
        {
            _scoreIncrementValue = 50;
            //Debug.Log("Start() Score Increment Value : " + _scoreIncrementValue);
        }

        else if(m_CoinSpriteColour.ToString() == "RGBA(0.686, 0.000, 0.000, 1.000)")
        {
            _scoreIncrementValue =  75;
            //Debug.Log("Start() Score Increment Value : " + _scoreIncrementValue);
        }
    }

    public int GetScoreIncrementValue()
    {
        Debug.Log("GetScoreIncrementValue() Score Increment Value : " + _scoreIncrementValue);
        return _scoreIncrementValue;
    }

    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.gameObject.tag.Equals("Player"))
        {
            EventsManager.InvokeEvent(SelfiePussEvent.ScoreChanged);
			EventsManager.InvokeEvent(SelfiePussEvent.CoinCollected);
            Destroy(gameObject); //TODO Object Pooling instead of Destroy
        }
    }
}
