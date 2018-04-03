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
    bool _googlePlayGamesLogInButtonTapped;
    //Dictionary<string , object> _highScoresData;
    //Dictionary<string , string> _scores = null;
    //float _highScore;
    GameManager _gameManager;
	Image _googlePlayGamesLeaderboardConfirmMenuImage , _googlePlayGamesLeaderboardOKButtonImage , _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage , _googlePlayGamesLeaderboardUpdateAcceptButtonImage , _googlePlayGamesLeaderboardUpdateCancelButtonImage;
    int _currentScene;
    IScore _highScore;
    string /*_applinkURL , */_leaderboardID = "CgkInMKFu8wYEAIQAw";
	Text _googlePlayGamesLeaderboardUpdateText;

    //[SerializeField] Image _facebookShareButtonImage, _fbChallengeInviteButtonImage;
    [SerializeField] Text /*_fbChallengeInviteTestText, _fbScoreText, */_googlePlayGamesLeaderboardLogInCheckText , _googlePlayGamesLeaderboardTestText , _googlePlayGamesLeaderboardUpdateRequestedText;

    public static bool /*m_facebookProfilePicExists = false, m_isFacebookShareTestMode = false , */m_isGooglePlayGamesLeaderboardTestMode , m_isGooglePlayGamesLogInTestMode , m_isGooglePlayGamesLoggedIn;
    public static Button m_googlePlayGamesLeaderboardButton;
    public static GameObject /*m_facebookShareMenuObj , m_facebookShareSuccessMenuObj , m_facebookShareTestMenuObj , */m_googlePlayGamesLeaderboardButtonObj;
    public static Image /*m_facebookButtonImage , m_facebookProfilePicImage , */m_googlePlayGamesLeaderboardTestGetButtonImage , m_googlePlayGamesLeaderboardTestMenuImage;
    public static Image m_googlePlayGamesLeaderboardTestSetButtonImage , m_googlePlayGamesLogInButtonImage , m_googlePlayRateButtonImage , m_googlePlayGamesProfilePicImage;
    public static Text /*m_facebookUsernameText , */m_googlePlayGamesLeaderboardTestText , m_googlePlayGamesLogInTestText , m_googlePlayGamesUsernameText , m_noInternetText, m_noProfilePicText, m_noUsernameText;

    public static bool m_scoresExist;
    public static int m_playerRank;

    void Start()
	{
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
            m_googlePlayGamesUsernameText = GameObject.Find("GPGsUsernameText").GetComponent<Text>();
            m_googlePlayRateButtonImage = GameObject.Find("GPGsRateButton").GetComponent<Image>();
            m_noInternetText = GameObject.Find("NoInternetText").GetComponent<Text>();
            //m_noProfilePicText = GameObject.Find("NoProfilePicText").GetComponent<Text>();
            //m_noUsernameText = GameObject.Find("NoUsernameText").GetComponent<Text>();
            //FacebookInit();
            GooglePlayGamesInit();
            //Invoke("FacebookLogInCheck" , 0.2f);
            GooglePlayGamesLogInCheck();
        }
        else
        {
            m_googlePlayGamesLeaderboardButtonObj = GameObject.Find("GPGsLeaderboardButton");
            m_googlePlayGamesLeaderboardButton = GameObject.Find("GPGsLeaderboardButton").GetComponent<Button>();
            _googlePlayGamesLeaderboardConfirmMenuImage = GameObject.Find("GPGsLeaderboardConfirmMenu").GetComponent<Image>();
            _googlePlayGamesLeaderboardOKButtonImage = GameObject.Find("OKButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage = GameObject.Find("SuccessOrFailedOKButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestMenuImage = GameObject.Find("GPGsLeaderboardTestMenu").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestGetButtonImage = GameObject.Find("TestGetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestSetButtonImage = GameObject.Find("TestSetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestText = GameObject.Find("TestText").GetComponent<Text>();
            _googlePlayGamesLeaderboardUpdateAcceptButtonImage = GameObject.Find("UpdateAcceptButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateCancelButtonImage = GameObject.Find("UpdateCancelButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateText = GameObject.Find("UpdateText").GetComponent<Text>();
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

    void GooglePlayGamesInit()
    {
        _googlePlayGamesLogInButtonTapped = false;
        PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(clientConfig);
        PlayGamesPlatform.Activate();
    }

    public void GooglePlayGamesLeaderboardButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();

        if(GameManager.m_currentScene == 1)
        {
            if(!GameManager.m_adsMenuImage.enabled && !_googlePlayGamesLeaderboardLogInCheckText.enabled)
            {
                _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = true;
                _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = true;
                _googlePlayGamesLeaderboardConfirmMenuImage.enabled = true;
                _googlePlayGamesLeaderboardUpdateText.enabled = true;
            
                if(m_isGooglePlayGamesLeaderboardTestMode)
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
                _googlePlayGamesLeaderboardConfirmMenuImage.enabled = true;
                _googlePlayGamesLeaderboardUpdateText.enabled = true;
            
                if(m_isGooglePlayGamesLeaderboardTestMode)
                {
                    GooglePlayGamesLeaderboardTestMenuDisappear();
                }

                GameManager.m_pauseButtonImage.enabled = false;
                Time.timeScale = 0;
            }
        }
    }

    public void GooglePlayGamesLeaderboardOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GameManager.m_pauseButtonImage.enabled = true;
        _googlePlayGamesLeaderboardConfirmMenuImage.enabled = false;
        _googlePlayGamesLeaderboardLogInCheckText.enabled = false;
        _googlePlayGamesLeaderboardOKButtonImage.enabled = false;
        Time.timeScale = 1;
    }

    public void GooglePlayGamesLeaderboardPlayerRank()
    {
        if(m_isGooglePlayGamesLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboardID , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
            {
                m_playerRank = data.PlayerScore.rank; //This is the rank of the player in the leaderboard

                if(data.Scores.Length > 0)
                {
                    m_scoresExist = true;
                }
            });
        }
    }

    public void GooglePlayGamesLeaderboardScoreGet()
    {
        if(m_isGooglePlayGamesLoggedIn)
        {
            PlayGamesPlatform.Instance.LoadScores(_leaderboardID , LeaderboardStart.TopScores , 20 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (data) =>
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
        if(m_isGooglePlayGamesLeaderboardTestMode)
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboardID , (bool success) =>
            {
                _googlePlayGamesLeaderboardTestText.text = "Score Update : " + success;
            });
        }
    }

    public void GooglePlayGamesLeaderboardSuccessOKButton()
    {
        GooglePlayGamesLeaderboardPlayerRank();
        GameManager.m_pauseButtonImage.enabled = true;
        _googlePlayGamesLeaderboardConfirmMenuImage.enabled = false;
        _googlePlayGamesLeaderboardSuccessOrFailedOKButtonImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateRequestedText.enabled = false;
        Time.timeScale = 1;
    }

    public static void GooglePlayGamesLeaderboardTestMenuAppear()
    {
        if(GameManager.m_currentScene >= 1 && m_isGooglePlayGamesLeaderboardTestMode)
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

        if(m_isGooglePlayGamesLoggedIn) 
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboardID , (bool success) =>
            {
                if(m_isGooglePlayGamesLeaderboardTestMode)
                {
                    _googlePlayGamesLeaderboardLogInCheckText.text = "Score Update : " + success;
                }

                _googlePlayGamesLeaderboardConfirmMenuImage.enabled = true;
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
        _googlePlayGamesLeaderboardConfirmMenuImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateText.enabled = false;
        _googlePlayGamesLeaderboardLogInCheckText.enabled = false;
        
        if(m_isGooglePlayGamesLeaderboardTestMode)
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

            if(m_googlePlayGamesProfilePicImage.sprite != null)
            {
                m_googlePlayGamesProfilePicImage.enabled = true;
                m_googlePlayGamesUsernameText.enabled = true;
                m_noProfilePicText.enabled = false;
                m_noUsernameText.enabled = false;
            }
            else
            {
                m_googlePlayGamesProfilePicImage.enabled = false;
                m_googlePlayGamesUsernameText.enabled = false;
                m_noProfilePicText.enabled = true;
                m_noUsernameText.enabled = true;
            }
        }

        if(m_isGooglePlayGamesLogInTestMode)
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
            m_googlePlayGamesUsernameText.enabled = false;
        }

        if(_googlePlayGamesLogInButtonTapped && m_isGooglePlayGamesLogInTestMode)
        {
            m_googlePlayGamesLogInTestText.text = "Log In Failed :(";
            _googlePlayGamesLogInButtonTapped = false;
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
        _googlePlayGamesLogInButtonTapped = true;
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

        m_isGooglePlayGamesLoggedIn = success;
    }

    void GooglePlayGamesLogInCheck()
    {
        if(m_isGooglePlayGamesLoggedIn)
        {
            m_googlePlayGamesProfilePicImage.sprite = Sprite.Create(PlayGamesPlatform.Instance.localUser.image , new Rect(0 , 0 , 50 , 50) , new Vector2(0 , 0)); //TODO Pivot value may be adjusted so pic looks perfect in center
            m_googlePlayGamesUsernameText.text = PlayGamesPlatform.Instance.localUser.userName;
            GooglePlayGamesLoggedIn();
        }
        else
        {
            GooglePlayGamesLoggedOut();  
        }  
    }

    public void GooglePlayRateButton()
    {
        Application.OpenURL("http://uabhanu.wixsite.com/portfolio"); //TODO Game Play Store URL here after it's live
    }
}
