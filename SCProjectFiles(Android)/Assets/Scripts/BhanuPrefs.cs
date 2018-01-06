using UnityEngine;

public class BhanuPrefs : MonoBehaviour
{
    const string HIGH_SCORE_KEY = "HighScore";
    const string GAME_TIME_KEY = "GameTime";

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static float GetGameTime()
    {
        if(PlayerPrefs.HasKey(GAME_TIME_KEY))
        {
            return PlayerPrefs.GetFloat(GAME_TIME_KEY);
        }

        return 0f;
    }

    public static int GetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }

        return 0;
    }

    public static void SetGameTime(float time)
    {
        PlayerPrefs.SetFloat(GAME_TIME_KEY , time);
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY , score);
    }
}