using SelfiePuss.Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private string _coinColourString , _coinSpriteColourString;
    [SerializeField] private Color _coinSpriteColour;
    [SerializeField] private SpriteRenderer _coinRenderer;

    private void Start()
    {
        _coinSpriteColour = _coinRenderer.color;
        _coinSpriteColourString = _coinSpriteColour.ToString();

        switch(_coinSpriteColourString)
        {
            case "RGBA(1.000, 1.000, 1.000, 1.000)" :
                _coinColourString = "White";
            break;

            case "RGBA(0.000, 1.000, 0.000, 1.000)" :
                _coinColourString = "Green";
            break;

            case "RGBA(0.686, 0.000, 0.000, 1.000)" :
                _coinColourString = "Red";
            break;
        }
    }

    public string GetCoinColourString()
    {
        return _coinColourString; //TODO This is not getting the value from the start method for some reason so debugging WIP
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
