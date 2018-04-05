using UnityEngine;

public class BhanuPrefs : MonoBehaviour
{
    const string BANANAS_COLLECTED_KEY = "BananasCollected";
    const string GAME_TIME_KEY = "GameTime";
    const string HIGH_SCORE_KEY = "HighScore";
    const string SOUNDS_STATUS_KEY = "SoundsStatus";
    const string SUPERS_KEY = "Supers";

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static int GetBananasCollected()
    {
        if(PlayerPrefs.HasKey(BANANAS_COLLECTED_KEY))
        {
            return PlayerPrefs.GetInt(BANANAS_COLLECTED_KEY);
        }

        return 0;
    }
		
    public static float GetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            return PlayerPrefs.GetFloat(HIGH_SCORE_KEY);
        }

        return 0;
    }

    public static int GetSoundsStatus()
    {
        if(PlayerPrefs.HasKey(SOUNDS_STATUS_KEY))
        {
            return PlayerPrefs.GetInt(SOUNDS_STATUS_KEY);
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

    public static void SetBananasCollected(int bananas)
    {
        PlayerPrefs.SetInt(BANANAS_COLLECTED_KEY , bananas);
    }
	
    public static void SetHighScore(float score)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_KEY , score);
    }

    public static void SetSoundsStatus(int mute)
    {
        PlayerPrefs.SetInt(SOUNDS_STATUS_KEY , mute);
    }

    public static void SetSupers(int supers)
    {
        PlayerPrefs.SetInt(SUPERS_KEY , supers);
    }
}