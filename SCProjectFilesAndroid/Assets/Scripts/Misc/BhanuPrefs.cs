using UnityEngine;

public class BhanuPrefs : MonoBehaviour
{
    const string CHIMPIONSHIPS_COUNT_KEY = "NumberOfTimesChimpion";
    const string CURRENT_CHIMPIONSHIP_STATUS_KEY = "CurrentChimpionshipStatus";
    const string FIRST_TIME_IAP_TUTORIAL_KEY = "FirstTimeIAPTutorial";
    const string FIRST_TIME_JUMP_TUTORIAL_KEY = "FirstTimeJumpTutorial";
    const string FIRST_TIME_SLIDE_TUTORIAL_KEY = "FirstTimeSlideTutorial";
    const string FIRST_TIME_UI_BUTTONS_TUTORIAL_KEY = "FirstTimeUIButtonsTutorial";
    const string FIRST_TIME_WATER_LEVEL_TUTORIAL_KEY = "FirstTimeWaterLevelTutorial";
    const string GAME_DIFFICULTY_KEY = "GameDifficulty";
    const string HIGH_SCORE_KEY = "HighScore";
    const string PLAYER_LEVEL_KEY = "PlayerLevel";
    const string POLAROIDS_COUNT_KEY = "PolaroidsCount";
    const string PORTAL_PICKED_UP_KEY = "PortalPickedUp";
    const string PORTAL_RESPAWN_TIMER_KEY = "PortalRespawnTimer";
    const string SOUNDS_STATUS_KEY = "SoundsStatus";
    const string SUPER_PICKED_UP_KEY = "SuperPickedUp";

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static void DeleteScore()
    {
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
    }

    public static int GetChimpionshipsCount()
    {
        if(PlayerPrefs.HasKey(CHIMPIONSHIPS_COUNT_KEY))
        {
            return PlayerPrefs.GetInt(CHIMPIONSHIPS_COUNT_KEY);
        }

        return 0;
    }

    public static int GetCurrentChimpionshipStatus()
    {
        if(PlayerPrefs.HasKey(CURRENT_CHIMPIONSHIP_STATUS_KEY))
        {
            return PlayerPrefs.GetInt(CURRENT_CHIMPIONSHIP_STATUS_KEY);
        }

        return 0;
    }

    public static int GetFirstTimeIAPTutorialStatus()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_IAP_TUTORIAL_KEY))
        {
            return PlayerPrefs.GetInt(FIRST_TIME_IAP_TUTORIAL_KEY);
        }

        return 0;
    }

    public static int GetFirstTimeJumpTutorialStatus()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_JUMP_TUTORIAL_KEY))
        {
            return PlayerPrefs.GetInt(FIRST_TIME_JUMP_TUTORIAL_KEY);
        }

        return 0;
    }

    public static int GetFirstTimeSlideTutorialStatus()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_SLIDE_TUTORIAL_KEY))
        {
            return PlayerPrefs.GetInt(FIRST_TIME_SLIDE_TUTORIAL_KEY);
        }

        return 0;
    }

    public static int GetFirstTimeUIButtonsTutorialStatus()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_UI_BUTTONS_TUTORIAL_KEY))
        {
            return PlayerPrefs.GetInt(FIRST_TIME_UI_BUTTONS_TUTORIAL_KEY);
        }

        return 0;
    }

    public static int GetFirstTimeWaterLevelTutorialStatus()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_WATER_LEVEL_TUTORIAL_KEY))
        {
            return PlayerPrefs.GetInt(FIRST_TIME_WATER_LEVEL_TUTORIAL_KEY);
        }

        return 0;
    }

    public static int GetGameDifficultyStatus()
    {
        if(PlayerPrefs.HasKey(GAME_DIFFICULTY_KEY))
        {
            return PlayerPrefs.GetInt(GAME_DIFFICULTY_KEY);
        }

        return 0;
    }
		
    public static int GetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }

        return 0;
    }

    public static int GetPlayerLevel()
    {
        if(PlayerPrefs.HasKey(PLAYER_LEVEL_KEY))
        {
            return PlayerPrefs.GetInt(PLAYER_LEVEL_KEY);
        }

        return 0;
    }

    public static int GetPolaroidsCount()
    {
        if(PlayerPrefs.HasKey(POLAROIDS_COUNT_KEY))
        {
            return PlayerPrefs.GetInt(POLAROIDS_COUNT_KEY);
        }

        return 0;
    }

    public static int GetPortalPickedUp()
    {
        if(PlayerPrefs.HasKey(PORTAL_PICKED_UP_KEY))
        {
            return PlayerPrefs.GetInt(PORTAL_PICKED_UP_KEY);
        }

        return 0;
    }

    public static float GetPortalRespawnTimer()
    {
        if(PlayerPrefs.HasKey(PORTAL_RESPAWN_TIMER_KEY))
        {
            return PlayerPrefs.GetFloat(PORTAL_RESPAWN_TIMER_KEY);
        }

        return 0.0f;
    }

    public static int GetSoundsMuteStatus()
    {
        if(PlayerPrefs.HasKey(SOUNDS_STATUS_KEY))
        {
            return PlayerPrefs.GetInt(SOUNDS_STATUS_KEY);
        }

        return 0;
    }

    public static int GetSuperPickedUp()
    {
        if(PlayerPrefs.HasKey(SUPER_PICKED_UP_KEY))
        {
            return PlayerPrefs.GetInt(SUPER_PICKED_UP_KEY);
        }

        return 0;
    }

    public static void SetChimpionshipsCount(int chimpionshipsCount)
    {
        PlayerPrefs.SetInt(CHIMPIONSHIPS_COUNT_KEY , chimpionshipsCount);
    }

    public static void SetCurrentChimpionshipStatus(int chimpionshipStatus)
    {
        PlayerPrefs.SetInt(CURRENT_CHIMPIONSHIP_STATUS_KEY , chimpionshipStatus);
    }

    public static void SetFirstTimeIAPTutorialStatus(int iapTutStatus)
    {
        PlayerPrefs.SetInt(FIRST_TIME_IAP_TUTORIAL_KEY , iapTutStatus);
    }

    public static void SetFirstTimeJumpTutorialStatus(int jumpTutStatus)
    {
        PlayerPrefs.SetInt(FIRST_TIME_JUMP_TUTORIAL_KEY , jumpTutStatus);
    }

    public static void SetFirstTimeSlideTutorialStatus(int slideTutStatus)
    {
        PlayerPrefs.SetInt(FIRST_TIME_SLIDE_TUTORIAL_KEY , slideTutStatus);
    }

    public static void SetFirstTimeUIButtonsTutorialStatus(int uiButsTutStatus)
    {
        PlayerPrefs.SetInt(FIRST_TIME_UI_BUTTONS_TUTORIAL_KEY , uiButsTutStatus);
    }

    public static void SetFirstTimeWaterLevelTutorialStatus(int waterLevelStatus)
    {
        PlayerPrefs.SetInt(FIRST_TIME_WATER_LEVEL_TUTORIAL_KEY , waterLevelStatus);
    }

    public static void SetGameDifficultyStatus(int gameDifficultyStatus)
    {
        PlayerPrefs.SetInt(GAME_DIFFICULTY_KEY , gameDifficultyStatus);
    }

    public static void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY , highScore);
    }

    public static void SetPlayerLevel(int playerLevel)
    {
        PlayerPrefs.SetInt(PLAYER_LEVEL_KEY , playerLevel);
    }

    public static void SetPolaroidsCount(int polaroidsCount)
    {
        PlayerPrefs.SetInt(POLAROIDS_COUNT_KEY , polaroidsCount);
    }

    public static void SetPortalPickedUp(int portalPickedUp)
    {
        PlayerPrefs.SetInt(PORTAL_PICKED_UP_KEY , portalPickedUp);
    }

    public static void SetPortalRespawnTimer(float portalRespawnTimer)
    {
        PlayerPrefs.SetFloat(PORTAL_PICKED_UP_KEY , portalRespawnTimer);
    }

    public static void SetSoundsMuteStatus(int mute)
    {
        PlayerPrefs.SetInt(SOUNDS_STATUS_KEY , mute);
    }

    public static void SetSuperPickedUp(int superPickedUp)
    {
        PlayerPrefs.SetInt(SUPER_PICKED_UP_KEY , superPickedUp);
    }
}