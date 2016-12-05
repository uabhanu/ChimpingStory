//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public int bananaScoreValue;
	public int superChimpScoreValue;
	public int trophiesScoreValue;
	public GameManager gameManagementScript;
	public string trophiesLeaderboard;
	public Text bananaScoreLabel;
	public Text superChimpScoreLabel;
	public Text trophiesScoreLabel;

	void Start() 
	{
		if(PlayerPrefs.HasKey("BananaScore"))
		{
			bananaScoreValue = PlayerPrefs.GetInt("BananaScore");
		}

		if(PlayerPrefs.HasKey("MonkeynutScore"))
		{
			superChimpScoreValue = PlayerPrefs.GetInt("MonkeynutScore");
		}

		if(PlayerPrefs.HasKey("TrophiesScore"))
		{
			//Debug.Log("Retrieve Score from PlayerPrefs"); //Working
			trophiesScoreValue = PlayerPrefs.GetInt("TrophiesScore"); //Do not forget to use this for final version of game
		}

		//StartCoroutine("ScoreFromLeaderboard"); //After you got this working but for now, not needed
	}

	IEnumerator ScoreFromLeaderboard()
	{
		yield return new WaitForSeconds(0.1f);

		if(Social.localUser.authenticated) 
		{
			Debug.Log("Login Success");

//			PlayGamesPlatform.Instance.LoadScores(trophiesLeaderboard , LeaderboardStart.PlayerCentered , 1 , LeaderboardCollection.Public , LeaderboardTimeSpan.AllTime , (LeaderboardScoreData lsd) => 
//			{
//				Debug.Log("Retrieve Score from Leaderboard");
//			});	
		} 
		else
		{
			if(PlayerPrefs.HasKey("TrophiesScore"))
			{
				Debug.Log("Retrieve Score from PlayerPrefs");
				trophiesScoreValue = PlayerPrefs.GetInt("TrophiesScore"); //Do not forget to use this for final version of game
			}
		}

		StartCoroutine("ScoreFromLeaderboard");
	}

	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
		bananaScoreLabel.text = "" + bananaScoreValue;

		if(superChimpScoreLabel != null)
		{
			superChimpScoreLabel.text = "" + superChimpScoreValue;
			PlayerPrefs.SetInt("MonkeynutScore" , superChimpScoreValue);
		}

		if(trophiesScoreLabel != null)
		{
			trophiesScoreLabel.text = " " + trophiesScoreValue;
			PlayerPrefs.SetInt("TrophiesScore" , trophiesScoreValue);
		}

		PlayerPrefs.SetInt("BananaScore" , bananaScoreValue);
	}
}
