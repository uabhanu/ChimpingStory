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
    bool _bGooglePlayGamesAchievementsButtonTapped , _bGooglePlayGamesLogInButtonTapped;
    //Dictionary<string , object> _highScoresData;
    //Dictionary<string , string> _scores = null;
    //float _highScore;
    GameManager _gameManager;
	Image _googlePlayGamesMenuImage , _googlePlayGamesLeaderboardOKButtonImage , _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage , _googlePlayGamesLeaderboardUpdateAcceptButtonImage , _googlePlayGamesLeaderboardUpdateCancelButtonImage;
    int _currentScene;
    IScore _highScore;
    string /*_applinkURL , */_leaderboard = "leaderboard_chimpionship_board";
	Text _googlePlayGamesLeaderboardUpdateText;

    //[SerializeField] Image _facebookShareButtonImage, _fbChallengeInviteButtonImage;
    [SerializeField] Text /*_fbChallengeInviteTestText, _fbScoreText, */_googlePlayGamesLeaderboardLogInCheckText , _googlePlayGamesLeaderboardTestText , _googlePlayGamesLeaderboardUpdateRequestedText;

    public static bool /*m_facebookProfilePicExists = false, m_isFacebookShareTestMode = false , */b_googlePlayGamesAchievementsButtonAvailable , b_googlePlayGamesLeaderboardButtonAvailable , b_googlePlayGamesLeaderboardAvailable , b_isGooglePlayGamesLeaderboardTestMode;
    public static bool b_isGooglePlayGamesLogInTestMode , b_googlePlayGamesLoggedIn , b_scoresExist;
    public static Button m_googlePlayGamesAchievementsButton , m_googlePlayGamesLeaderboardButton;
    public static GameObject /*m_facebookShareMenuObj , m_facebookShareSuccessMenuObj , m_facebookShareTestMenuObj , */m_googlePlayGamesAchievementsButtonObj , m_googlePlayGamesLeaderboardButtonObj;
    public static Image /*m_facebookButtonImage , m_facebookProfilePicImage , */m_googlePlayGamesAchievementsButtonImage , m_googlePlayGamesLeaderboardButtonImage , m_googlePlayGamesLeaderboardTestGetButtonImage , m_googlePlayGamesLeaderboardTestMenuImage;
    public static Image m_googlePlayGamesLeaderboardTestSetButtonImage , m_googlePlayGamesLogInButtonImage , m_googlePlayGamesProfilePicImage , m_googlePlayGamesProfilePicMaskImage , m_googlePlayRateButtonImage;
    public static int m_playerRank;
    public static Text /*m_facebookUsernameText , */m_googlePlayGamesLeaderboardTestText , m_googlePlayGamesLogInTestText , m_googlePlayGamesUsernameText , m_noInternetText, m_noProfilePicText, m_noUsernameText;

    public Sprite[] m_googlePlayGamessAchievementsButtonSprites , m_googlePlayGamesLeaderboardButtonSprites;
    public string[] m_achievements;

    void Start()
	{
       /****************************************************
        * _achievements[0] = achievement_collect_3_bananas *
        * _achievements[1] = achievement_collect_6_bananas *
        ****************************************************/
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GooglePlayGamesLeaderboardPlayerRank();
        //m_isGooglePlayGamesLeaderboardTestMode = true; //TODO Remove this after testing is finished

        if(_currentScene == 0)
        {
            //m_facebookButtonImage = GameObject.Find("FacebookButton").GetComponent<Image>();
            //m_facebookProfilePicImage = GameObject.Find("FacebookProfilePicImage").GetComponent<Image>();
            //m_facebookShareSuccessMenuObj = GameObject.Find("FBShareSuccessMenu");
            //m_facebookShareTestMenuObj = GameObject.Find("FBShareTestMenu");
            //m_facebookUsernameText = GameObject.Find("FacebookUsernameText").GetComponent<Text>();
            m_googlePlayGamesLogInButtonImage = GameObject.Find("GPGsLogInButton").GetComponent<Image>();
            m_googlePlayGamesProfilePicImage = GameObject.Find("GPGsProfilePicImage").GetComponent<Image>();
            m_googlePlayGamesProfilePicMaskImage = GameObject.Find("GPGsProfilePicMask").GetComponent<Image>();
            m_googlePlayGamesUsernameText = GameObject.Find("GPGsUsernameText").GetComponent<Text>();
            m_googlePlayRateButtonImage = GameObject.Find("GPGsRateButton").GetComponent<Image>();
            m_noInternetText = GameObject.Find("NoInternetText").GetComponent<Text>();
            //m_noProfilePicText = GameObject.Find("NoProfilePicText").GetComponent<Text>();
            //m_noUsernameText = GameObject.Find("NoUsernameText").GetComponent<Text>();
            //FacebookInit();
            GooglePlayGamesInit();
            //Invoke("FacebookLogInCheck" , 0.2f);
            Invoke("GooglePlayGamesLogInCheck" , 0.2f);
        }
        else
        {
            m_googlePlayGamesAchievementsButtonObj = GameObject.Find("GPGsAchievementsButton");
            m_googlePlayGamesAchievementsButton = m_googlePlayGamesAchievementsButtonObj.GetComponent<Button>();
            m_googlePlayGamesAchievementsButtonImage = m_googlePlayGamesAchievementsButtonObj.GetComponent<Image>();
            m_googlePlayGamesLeaderboardButtonObj = GameObject.Find("GPGsLeaderboardButton");
            m_googlePlayGamesLeaderboardButton = m_googlePlayGamesLeaderboardButtonObj.GetComponent<Button>();
            m_googlePlayGamesLeaderboardButtonImage = m_googlePlayGamesLeaderboardButtonObj.GetComponent<Image>();
            _googlePlayGamesLeaderboardOKButtonImage = GameObject.Find("OKButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage = GameObject.Find("SuccessOrFailedOKButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestMenuImage = GameObject.Find("GPGsLeaderboardTestMenu").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestGetButtonImage = GameObject.Find("TestGetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestSetButtonImage = GameObject.Find("TestSetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestText = GameObject.Find("TestText").GetComponent<Text>();
            _googlePlayGamesLeaderboardUpdateAcceptButtonImage = GameObject.Find("UpdateAcceptButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateCancelButtonImage = GameObject.Find("UpdateCancelButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateText = GameObject.Find("UpdateText").GetComponent<Text>();
            _googlePlayGamesMenuImage = GameObject.Find("GooglePlayGamesMenu").GetComponent<Image>();
        }
    }

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

    public void GooglePlayGamesAchievementsButton()
    {
        _bGooglePlayGamesAchievementsButtonTapped = true;

        if(b_googlePlayGamesAchievementsButtonAvailable)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            GameManager.m_pauseMenuObj.SetActive(false);
            _googlePlayGamesLeaderboardOKButtonImage.enabled = true;
            _googlePlayGamesLeaderboardLogInCheckText.enabled = true;
            _googlePlayGamesMenuImage.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void GooglePlayGamesIncrementalAchievements(string achievement , int steps)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(achievement , steps , (bool success) => {});
    }

    void GooglePlayGamesInit()
    {
        _bGooglePlayGamesLogInButtonTapped = false;
        PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(clientConfig);
        PlayGamesPlatform.Activate();
    }

    public void GooglePlayGamesLeaderboardButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        if(b_googlePlayGamesLeaderboardButtonAvailable && b_googlePlayGamesLeaderboardAvailable)
        {
            if(GameManager.m_currentScene == 1)
            {
                if(!GameManager.m_adsMenuImage.enabled && !_googlePlayGamesLeaderboardLogInCheckText.enabled)
                {
                    _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = true;
                    _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = true;
                    _googlePlayGamesMenuImage.enabled = true;
                    _googlePlayGamesLeaderboardUpdateText.enabled = true;
            
                    if(b_isGooglePlayGamesLeaderboardTestMode)
                    {
                        GooglePlayGamesLeaderboardTestMenuDisappear();
                    }

                    GameManager.m_pauseButtonImage.enabled = false;
                    Time.timeScale = 0;
                }
            }
            else
            {
                if(!_googlePlayGamesLeaderboardLogInCheckText.enabled)
                {
                    _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = true;
                    _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = true;
                    _googlePlayGamesMenuImage.enabled = true;
                    _googlePlayGamesLeaderboardUpdateText.enabled = true;
            
                    if(b_isGooglePlayGamesLeaderboardTestMode)
                    {
                        GooglePlayGamesLeaderboardTestMenuDisappear();
                    }

                    GameManager.m_pauseButtonImage.enabled = false;
                    Time.timeScale = 0;
                }
            }
        }
        
        else if(b_googlePlayGamesLeaderboardButtonAvailable && !b_googlePlayGamesLeaderboardAvailable)
        {
            if(GameManager.m_muteButtonImage.enabled)
            {
                GameManager.m_muteButtonImage.enabled = false;
            }
            else
            {
                GameManager.m_unmuteButtonImage.enabled = false;
            }

            GameManager.m_chimpionshipBeltImage.enabled = false;
            GameManager.m_highScoreDisplayText.enabled = false;
            GameManager.m_highScoreValueText.enabled = false;
            GameManager.m_pauseButtonImage.enabled = false;
            m_googlePlayGamesLeaderboardButtonImage.enabled = false;
            _googlePlayGamesMenuImage.enabled = true;
            _googlePlayGamesLeaderboardOKButtonImage.enabled = true;
            _googlePlayGamesLeaderboardLogInCheckText.text = "Leaderboards are not available yet, probably because there are are no high scores available";
            _googlePlayGamesLeaderboardLogInCheckText.enabled = true;
            Time.timeScale = 0;
        }

        else
        {
            if(GameManager.m_muteButtonImage.enabled)
            {
                GameManager.m_muteButtonImage.enabled = false;
            }
            else
            {
                GameManager.m_unmuteButtonImage.enabled = false;
            }

            GameManager.m_chimpionshipBeltImage.enabled = false;
            GameManager.m_highScoreDisplayText.enabled = false;
            GameManager.m_highScoreValueText.enabled = false;
            GameManager.m_pauseButtonImage.enabled = false;
            m_googlePlayGamesLeaderboardButtonImage.enabled = false;
            _googlePlayGamesMenuImage.enabled = true;
            _googlePlayGamesLeaderboardOKButtonImage.enabled = true;
            _googlePlayGamesLeaderboardLogInCheckText.enabled = true;
            Time.timeScale = 0;
        }

        
    }

    public void GooglePlayGamesLeaderboardOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GameManager.m_pauseMenuObj.SetActive(true);
        _gameManager.ResumeButton();
        m_googlePlayGamesLeaderboardButtonImage.enabled = true;
        _googlePlayGamesLeaderboardLogInCheckText.enabled = false;
        _googlePlayGamesLeaderboardOKButtonImage.enabled = false;
        _googlePlayGamesMenuImage.enabled = false;
    }

    public void GooglePlayGamesLeaderboardPlayerRank()
    {
        if(b_googlePlayGamesLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboard , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
            {
                m_playerRank = data.PlayerScore.rank; //This is the rank of the player in the leaderboard

                if(data.Scores.Length > 0)
                {
                    b_scoresExist = true;
                }
            });
        }
    }

    public void GooglePlayGamesLeaderboardScoreGet()
    {
        if(b_googlePlayGamesLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboard , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
            {
                _highScore = data.PlayerScore; //This retrieves the player high score 
            });
        }
        else
        {
            _googlePlayGamesLeaderboardTestText.text = "Please Log In First :)";
        }
    }

    public void GooglePlayGamesLeaderboardScoreSet()
    {
        if(b_isGooglePlayGamesLeaderboardTestMode)
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboard , (bool success) =>
            {
                _googlePlayGamesLeaderboardTestText.text = "Score Update : " + success;
            });
        }
    }

    public void GooglePlayGamesLeaderboardSuccessOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GameManager.m_pauseButtonImage.enabled = true;
        _googlePlayGamesMenuImage.enabled = false;
        _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateRequestedText.enabled = false;
        Time.timeScale = 1;
    }

    public static void GooglePlayGamesLeaderboardTestMenuAppear()
    {
        if(GameManager.m_currentScene >= 1 && b_isGooglePlayGamesLeaderboardTestMode)
        {
            m_googlePlayGamesLeaderboardTestMenuImage.enabled = true;
            m_googlePlayGamesLeaderboardTestGetButtonImage.enabled = true;
            m_googlePlayGamesLeaderboardTestSetButtonImage.enabled = true;
            m_googlePlayGamesLeaderboardTestText.enabled = true;
        }
    }

    public static void GooglePlayGamesLeaderboardTestMenuDisappear()
    {
        m_googlePlayGamesLeaderboardTestMenuImage.enabled = false;
        m_googlePlayGamesLeaderboardTestGetButtonImage.enabled = false;
        m_googlePlayGamesLeaderboardTestSetButtonImage.enabled = false;
        m_googlePlayGamesLeaderboardTestText.enabled = false;
    }

    public void GooglePlayGamesLeaderboardUpdateAcceptButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GooglePlayGamesLeaderboardScoreGet();

        if(b_googlePlayGamesLoggedIn) 
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboard , (bool success) =>
            {
                if(b_isGooglePlayGamesLeaderboardTestMode)
                {
                    _googlePlayGamesLeaderboardLogInCheckText.text = "Score Update : " + success;
                }

                _googlePlayGamesMenuImage.enabled = true;
                _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage.enabled = true;
                _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = false;
                _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = false;
                _googlePlayGamesLeaderboardUpdateRequestedText.enabled = true;
                _googlePlayGamesLeaderboardUpdateText.enabled = false;
            });

            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.LogError("Sir Bhanu, Please make sure you are logged in first :) ");
            _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = false;
            _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = false;
            _googlePlayGamesLeaderboardUpdateText.enabled = false;
            _googlePlayGamesLeaderboardOKButtonImage.enabled = true;
            _googlePlayGamesLeaderboardLogInCheckText.enabled = true;
        }
    }

    public void GooglePlayGamesLeaderboardUpdateCancelButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        if(PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }

        _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = false;
        _googlePlayGamesMenuImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateText.enabled = false;
        _googlePlayGamesLeaderboardLogInCheckText.enabled = false;
        
        if(b_isGooglePlayGamesLeaderboardTestMode)
        {
            GooglePlayGamesLeaderboardTestMenuAppear();
        }

        GameManager.m_pauseButtonImage.enabled = true;
        Time.timeScale = 1;
    }

    public void GooglePlayGamesLoggedIn()
    {
        if(!GameManager.m_isQuitButtonTapped)
        {
            m_googlePlayGamesLogInButtonImage.enabled = false;
            m_googlePlayRateButtonImage.enabled = true;
            m_googlePlayGamesProfilePicImage.enabled = true;
            m_googlePlayGamesProfilePicMaskImage.enabled = true;
            m_googlePlayGamesUsernameText.enabled = true;
        }

        if(b_isGooglePlayGamesLogInTestMode)
        {
            m_googlePlayGamesLogInTestText.text = "Log In Success :)";
        }
    }

    public void GooglePlayGamesLoggedOut()
    {
        if(!GameManager.m_isQuitButtonTapped)
        {
            m_googlePlayGamesLogInButtonImage.enabled = true;
            m_googlePlayRateButtonImage.enabled = false;
            m_googlePlayGamesProfilePicImage.enabled = false;
            m_googlePlayGamesProfilePicMaskImage.enabled = false;
            m_googlePlayGamesUsernameText.enabled = false;
        }

        if(_bGooglePlayGamesLogInButtonTapped && b_isGooglePlayGamesLogInTestMode)
        {
            m_googlePlayGamesLogInTestText.text = "Log In Failed :(";
            _bGooglePlayGamesLogInButtonTapped = false;
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
        _bGooglePlayGamesLogInButtonTapped = true;
        GooglePlayGamesLogIn();
    }

    void GooglePlayGamesLogInCallback(bool success) 
    {  
        if(success)
        {
            GooglePlayGamesLoggedIn();
            m_noInternetText.enabled = false;
        }
        else
        {
            GooglePlayGamesLoggedOut();
            m_noInternetText.enabled = true;
        }

        b_googlePlayGamesLoggedIn = success;
    }

    void GooglePlayGamesLogInCheck()
    {
        if(b_googlePlayGamesLoggedIn)
        {
            m_googlePlayGamesProfilePicImage.sprite = Sprite.Create(PlayGamesPlatform.Instance.localUser.image , new Rect(0 , 0 , 96 , 96) , new Vector2(0 , 0));
            m_googlePlayGamesUsernameText.text = PlayGamesPlatform.Instance.localUser.userName;
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
}
