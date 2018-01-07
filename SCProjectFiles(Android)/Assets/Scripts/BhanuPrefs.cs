using UnityEngine;

public class BhanuPrefs : MonoBehaviour
{
    const string HIGH_SCORE_KEY = "HighScore";
    const string GAME_TIME_KEY = "GameTime";
    const string SUPERS_KEY = "Supers";

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

    public static float GetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            return PlayerPrefs.GetFloat(HIGH_SCORE_KEY);
        }

        return 0;
    }

    public static int GetSupers()
    {
        if(PlayerPrefs.HasKey(SUPERS_KEY))
        {
            return PlayerPrefs.GetInt(SUPERS_KEY);
        }

        return 0;
    }

    public static void SetGameTime(float time)
    {
        PlayerPrefs.SetFloat(GAME_TIME_KEY , time);
    }

    public static void SetHighScore(float score)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_KEY , score);
    }

    public static void SetSupers(int supers)
    {
        PlayerPrefs.SetInt(SUPERS_KEY , supers);
    }
}