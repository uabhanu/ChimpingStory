//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public int m_bananasCollected = 0 , m_superChimpScoreValue , m_trophiesScoreValue;
	//public string m_trophiesLeaderboard;
	public Text m_bananaScoreLabel , m_superChimpScoreLabel , m_trophiesScoreLabel;

	void Start() 
	{
        if(PlayerPrefs.HasKey("BananasCollected"))
        {
            m_bananasCollected = PlayerPrefs.GetInt("BananasCollected");
        }

		if(PlayerPrefs.HasKey("MonkeynutScore"))
		{
			m_superChimpScoreValue = PlayerPrefs.GetInt("MonkeynutScore");
		}

		if(PlayerPrefs.HasKey("TrophiesScore"))
		{
			//Debug.Log("Retrieve Score from PlayerPrefs"); //Working
			m_trophiesScoreValue = PlayerPrefs.GetInt("TrophiesScore"); //Do not forget to use this for final version of game
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
				m_trophiesScoreValue = PlayerPrefs.GetInt("TrophiesScore"); //Do not forget to use this for final version of game
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
			
		m_bananaScoreLabel.text = "" + m_bananasCollected;

		if(m_superChimpScoreLabel != null)
		{
			m_superChimpScoreLabel.text = "" + m_superChimpScoreValue;
			PlayerPrefs.SetInt("MonkeynutScore" , m_superChimpScoreValue);
		}

		if(m_trophiesScoreLabel != null)
		{
			m_trophiesScoreLabel.text = " " + m_trophiesScoreValue;
			PlayerPrefs.SetInt("TrophiesScore" , m_trophiesScoreValue);
		}

        PlayerPrefs.SetInt("BananasCollected", m_bananasCollected);
    }
}
