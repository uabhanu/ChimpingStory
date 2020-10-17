using UnityEngine;

[CreateAssetMenu]
public class ScoreManagerSO : ScriptableObject
{
    [SerializeField] private int _scoreValue;

    public int GetScoreValue()
    {
        return _scoreValue;
    }

    public void SetScoreValue(int scoreValue)
    {
        _scoreValue = scoreValue;
    }
}
