//using Facebook.Unity;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
//using System;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SocialmediaManager : MonoBehaviour 
{
    bool _bGPGsAchievementsButtonTapped , _bGPGsLogInButtonTapped;
    //Dictionary<string , object> _highScoresData;
    //Dictionary<string , string> _scores = null;
    //float _highScore;
    GameManager _gameManager;
	Image _gpgsMenuImage , _gpgsLeaderboardNotLoggedInOKButtonImage , _gpgsLeaderboardSuccessOrFailedOKButtonImage , _gpgsLeaderboardUpdateAcceptButtonImage , _gpgsLeaderboardUpdateCancelButtonImage;
    int _currentScene;
    IScore _highScore;
    string /*_applinkURL , */_leaderboard = "CgkInMKFu8wYEAIQAw"; //String name doesn't work so use this unique key, same for achievements
	Text _gpgsLeaderboardUpdateText;

    //[SerializeField] Image _facebookShareButtonImage, _fbChallengeInviteButtonImage;
    [SerializeField] Text /*_fbChallengeInviteTestText, _fbScoreText, */_gpgsLeaderboardLogInCheckText , _gpgsLeaderboardTestText , _gpgsLeaderboardUpdateRequestedText;

    public static bool /*m_facebookProfilePicExists = false, m_isFacebookShareTestMode = false , */b_gpgsAchievementsButtonAvailable , b_gpgsLeaderboardButtonAvailable , b_isGPGsLeaderboardTestMode;
    public static bool b_isGPGsAchievementsTestMode , b_isGPGsLogInTestMode , b_gpgsLoggedIn;
    public static Button m_gpgsAchievementsButton , m_gpgsLeaderboardButton;
    public static GameObject /*m_facebookShareMenuObj , m_facebookShareSuccessMenuObj , m_facebookShareTestMenuObj , */m_gpgsAchievementsButtonObj , m_gpgsLeaderboardButtonObj;
    public static Image /*m_facebookButtonImage , m_facebookProfilePicImage , */m_gpgsAchievementsButtonImage , m_gpgsLeaderboardButtonImage , m_gpgsLeaderboardTestGetButtonImage , m_gpgsLeaderboardTestMenuImage;
    public static Image m_gpgsLeaderboardTestSetButtonImage , m_gpgsLogInButtonImage , m_gpgsProfilePicImage , m_gpgsProfilePicMaskImage , m_gpgsRateButtonImage;
    public static int m_playerRank;
    public static Text /*m_facebookUsernameText , */m_gpgsAchievementsTestText , m_gpgsLogInTestText , m_gpgsUsernameText , m_noInternetText, m_noProfilePicText, m_noUsernameText;

    public Sprite[] m_gpgsAchievementsButtonSprites , m_gpgsLeaderboardButtonSprites;

    void Start()
	{
        //b_isGPGsAchievementsTestMode = true;
        //b_isGPGsLeaderboardTestMode = true; //TODO Remove this after testing is finished
        //b_isGPGsLogInTestMode = true;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        OneSignal.StartInit("48e311d3-611f-4dc8-9a48-590f7b15a4e8").HandleNotificationOpened(OneSignalHandleNotificationOpened).EndInit();
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        if(_currentScene == 0)
        {
            #region Facebook Start
            //m_facebookButtonImage = GameObject.Find("FacebookButton").GetComponent<Image>();
            //m_facebookProfilePicImage = GameObject.Find("FacebookProfilePicImage").GetComponent<Image>();
            //m_facebookShareSuccessMenuObj = GameObject.Find("FBShareSuccessMenu");
            //m_facebookShareTestMenuObj = GameObject.Find("FBShareTestMenu");
            //m_facebookUsernameText = GameObject.Find("FacebookUsernameText").GetComponent<Text>();
            //FacebookInit();
            //Invoke("FacebookLogInCheck" , 0.2f);
            #endregion
            m_gpgsLogInButtonImage = GameObject.Find("GPGsLogInButton").GetComponent<Image>();
            m_gpgsLogInTestText = GameObject.Find("GPGsLogInTestText").GetComponent<Text>();
            m_gpgsProfilePicImage = GameObject.Find("GPGsProfilePicImage").GetComponent<Image>();
            m_gpgsProfilePicMaskImage = GameObject.Find("GPGsProfilePicMask").GetComponent<Image>();
            m_gpgsRateButtonImage = GameObject.Find("GPGsRateButton").GetComponent<Image>();
            m_gpgsUsernameText = GameObject.Find("GPGsUsernameText").GetComponent<Text>();
            m_noInternetText = GameObject.Find("NoInternetText").GetComponent<Text>();
            m_noProfilePicText = GameObject.Find("NoProfilePicText").GetComponent<Text>();
            m_noUsernameText = GameObject.Find("NoUsernameText").GetComponent<Text>();
            GooglePlayGamesInit();
            Invoke("GooglePlayGamesLogInCheck" , 0.2f);

            if(b_isGPGsLogInTestMode)
            {
                m_gpgsLogInTestText.enabled = true;
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
            _gpgsLeaderboardSuccessOrFailedOKButtonImage = GameObject.Find("SuccessOrFailedOKButton").GetComponent<Image>();
            m_gpgsLeaderboardTestMenuImage = GameObject.Find("PF_GPGsLeaderboardTestMenu").GetComponent<Image>();
            m_gpgsLeaderboardTestGetButtonImage = GameObject.Find("TestGetButton").GetComponent<Image>();
            m_gpgsLeaderboardTestSetButtonImage = GameObject.Find("TestSetButton").GetComponent<Image>();
            _gpgsLeaderboardUpdateAcceptButtonImage = GameObject.Find("UpdateAcceptButton").GetComponent<Image>();
            _gpgsLeaderboardUpdateCancelButtonImage = GameObject.Find("UpdateCancelButton").GetComponent<Image>();
            _gpgsLeaderboardUpdateText = GameObject.Find("UpdateText").GetComponent<Text>();
            _gpgsMenuImage = GameObject.Find("PF_GPGsMenu").GetComponent<Image>();
        }
    }

    #region Facebook Methods
    //   void FacebookAppLinkURL(IAppLinkResult applinkResult) //Not sure how to use this yet so not using for now
    //   {
    //       if(!string.IsNullOrEmpty(applinkResult.Url))
    //       {
    //           _applinkURL = "" + applinkResult.Url + "";
    //           Debug.Log(_applinkURL);
    //       }
    //       else
    //       {
    //           _applinkURL = "http://uabhanu.wixsite.com/portfolio";
    //       }
    //   }

    //   public void FacebookChallengePlayers()
    //   {
    //       FB.AppRequest
    //       (
    //           "Come and try to be the Chimpion :) " ,
    //           null ,
    //           new List<object> { "app_users" } ,
    //           null ,
    //           null ,
    //           null ,
    //           null ,
    //           FacebookChallengePlayersCallback
    //       );
    //   }

    //   void FacebookChallengePlayersCallback(IAppRequestResult appRequestResult)
    //   {
    //       if(appRequestResult.Cancelled)
    //       {
    //           //Debug.LogWarning("Sir Bhanu, You have cancelled the invite");
    //       }

    //       else if(!string.IsNullOrEmpty(appRequestResult.Error))
    //       {
    //           //Debug.LogError("Sir Bhanu, There is a problem : " + appRequestResult.Error);
    //       }

    //       else if(!string.IsNullOrEmpty(appRequestResult.RawResult))
    //       {
    //           //Debug.LogWarning("Sir Bhanu, Your invitation : " + appRequestResult.RawResult);

    //           if(!m_isFacebookShareTestMode)
    //           {
    //               ScoreManager.m_supersCount++;
    //               BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
    //           }

    //           //_fbChallengeInviteSuccessMenuObj.SetActive(true);
    //       }
    //   }

    //   void FacebookInit()
    //   {
    //       if(!FB.IsInitialized)
    //       {
    //           HideUnityDelegate FBOnHideUnity = null;
    //           InitDelegate FBSetInit = null;
    //           FB.Init(FBSetInit , FBOnHideUnity);
    //       }
    //   }

    //   public void FacebookInviteOKButton()
    //   {
    //       //_fbChallengeInviteSuccessMenuObj.SetActive(false);
    //   }

    //   void FacebookLoggedIn()
    //   {
    //       FB.API("/me?fields=first_name" , HttpMethod.GET , FacebookUsernameDisplay);
    //       FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FacebookProfilePicDisplay);

    //       m_facebookButtonImage.enabled = false;

    //       if(m_facebookProfilePicExists)
    //       {
    //           m_facebookProfilePicImage.enabled = true;
    //       }

    //       m_facebookUsernameText.enabled = true;
    //   }

    //   void FacebookLoggedOut()
    //{
    //       m_facebookProfilePicImage.enabled = false;
    //       m_facebookUsernameText.enabled = false;
    //}

    //   void FacebookLogIn()
    //   {
    //       if(FB.IsInitialized)
    //       {
    //           m_noInternetText.enabled = false;
    //           List<string> permissions = new List<string>();
    //           FB.LogInWithReadPermissions(permissions , FacebookLogInCallBack);
    //       }
    //   }

    //   public void FacebookLoginButton()
    //   {
    //       FacebookLogIn();
    //       Invoke("FacebookLoggedIn" , 0.2f);
    //   }

    //   void FacebookLogInCallBack(IResult logInResult)
    //   {
    //       if(logInResult.Cancelled)
    //       {
    //           Debug.LogWarning("Sir Bhanu, You have cancelled the LogIn" + logInResult.RawResult);
    //           m_facebookButtonImage.enabled = true;
    //           FacebookLoggedOut();
    //       }

    //       else if(!string.IsNullOrEmpty(logInResult.Error))
    //       {
    //           if(logInResult.RawResult.Contains("Error button pressed"))
    //           {
    //               Debug.LogWarning("Sir Bhanu, You have pressed Error Button" + logInResult.RawResult);
    //               m_facebookButtonImage.enabled = true;
    //               FacebookLoggedOut();
    //           }
    //           else
    //           {
    //               Debug.LogWarning("Sir Bhanu, Please check your internet connection" + logInResult.RawResult);
    //               m_facebookButtonImage.enabled = true;
    //               m_noInternetText.enabled = true;
    //               FacebookLoggedOut();
    //           }
    //       }

    //       else if(!string.IsNullOrEmpty(logInResult.RawResult))
    //       {
    //           Debug.LogWarning("Sir Bhanu, Your LogIn : " + logInResult.RawResult);
    //           FacebookLoggedIn();
    //       }
    //   }

    //   void FacebookLogInCheck()
    //   {
    //       if(!m_facebookProfilePicExists)
    //       {
    //           Invoke("FacebookLogInCheck" , 0.2f);
    //       }

    //       if(FB.IsLoggedIn)
    //       {
    //           FacebookLoggedIn();
    //       }
    //       else
    //       {
    //           FacebookLoggedOut();
    //       }
    //   }

    //   void FacebookOnHideUnity(bool isGameShown)
    //{
    //	if(!isGameShown) 
    //	{
    //		Time.timeScale = 0;
    //	} 
    //	else 
    //	{
    //		Time.timeScale = 1;	
    //	}
    //}

    //   void FacebookProfilePicDisplay(IGraphResult graphicResult)
    //   {
    //       try
    //       {
    //           if(graphicResult.Texture != null && graphicResult.Error == null)
    //           {
    //               m_facebookProfilePicImage.sprite = Sprite.Create(graphicResult.Texture , new Rect(0 , 0 , graphicResult.Texture.width , graphicResult.Texture.height) , new Vector2());

    //               if(m_facebookProfilePicImage.sprite != null)
    //               {
    //                   m_facebookProfilePicExists = true; //This is used to check if sprite created properly and display only if it is, or else, _profilePicImage won't be enabled
    //               }
    //           }
    //       }
    //       catch(Exception e)
    //       {
    //           Debug.Log(e);
    //       }
    //   }

    //   void FacebookSetInit()
    //   {
    //       if(FB.IsLoggedIn)
    //       {
    //           FacebookLoggedIn();
    //       }
    //       else
    //       {
    //           FacebookLoggedOut();
    //       }
    //   }

    //   public void FacebookShareButton()
    //   {
    //       FB.ShareLink
    //       (
    //           contentTitle: "Fourth Lion Studios Message" ,
    //           contentURL: new Uri("http://uabhanu.wixsite.com/portfolio") , //TODO Game URL here when Live
    //           contentDescription: "We really hope you love the game" ,
    //           callback: FacebookShareCallback
    //       );
    //   }

    //   void FacebookShareCallback(IShareResult shareResult)
    //   {
    //       if(shareResult.Cancelled)
    //       {
    //           Debug.LogWarning("Sir Bhanu, you have cancelled the Share :)");
    //       }

    //       else if(!string.IsNullOrEmpty(shareResult.Error))
    //       {
    //           Debug.LogError("Sir Bhanu, You have pressed error button");
    //       }

    //       else
    //       {
    //           Debug.LogWarning("Sir Bhanu, Your Share is a success :)");

    //           if(!m_isFacebookShareTestMode)
    //           {
    //               ScoreManager.m_supersCount++;
    //               BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
    //           }

    //           m_facebookShareSuccessMenuObj.SetActive(true);
    //           m_googlePlayGamesLogInButtonImage.enabled = false;
    //           m_googlePlayGamesLogInTestText.enabled = false;
    //           m_googlePlayRateButtonImage.enabled = false;
    //       }
    //   }

    //   public void FacebookShareOKButton()
    //   {
    //       if(m_facebookShareSuccessMenuObj != null)
    //       {
    //          m_facebookShareSuccessMenuObj.SetActive(false);
    //       }

    //       if(m_googlePlayGamesLogInButtonImage != null)
    //       {
    //           m_googlePlayGamesLogInButtonImage.enabled = true;
    //       }

    //       if(m_isGooglePlayGamesLogInTestMode && m_googlePlayGamesLogInTestText != null)
    //       {
    //           m_googlePlayGamesLogInTestText.enabled = true;
    //       }

    //       if(m_googlePlayRateButtonImage != null)
    //       {
    //           m_googlePlayRateButtonImage.enabled = true;
    //       }
    //   }

    //   void FacebookUsernameDisplay(IResult usernameResult)
    //   {
    //       if(usernameResult.Error == null)
    //       {
    //           m_facebookUsernameText.text = "Hi " + usernameResult.ResultDictionary["first_name"];
    //       }

    //       if(m_facebookUsernameText.text != null)
    //       {
    //           m_facebookUsernameText.enabled = true;
    //       }
    //   }
    #endregion

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
        GooglePlayGamesLeaderboardPlayerRank();

        if(GameManager.m_firstTimeUIButtonsTutorial == 1)
        {
            if(b_gpgsLeaderboardButtonAvailable)
            {
                GameManager.m_chimpionshipBeltButtonImage.enabled = false;
                GameManager.m_highScoreDisplayText.enabled = false;
                GameManager.m_highScoreValueText.enabled = false;

                if(GameManager.m_playerMutedSounds == 0)
                {
                    GameManager.m_muteButtonImage.enabled = false;
                }
                else
                {
                    GameManager.m_unmuteButtonImage.enabled = false;
                }

                GameManager.m_pauseButtonImage.enabled = false;
                _gpgsLeaderboardUpdateAcceptButtonImage.enabled = true;
                m_gpgsLeaderboardButtonImage.enabled = false;
                _gpgsLeaderboardUpdateCancelButtonImage.enabled = true;
                _gpgsLeaderboardLogInCheckText.enabled = false;
                _gpgsMenuImage.enabled = true;
                _gpgsLeaderboardUpdateText.enabled = true;

                if(b_isGPGsLeaderboardTestMode)
                {
                    GooglePlayGamesLeaderboardTestMenuDisappear();
                }

                Time.timeScale = 0;
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
                GameManager.m_highScoreDisplayText.enabled = false;
                GameManager.m_highScoreValueText.enabled = false;
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
    }

    public void GooglePlayGamesLeaderboardPlayerRank()
    {
        if(b_gpgsLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboard , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
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
            PlayGamesPlatform.Instance.LoadScores(_leaderboard , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
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
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboard , (bool success) =>
            {
                _gpgsLeaderboardTestText.text = "Score Update : " + success;
            });
        }
    }

    public void GooglePlayGamesLeaderboardSuccessOrFailureOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        GameManager.m_chimpionshipBeltButtonImage.enabled = true;
        GameManager.m_highScoreDisplayText.enabled = true;
        GameManager.m_highScoreValueText.enabled = true;

        if(GameManager.m_playerMutedSounds == 0)
        {
            GameManager.m_muteButtonImage.enabled = true;
        }
        else
        {
            GameManager.m_unmuteButtonImage.enabled = true;
        }

        GameManager.m_pauseButtonImage.enabled = true;
        m_gpgsLeaderboardButtonImage.enabled = true;
        _gpgsLeaderboardSuccessOrFailedOKButtonImage.enabled = false;
        _gpgsLeaderboardUpdateAcceptButtonImage.enabled = false;
        _gpgsLeaderboardUpdateCancelButtonImage.enabled = false;
        _gpgsLeaderboardUpdateRequestedText.enabled = false;
        _gpgsMenuImage.enabled = false;
        _gpgsLeaderboardUpdateText.enabled = false;
        _gpgsLeaderboardLogInCheckText.enabled = false;
        Time.timeScale = 1;
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

    public void GooglePlayGamesLeaderboardUpdateAcceptButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        if(b_gpgsLoggedIn) 
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboard , (bool success) =>
            {
                if(b_isGPGsLeaderboardTestMode)
                {
                    _gpgsLeaderboardLogInCheckText.text = "Score Update : " + success;
                }

                _gpgsMenuImage.enabled = true;
                _gpgsLeaderboardSuccessOrFailedOKButtonImage.enabled = true;
                _gpgsLeaderboardUpdateAcceptButtonImage.enabled = false;
                _gpgsLeaderboardUpdateCancelButtonImage.enabled = false;
                _gpgsLeaderboardUpdateRequestedText.enabled = true;
                _gpgsLeaderboardUpdateText.enabled = false;
            });

            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.LogError("Sir Bhanu, Please make sure you are logged in first :) ");
            _gpgsLeaderboardUpdateAcceptButtonImage.enabled = false;
            _gpgsLeaderboardUpdateCancelButtonImage.enabled = false;
            _gpgsLeaderboardUpdateText.enabled = false;
            _gpgsLeaderboardNotLoggedInOKButtonImage.enabled = true;
            _gpgsLeaderboardLogInCheckText.enabled = true;
        }
    }

    public void GooglePlayGamesLeaderboardUpdateCancelButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        if(PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }

        if(b_isGPGsLeaderboardTestMode)
        {
            GooglePlayGamesLeaderboardTestMenuAppear();
        }

        GameManager.m_chimpionshipBeltButtonImage.enabled = true;
        GameManager.m_highScoreDisplayText.enabled = true;
        GameManager.m_highScoreValueText.enabled = true;

        if(GameManager.m_playerMutedSounds == 0)
        {
            GameManager.m_muteButtonImage.enabled = true;
        }
        else
        {
            GameManager.m_unmuteButtonImage.enabled = true;
        }

        GameManager.m_pauseButtonImage.enabled = true;
        m_gpgsLeaderboardButtonImage.enabled = true;
        _gpgsLeaderboardUpdateAcceptButtonImage.enabled = false;
        _gpgsLeaderboardUpdateCancelButtonImage.enabled = false;
        _gpgsMenuImage.enabled = false;
        _gpgsLeaderboardUpdateText.enabled = false;
        _gpgsLeaderboardLogInCheckText.enabled = false;
        Time.timeScale = 1;
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

    void OneSignalHandleNotificationOpened(OSNotificationOpenedResult notificationResult)
    {

    }
}
