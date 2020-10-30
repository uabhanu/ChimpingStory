using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CoinDataSO : ScriptableObject
{
    [SerializeField] private GameObject _coinPointsPrefab;
    [SerializeField] private int _scoreIncrementValue;

    public GameObject GetCoinPointsPrefab()
    {
        return _coinPointsPrefab;
    }

    public int GetScoreIncrementValue()
    {
        return _scoreIncrementValue;
    }
}
