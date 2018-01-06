using UnityEngine;

public class BhanuPrefs : MonoBehaviour
{
    const string HIGH_SCORE_KEY = "HighScore";

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static int GetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }

        return 0;
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY , score);
    }
}