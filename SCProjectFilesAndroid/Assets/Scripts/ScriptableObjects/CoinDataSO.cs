using UnityEngine;

[CreateAssetMenu]
public class CoinDataSO : ScriptableObject
{
    public Color m_CoinSpriteColour;
    public int m_ScoreIncrementValue;
    public SpriteRenderer m_CoinRenderer;
}
