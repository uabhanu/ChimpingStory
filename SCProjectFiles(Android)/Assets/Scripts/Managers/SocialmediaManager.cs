using Facebook.Unity;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SocialmediaManager : MonoBehaviour 
{
    bool _googlePlayGamesLogInButtonTapped , _googlePlayGamesLogInComplete;
    //Dictionary<string , object> _highScoresData;
    //Dictionary<string , string> _scores = null;
    //float _highScore;
	Image _googlePlayGamesLeaderboardConfirmMenuImage , _googlePlayGamesLeaderboardOKButtonImage , _googlePlayGamesLeaderboardSuccessOKButtonImage , _googlePlayGamesLeaderboardUpdateAcceptButtonImage , _googlePlayGamesLeaderboardUpdateCancelButtonImage;
    int _currentScene;
    PlayGamesUserProfile _playerProfile;
    string _applinkURL , _leaderboardID = "CgkInMKFu8wYEAIQAQ";
	Text _googlePlayGamesLeaderboardUpdateText;

    [SerializeField] GameObject /*_fbChallengeInviteSuccessMenuObj , */_facebookShareMenuObj , _facebookLoggedInObj , _facebookLoggedOutObj , _googlePlayGamesLoggedInObj , _googlePlayGamesLoggedOutObj;
    [SerializeField] Image _facebookShareButtonImage /*, _fbChallengeInviteButtonImage , */;
    [SerializeField] Text /*_fbChallengeInviteTestText, _fbScoreText, */_googlePlayGamesLeaderboardLogInCheckText , _googlePlayGamesLeaderboardTestText , _googlePlayGamesLeaderboardUpdateSuccessText;

    public static bool m_facebookProfilePicEnabled = false , m_isFacebookShareTestMode = false , m_isGooglePlayGamesLeaderboardTestMode = false , m_isGooglePlayGamesLogInTestMode = false;
    public static Button m_googlePlayGamesLeaderboardButton;
    public static GameObject m_facebookShareSuccessMenuObj , m_facebookShareTestMenuObj;
    public static Image m_facebookButtonImage , m_facebookProfilePicImage , m_googlePlayGamesLeaderboardButtonImage , m_googlePlayGamesLeaderboardTestGetButtonImage , m_googlePlayGamesLeaderboardTestMenuImage;
    public static Image m_googlePlayGamesLeaderboardTestSetButtonImage , m_googlePlayGamesLogInButtonImage , m_googlePlayRateButtonImage , m_googlePlayGamesProfilePicImage;
    public static Text m_facebookUsernameText , m_googlePlayGamesLeaderboardTestText , m_googlePlayGamesLogInTestText , m_googlePlayGamesUsernameText , m_noInternetText , m_noProfilePicText , m_noUsernameText;

    void Start()
	{
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _googlePlayGamesLogInComplete = false;
        //m_isGooglePlayGamesLeaderboardTestMode = true; //TODO Remove this after testing is finished

        if(_currentScene == 0)
        {
            m_facebookButtonImage = GameObject.Find("FacebookButton").GetComponent<Image>();
            m_facebookProfilePicImage = GameObject.Find("FacebookProfilePicImage").GetComponent<Image>();
            m_facebookShareSuccessMenuObj = GameObject.Find("FBShareSuccessMenu");
            m_facebookShareTestMenuObj = GameObject.Find("FBShareTestMenu");
            m_facebookUsernameText = GameObject.Find("FacebookUsernameText").GetComponent<Text>();
            m_googlePlayGamesLogInButtonImage = GameObject.Find("GPGsLogInButton").GetComponent<Image>();
            m_googlePlayGamesProfilePicImage = GameObject.Find("GPGsProfilePicImage").GetComponent<Image>();
            m_googlePlayGamesUsernameText = GameObject.Find("GPGsUsernameText").GetComponent<Text>();
            m_googlePlayRateButtonImage = GameObject.Find("GPGsRateButton").GetComponent<Image>();
            m_noInternetText = GameObject.Find("NoInternetText").GetComponent<Text>();
            m_noProfilePicText = GameObject.Find("NoProfilePicText").GetComponent<Text>();
            m_noUsernameText = GameObject.Find("NoUsernameText").GetComponent<Text>();
            FacebookInit();
            GooglePlayGamesInit();
            Invoke("FacebookLogInCheck" , 0.2f);
            Invoke("GooglePlayGamesLogInCheck" , 0.2f);
        }

        else if(_currentScene == 1)
        {
            m_googlePlayGamesLeaderboardButton = GameObject.Find("GPGsLeaderboardButton").GetComponent<Button>();
            m_googlePlayGamesLeaderboardButtonImage = GameObject.Find("GPGsLeaderboardButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardConfirmMenuImage = GameObject.Find("GPGsLeaderboardConfirmMenu").GetComponent<Image>();
            _googlePlayGamesLeaderboardOKButtonImage = GameObject.Find("OKButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardSuccessOKButtonImage = GameObject.Find("SuccessOKButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestMenuImage = GameObject.Find("GPGsLeaderboardTestMenu").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestGetButtonImage = GameObject.Find("TestGetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestSetButtonImage = GameObject.Find("TestSetButton").GetComponent<Image>();
            m_googlePlayGamesLeaderboardTestText = GameObject.Find("TestText").GetComponent<Text>();
            _googlePlayGamesLeaderboardUpdateAcceptButtonImage = GameObject.Find("UpdateAcceptButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateCancelButtonImage = GameObject.Find("UpdateCancelButton").GetComponent<Image>();
            _googlePlayGamesLeaderboardUpdateText = GameObject.Find("UpdateText").GetComponent<Text>();
        }
    }

    void FacebookAppLinkURL(IAppLinkResult applinkResult) //Not sure how to use this yet so not using for now
    {
        if(!string.IsNullOrEmpty(applinkResult.Url))
        {
            _applinkURL = "" + applinkResult.Url + "";
            Debug.Log(_applinkURL);
        }
        else
        {
            _applinkURL = "http://uabhanu.wixsite.com/portfolio";
        }
    }

    public void FacebookChallengePlayers()
    {
        FB.AppRequest
        (
            "Come and try to be the Chimpion :) " ,
            null ,
            new List<object> { "app_users" } ,
            null ,
            null ,
            null ,
            null ,
            FacebookChallengePlayersCallback
        );
    }

    void FacebookChallengePlayersCallback(IAppRequestResult appRequestResult)
    {
        if(appRequestResult.Cancelled)
        {
            //Debug.LogWarning("Sir Bhanu, You have cancelled the invite");
        }

        else if(!string.IsNullOrEmpty(appRequestResult.Error))
        {
            //Debug.LogError("Sir Bhanu, There is a problem : " + appRequestResult.Error);
        }

        else if(!string.IsNullOrEmpty(appRequestResult.RawResult))
        {
            //Debug.LogWarning("Sir Bhanu, Your invitation : " + appRequestResult.RawResult);

            if(!m_isFacebookShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }

            //_fbChallengeInviteSuccessMenuObj.SetActive(true);
        }
    }

    void FacebookInit()
    {
        if(!FB.IsInitialized)
        {
            HideUnityDelegate FBOnHideUnity = null;
            InitDelegate FBSetInit = null;
            FB.Init(FBSetInit , FBOnHideUnity);
        }
    }

    public void FacebookInviteOKButton()
    {
        //_fbChallengeInviteSuccessMenuObj.SetActive(false);
    }

    void FacebookLoggedIn()
    {
        FB.API("/me?fields=first_name" , HttpMethod.GET , FacebookUsernameDisplay);
        FB.API("/me/picture?type=square&height=480&width=480" , HttpMethod.GET , FacebookProfilePicDisplay);

        if(_facebookLoggedInObj != null && _facebookLoggedOutObj != null)
        {
            _facebookLoggedInObj.SetActive(true);
            m_facebookProfilePicImage.enabled = true;
            m_facebookUsernameText.enabled = true;
            _facebookLoggedOutObj.SetActive(false);
        }
        else
        {
            Debug.LogError("Sir Bhanu, FB Logged In & Out Objs no longer exist but you can ignore them :)");
        }
    }

    void FacebookLoggedOut()
	{
		if(_facebookLoggedInObj != null && _facebookLoggedOutObj != null)
        {
            _facebookLoggedInObj.SetActive(false);
		    _facebookLoggedOutObj.SetActive(true);		
        }
        else
        {
            Debug.LogError("Sir Bhanu, Facebook Logged In & Out Objs no longer exist but you can ignore them :)");
        }
	}

    void FacebookLogIn()
    {
        if(FB.IsInitialized)
        {
            m_noInternetText.enabled = false;
            List<string> permissions = new List<string>();
            FB.LogInWithReadPermissions(permissions , FacebookLogInCallBack);
        }
    }

    public void FacebookLoginButton()
    {
        FacebookLogIn();
        Invoke("FacebookLoggedIn" , 0.2f);
    }

    void FacebookLogInCallBack(IResult logInResult)
    {
        if(logInResult.Cancelled)
        {
            Debug.LogWarning("Sir Bhanu, You have cancelled the LogIn" + logInResult.RawResult);
            FacebookLoggedOut();
        }

        else if(!string.IsNullOrEmpty(logInResult.Error))
        {
            if(logInResult.RawResult.Contains("Error button pressed"))
            {
                Debug.LogWarning("Sir Bhanu, You have pressed Error Button" + logInResult.RawResult);
                FacebookLoggedOut();
            }
            else
            {
                Debug.LogWarning("Sir Bhanu, Please check your internet connection" + logInResult.RawResult);
                m_noInternetText.enabled = true;
                FacebookLoggedOut();
            }
        }

        else if(!string.IsNullOrEmpty(logInResult.RawResult))
        {
            Debug.LogWarning("Sir Bhanu, Your LogIn : " + logInResult.RawResult);
            FacebookLoggedIn();
        }
    }

    void FacebookLogInCheck()
    {
        if(FB.IsLoggedIn)
        {
            FacebookLoggedIn();
        }
        else
        {
            FacebookLoggedOut();
            Invoke("FacebookLogInCheck" , 0.2f);
        }
    }

    void FacebookOnHideUnity(bool isGameShown)
	{
		if(!isGameShown) 
		{
			Time.timeScale = 0;
		} 
		else 
		{
			Time.timeScale = 1;	
		}
	}

    void FacebookProfilePicDisplay(IGraphResult graphicResult)
    {
        try
        {
            if(graphicResult.Texture != null && graphicResult.Error == null)
            {
                m_facebookProfilePicImage.sprite = Sprite.Create(graphicResult.Texture , new Rect(0 , 0 , graphicResult.Texture.width , graphicResult.Texture.height) , new Vector2());
                m_facebookProfilePicEnabled = true; //This is used to check if sprite created properly and display only if it is, or else, _profilePicImage won't be enabled
                m_facebookProfilePicImage.enabled = true;
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    void FacebookSetInit()
    {
        if(FB.IsLoggedIn)
        {
            FacebookLoggedIn();
        }
        else
        {
            FacebookLoggedOut();
        }
    }

    public void FacebookShareButton()
    {
        FB.ShareLink
        (
            contentTitle: "Fourth Lion Studios Message" ,
            contentURL: new Uri("http://uabhanu.wixsite.com/portfolio") , //TODO Game URL here when Live
            contentDescription: "We really hope you love the game" ,
            callback: FacebookShareCallback
        );
    }

    void FacebookShareCallback(IShareResult shareResult)
    {
        if(shareResult.Cancelled)
        {
            Debug.LogWarning("Sir Bhanu, you have cancelled the Share :)");
        }

        else if(!string.IsNullOrEmpty(shareResult.Error))
        {
            Debug.LogError("Sir Bhanu, You have pressed error button");
        }

        else
        {
            Debug.LogWarning("Sir Bhanu, Your Share is a success :)");

            if(!m_isFacebookShareTestMode)
            {
                ScoreManager.m_supersCount++;
                BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
            }

            m_facebookShareSuccessMenuObj.SetActive(true);
            m_googlePlayGamesLogInButtonImage.enabled = false;
            m_googlePlayGamesLogInTestText.enabled = false;
            m_googlePlayRateButtonImage.enabled = false;
        }
    }

    public void FacebookShareOKButton()
    {
        if(m_facebookShareSuccessMenuObj != null)
        {
           m_facebookShareSuccessMenuObj.SetActive(false);
        }
        
        if(m_googlePlayGamesLogInButtonImage != null)
        {
            m_googlePlayGamesLogInButtonImage.enabled = true;
        }
        
        if(m_isGooglePlayGamesLogInTestMode && m_googlePlayGamesLogInTestText != null)
        {
            m_googlePlayGamesLogInTestText.enabled = true;
        }

        if(m_googlePlayRateButtonImage != null)
        {
            m_googlePlayRateButtonImage.enabled = true;
        }
    }

    void FacebookUsernameDisplay(IResult usernameResult)
    {
        if(usernameResult.Error == null && GameManager.m_currentScene == 0)
        {
            m_facebookUsernameText.text = "Hi " + usernameResult.ResultDictionary["first_name"];
        }
        else
        {
            FacebookLoggedOut();
        }
    }

    void GooglePlayGamesInit()
    {
        _googlePlayGamesLogInButtonTapped = false;
        m_noInternetText.enabled = false;
        PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(clientConfig);
        PlayGamesPlatform.Activate();
    }

    public void GooglePlayGamesLeaderboardButton()
    {
        if(!GameManager._adsMenuImage.enabled && !_googlePlayGamesLeaderboardLogInCheckText.enabled)
        {
            _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = true;
            _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = true;
            _googlePlayGamesLeaderboardConfirmMenuImage.enabled = true;
            _googlePlayGamesLeaderboardUpdateText.enabled = true;
            
            if(m_isGooglePlayGamesLeaderboardTestMode)
            {
                GooglePlayGamesLeaderboardTestMenuDisappear();
            }

            GameManager._pauseButtonImage.enabled = false;
            Time.timeScale = 0;
        }
    }

    public void GooglePlayGamesLeaderboardOKButton()
    {
        _googlePlayGamesLeaderboardConfirmMenuImage.enabled = false;
        _googlePlayGamesLeaderboardLogInCheckText.enabled = false;
        _googlePlayGamesLeaderboardOKButtonImage.enabled = false;
        Time.timeScale = 1;
    }

    public void GooglePlayGamesLeaderboardScoreGet()
    {
        if(m_isGooglePlayGamesLeaderboardTestMode)
        {
            _googlePlayGamesLeaderboardTestText.text = ScoreManager.m_myScores;
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
        _googlePlayGamesLeaderboardConfirmMenuImage.enabled = false;
        _googlePlayGamesLeaderboardSuccessOKButtonImage.enabled = false;
        _googlePlayGamesLeaderboardUpdateSuccessText.enabled = false;
        Time.timeScale = 1;
    }

    public static void GooglePlayGamesLeaderboardTestMenuAppear()
    {
        if(m_isGooglePlayGamesLeaderboardTestMode)
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
        if(PlayGamesPlatform.Instance.localUser.authenticated) 
        {
            PlayGamesPlatform.Instance.ReportScore((long)ScoreManager.m_scoreValue , _leaderboardID , (bool success) =>
            {
                _googlePlayGamesLeaderboardLogInCheckText.text = "Score Update : " + success;
                _googlePlayGamesLeaderboardConfirmMenuImage.enabled = true;
                _googlePlayGamesLeaderboardSuccessOKButtonImage.enabled = true;
                _googlePlayGamesLeaderboardUpdateSuccessText.enabled = true;
                _googlePlayGamesLeaderboardUpdateAcceptButtonImage.enabled = false;
                _googlePlayGamesLeaderboardUpdateCancelButtonImage.enabled = false;
                _googlePlayGamesLeaderboardUpdateText.enabled = false;
            });
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

        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void GooglePlayGamesLeaderboardUpdateCancelButton()
    {
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

        GameManager._pauseButtonImage.enabled = true;
        Time.timeScale = 1;
    }

    void GooglePlayGamesLoggedIn()
    {
        if(_googlePlayGamesLoggedInObj != null && _googlePlayGamesLoggedOutObj != null)
        {
            _googlePlayGamesLoggedInObj.SetActive(true);
            _googlePlayGamesLoggedOutObj.SetActive(false);
            m_googlePlayRateButtonImage.enabled = true;
            m_googlePlayGamesProfilePicImage.sprite = Sprite.Create(Social.localUser.image , new Rect(0 , 0 , 50 , 50) , new Vector2(0 , 0)); //TODO Pivot value may be adjusted

            if(m_googlePlayGamesProfilePicImage.sprite != null)
            {
                _googlePlayGamesLogInComplete = true;
                m_googlePlayGamesProfilePicImage.enabled = true;
                m_noProfilePicText.enabled = false;
            }
            else
            {
                _googlePlayGamesLogInComplete = false;
                m_noProfilePicText.enabled = true;
            }

            m_googlePlayGamesUsernameText.text = Social.localUser.userName;

            if(m_googlePlayGamesUsernameText.text == null)
            {
                m_googlePlayGamesUsernameText.enabled = false;
                m_noUsernameText.enabled = true;
            }
            else
            {
                m_googlePlayGamesUsernameText.enabled = true;
                m_noUsernameText.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Sir Bhanu, GPGs Logged In & Logged Out Objs no longer exist but you can ignore them :) ");
        }

        if(m_isGooglePlayGamesLogInTestMode)
        {
            m_googlePlayGamesLogInTestText.text = "Log In Success :)";
        }
    }

    void GooglePlayGamesLoggedOut()
    {
        if(_googlePlayGamesLoggedInObj != null && _googlePlayGamesLoggedOutObj != null)
        {
            _googlePlayGamesLoggedInObj.SetActive(false);
            _googlePlayGamesLoggedOutObj.SetActive(true);
        }
        else
        {
            Debug.LogError("Sir Bhanu, GPGs Logged In & Logged Out Objs no longer exist but you can ignore them :) ");
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
    }

    void GooglePlayGamesLogInCheck()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated)
        {
            GooglePlayGamesLoggedIn();

            if(!_googlePlayGamesLogInComplete)
            {
                Invoke("GooglePlayGamesLogInCheck" , 0.2f);
            }
        }
        else
        {
            GooglePlayGamesLoggedOut();
            
            if(!_googlePlayGamesLogInComplete)
            {
                Invoke("GooglePlayGamesLogInCheck" , 0.2f);
            }
        }
    }

    public void GooglePlayRateButton()
    {
        Application.OpenURL("http://uabhanu.wixsite.com/portfolio"); //TODO Game Play Store URL here after it's live
    }
}
