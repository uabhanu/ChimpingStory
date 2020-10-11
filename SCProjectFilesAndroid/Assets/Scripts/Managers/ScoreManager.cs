using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private string _coinColourString;

    [SerializeField] private Coin _coinReference;
    [SerializeField] private ScoreManagerSO _scoreManagerSO;
    [SerializeField] private Text _highScoreValueText;

    private void Start()
    {
        _scoreManagerSO.m_ScoreValue = BhanuPrefs.GetHighScore();
        _highScoreValueText.text = _scoreManagerSO.m_ScoreValue.ToString();
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void OnScoreChanged()
    {
        _coinColourString = _coinReference.GetCoinColourString();

        switch(_coinColourString)
        {
            case "Green":
                _scoreManagerSO.m_ScoreValue += 50;
            break;

            case "Red":
                _scoreManagerSO.m_ScoreValue += 75;
            break;

            case "White":
                _scoreManagerSO.m_ScoreValue += 25;
            break;
        }

        BhanuPrefs.SetHighScore(_scoreManagerSO.m_ScoreValue);
        _highScoreValueText.text = _scoreManagerSO.m_ScoreValue.ToString();
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.ScoreChanged , OnScoreChanged);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.ScoreChanged , OnScoreChanged);
    }
}
