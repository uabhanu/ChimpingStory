using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SocialmediaManager : MonoBehaviour 
{
    bool _bGPGsAchievementsButtonTapped , _bGPGsLogInButtonTapped;
    GameManager _gameManager;
	Image _gpgsMenuImage , _gpgsLeaderboardNotLoggedInOKButtonImage;
    int _currentScene;
    IScore _highScore;

    static string _leaderboardID = "CgkIia2_r44YEAIQAQ"; //String name doesn't work so use this unique key, same for achievements
    static Text _gpgsLeaderboardLogInCheckText;

    //[SerializeField] Image _oneSignalMenuImage , _oneSignalPushAcceptButtonImage , _oneSignalPushCancelButtonImage;
    [SerializeField] Text _gpgsLeaderboardTestText/* , _oneSignalPushText*/;

    public static bool b_gpgsAchievementsButtonAvailable , b_gpgsLeaderboardButtonAvailable , b_isGPGsLeaderboardTestMode;
    public static bool b_isGPGsAchievementsTestMode , b_isGPGsLogInTestMode , b_gpgsLoggedIn , b_isOneSignalTestMode;
    public static Button m_gpgsAchievementsButton , m_gpgsLeaderboardButton;
    public static GameObject m_gpgsAchievementsButtonObj , m_gpgsLeaderboardButtonObj;
    public static Image m_gpgsAchievementsButtonImage , m_gpgsLeaderboardButtonImage , m_gpgsLeaderboardTestGetButtonImage , m_gpgsLeaderboardTestMenuImage;
    public static Image m_gpgsLeaderboardTestSetButtonImage , m_gpgsLogInButtonImage , m_gpgsProfilePicImage , m_gpgsProfilePicMaskImage , m_gpgsRateButtonImage;
    public static int m_playerRank;
    public static Text m_gpgsAchievementsTestText , m_gpgsLogInTestText , m_gpgsUsernameText , m_noInternetText, m_noProfilePicText, m_noUsernameText , m_oneSignalText;

    public Sprite[] m_gpgsAchievementsButtonSprites , m_gpgsLeaderboardButtonSprites;

    void Start()
	{
        //b_isGPGsAchievementsTestMode = true;
        //b_isGPGsLeaderboardTestMode = true; //TODO Remove this after testing is finished
        //b_isGPGsLogInTestMode = true;
        //b_isOneSignalTestMode = true;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        OneSignal.StartInit("4aa7d0ba-0abb-47ca-baef-51e1fdf67ef8").HandleNotificationOpened(OneSignalHandleNotificationOpened).EndInit();
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        OneSignal.permissionObserver += OneSignalPermissionObserver;

        if(_currentScene == 0)
        {
            m_gpgsLogInButtonImage = GameObject.Find("GPGsLogInButton").GetComponent<Image>();
            m_gpgsLogInTestText = GameObject.Find("GPGsLogInTestText").GetComponent<Text>();
            m_gpgsProfilePicImage = GameObject.Find("GPGsProfilePicImage").GetComponent<Image>();
            m_gpgsProfilePicMaskImage = GameObject.Find("GPGsProfilePicMask").GetComponent<Image>();
            m_gpgsRateButtonImage = GameObject.Find("GPGsRateButton").GetComponent<Image>();
            m_gpgsUsernameText = GameObject.Find("GPGsUsernameText").GetComponent<Text>();
            m_noInternetText = GameObject.Find("NoInternetText").GetComponent<Text>();
            m_noProfilePicText = GameObject.Find("NoProfilePicText").GetComponent<Text>();
            m_noUsernameText = GameObject.Find("NoUsernameText").GetComponent<Text>();
            m_oneSignalText = GameObject.Find("OneSignalText").GetComponent<Text>();
            GooglePlayGamesInit();
            Invoke("GooglePlayGamesLogInCheck" , 0.2f);

            if(b_isGPGsLogInTestMode)
            {
                m_gpgsLogInTestText.enabled = true;
            }

            if(b_isOneSignalTestMode)
            {
                m_oneSignalText.enabled = true;
            }
        }

        if(_currentScene > 0)
        {
            m_gpgsAchievementsButtonObj = GameObject.Find("GPGsAchievementsButton");
            m_gpgsAchievementsButton = m_gpgsAchievementsButtonObj.GetComponent<Button>();
            m_gpgsAchievementsButtonImage = m_gpgsAchievementsButtonObj.GetComponent<Image>();
            m_gpgsAchievementsTestText = GameObject.Find("AchievementsTestText").GetComponent<Text>();
            m_gpgsLeaderboardButtonObj = GameObject.Find("GPGsLeaderboardButton");
            m_gpgsLeaderboardButton = m_gpgsLeaderboardButtonObj.GetComponent<Button>();
            m_gpgsLeaderboardButtonImage = m_gpgsLeaderboardButtonObj.GetComponent<Image>();
            _gpgsLeaderboardNotLoggedInOKButtonImage = GameObject.Find("NotLoggedInOKButton").GetComponent<Image>();
            m_gpgsLeaderboardTestMenuImage = GameObject.Find("PF_GPGsLeaderboardTestMenu").GetComponent<Image>();
            m_gpgsLeaderboardTestGetButtonImage = GameObject.Find("TestGetButton").GetComponent<Image>();
            m_gpgsLeaderboardTestSetButtonImage = GameObject.Find("TestSetButton").GetComponent<Image>();
            _gpgsLeaderboardLogInCheckText = GameObject.Find("LogInCheckText").GetComponent<Text>();
            _gpgsMenuImage = GameObject.Find("PF_GPGsMenu").GetComponent<Image>();
        }
    }

    public void GooglePlayGamesAchievements(string achievementID)
    {
        PlayGamesPlatform.Instance.ReportProgress(achievementID , 100f , (bool success) => {} );
    }

    public void GooglePlayGamesAchievementsButton()
    {
        _bGPGsAchievementsButtonTapped = true;

        if(b_gpgsAchievementsButtonAvailable)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            GameManager.m_pauseMenuObj.SetActive(false);
            _gpgsLeaderboardNotLoggedInOKButtonImage.enabled = true;
            _gpgsLeaderboardLogInCheckText.enabled = true;
            _gpgsMenuImage.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void GooglePlayGamesIncrementalAchievements(string achievementID , int steps)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(achievementID , steps , (bool success) => {} );
    }

    void GooglePlayGamesInit()
    {
        _bGPGsLogInButtonTapped = false;
        PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(clientConfig);
        PlayGamesPlatform.Activate();
    }

    public void GooglePlayGamesLeaderboardButton()
    {
        _gameManager.ChimpionshipBelt();
        GooglePlayGamesLeaderboardPlayerRank();
        GooglePlayGamesLeaderboardUpdate();
        _gameManager.m_polaroidImage.enabled = false;
        GameManager.m_polaroidsCountText.enabled = false;

        if(GameManager.m_firstTimeUIButtonsTutorial == 1)
        {
            if(b_gpgsLeaderboardButtonAvailable)
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI();

                if(b_isGPGsLeaderboardTestMode)
                {
                    GooglePlayGamesLeaderboardTestMenuDisappear();
                }
            }
            else
            {
                if(GameManager.m_playerMutedSounds == 0)
                {
                    GameManager.m_muteButtonImage.enabled = false;
                }
                else
                {
                    GameManager.m_unmuteButtonImage.enabled = false;
                }

                GameManager.m_chimpionshipBeltButtonImage.enabled = false;
                _gameManager.m_highScoreLabelText.enabled = false;
                _gameManager.m_highScoreValueText.enabled = false;
                GameManager.m_pauseButtonImage.enabled = false;
                m_gpgsLeaderboardButtonImage.enabled = false;
                _gpgsMenuImage.enabled = true;
                _gpgsLeaderboardNotLoggedInOKButtonImage.enabled = true;
                _gpgsLeaderboardLogInCheckText.enabled = true;
                Time.timeScale = 0;
            }
        }
    }

    public void GooglePlayGamesLeaderboardNotLoggedInOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GameManager.m_pauseMenuObj.SetActive(true);
        _gameManager.ResumeButton();
        m_gpgsLeaderboardButtonImage.enabled = true;
        _gpgsLeaderboardLogInCheckText.enabled = false;
        _gpgsLeaderboardNotLoggedInOKButtonImage.enabled = false;
        _gpgsMenuImage.enabled = false;
        _gameManager.m_polaroidImage.enabled = true;
        GameManager.m_polaroidsCountText.enabled = true;
    }

    public void GooglePlayGamesLeaderboardPlayerRank()
    {
        if(b_gpgsLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboardID , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
            {
                m_playerRank = data.PlayerScore.rank; //This is the rank of the player in the leaderboard
            });

            if(b_isGPGsLeaderboardTestMode && _currentScene > 0)
            {
                _gpgsLeaderboardTestText.text = "Rank : " + m_playerRank;
            }
        }
    }

    public void GooglePlayGamesLeaderboardScoreGet() //This is only for testing
    {
        if(b_gpgsLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboardID , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
            {
                _highScore = data.PlayerScore; //This retrieves the player high score 
            });
        }
        else
        {
            _gpgsLeaderboardTestText.text = "Please Log In First :)";
        }
    }

    public void GooglePlayGamesLeaderboardScoreSet() //This is only for testing
    {
        if(b_isGPGsLeaderboardTestMode)
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboardID , (bool success) =>
            {
                _gpgsLeaderboardTestText.text = "Score Update : " + success;
            });
        }
    }

    public void GooglePlayGamesLeaderboardTestMenuAppear()
    {
        if(GameManager.m_currentScene >= 1 && b_isGPGsLeaderboardTestMode)
        {
            m_gpgsLeaderboardTestMenuImage.enabled = true;
            m_gpgsLeaderboardTestGetButtonImage.enabled = true;
            m_gpgsLeaderboardTestSetButtonImage.enabled = true;
            _gpgsLeaderboardTestText.enabled = true;
            
        }
    }

    public void GooglePlayGamesLeaderboardTestMenuDisappear()
    {
        m_gpgsLeaderboardTestMenuImage.enabled = false;
        m_gpgsLeaderboardTestGetButtonImage.enabled = false;
        m_gpgsLeaderboardTestSetButtonImage.enabled = false;
        _gpgsLeaderboardTestText.enabled = false;
        
    }

    public static void GooglePlayGamesLeaderboardUpdate()
    {
        if(b_gpgsLoggedIn) 
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboardID , (bool success) =>
            {
                if(b_isGPGsLeaderboardTestMode)
                {
                    _gpgsLeaderboardLogInCheckText.text = "Score Update : " + success;
                }
            });
        }
    }

    public void GooglePlayGamesLoggedIn()
    {
        if(!GameManager.b_quitButtonTapped)
        {
            m_gpgsLogInButtonImage.enabled = false;
            m_gpgsRateButtonImage.enabled = true;
            m_gpgsProfilePicImage.enabled = true;
            m_gpgsProfilePicMaskImage.enabled = true;
            m_gpgsUsernameText.enabled = true;
        }

        if(b_isGPGsLogInTestMode)
        {
            m_gpgsLogInTestText.text = "Log In Success :)";
        }
    }

    public void GooglePlayGamesLoggedOut()
    {
        if(!GameManager.b_quitButtonTapped)
        {
            m_gpgsLogInButtonImage.enabled = true;
            m_gpgsRateButtonImage.enabled = false;
            m_gpgsProfilePicImage.enabled = false;
            m_gpgsProfilePicMaskImage.enabled = false;
            m_gpgsUsernameText.enabled = false;
        }

        if(_bGPGsLogInButtonTapped && b_isGPGsLogInTestMode)
        {
            m_gpgsLogInTestText.text = "Log In Failed :(";
            _bGPGsLogInButtonTapped = false;
        }
    }

    void GooglePlayGamesLogIn() 
    {
        if(!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate(GooglePlayGamesLogInCallback);
        }
    }

    public void GooglePlayGamesLogInButton()
    {
        _bGPGsLogInButtonTapped = true;
        m_noInternetText.enabled = false;
        GooglePlayGamesLogIn();
    }

    void GooglePlayGamesLogInCallback(bool success) 
    {  
        if(success)
        {
            b_gpgsLoggedIn = success;
            GooglePlayGamesLeaderboardPlayerRank();
            m_noInternetText.enabled = false;
        }
        else
        {
            b_gpgsLoggedIn = success;

            if(!b_isGPGsLogInTestMode)
            {
                m_noInternetText.enabled = true;
            }
        }
    }

    void GooglePlayGamesLogInCheck()
    {
        if(b_gpgsLoggedIn)
        {
            m_gpgsProfilePicImage.sprite = Sprite.Create(PlayGamesPlatform.Instance.localUser.image , new Rect(0 , 0 , 96 , 96) , new Vector2(0 , 0));
            m_gpgsUsernameText.text = PlayGamesPlatform.Instance.localUser.userName;
            GooglePlayGamesLoggedIn();
        }
        else
        {
            GooglePlayGamesLoggedOut();
        }

        Invoke("GooglePlayGamesLogInCheck" , 0.2f);        
    }

    public void GooglePlayRateButton()
    {
        Application.OpenURL("http://uabhanu.wixsite.com/portfolio"); //TODO Game Play Store URL here after it's live
    }

    //public void OneSignalPushAcceptButton()
    //{
    //    _oneSignalMenuImage.enabled = false;
    //    _oneSignalPushAcceptButtonImage.enabled = false;
    //    _oneSignalPushCancelButtonImage.enabled = false;
    //    _oneSignalPushText.enabled = false;
    //    //TODO Turn Notifications On
    //}

    //public void OneSignalPushCancelButton()
    //{
    //    _oneSignalMenuImage.enabled = false;
    //    _oneSignalPushAcceptButtonImage.enabled = false;
    //    _oneSignalPushCancelButtonImage.enabled = false;
    //    _oneSignalPushText.enabled = false;
    //    //TODO Turn Notifications Off
    //}

    void OneSignalHandleNotificationOpened(OSNotificationOpenedResult notificationResult)
    {
        //m_oneSignalText.text = "Notification Opened :)"; //Working Great!!
        //_oneSignalMenuImage.enabled = true;
        //_oneSignalPushAcceptButtonImage.enabled = true;
        //_oneSignalPushCancelButtonImage.enabled = true;
        //_oneSignalPushText.enabled = true;
    }

    public static void OneSignalPermissionObserver(OSPermissionStateChanges stateChange)
    {
        if(stateChange.from.status == OSNotificationPermission.NotDetermined)
        {
          if(GameManager.m_currentScene == 0)
          {
              m_oneSignalText.text = "No Permission Info :(";
          }
        }

        else if(stateChange.to.status == OSNotificationPermission.Authorized)
        {
            if(GameManager.m_currentScene == 0)
            {
                m_oneSignalText.text = "Thanks for accepting notifications :)";
            }
        }
             
        else if(stateChange.to.status == OSNotificationPermission.Denied)
        {
            if(GameManager.m_currentScene == 0)
            {
                m_oneSignalText.text = "Notifications not accepted. You can turn them on later under your device settings :)";
            }
        }
    }

    public static void OneSignalTagDelete(string key)
    {
        OneSignal.DeleteTag(key);
    }

    public static void OneSignalTagSend(string key , string value)
    {
        OneSignal.SendTag(key , value);
    }
}
