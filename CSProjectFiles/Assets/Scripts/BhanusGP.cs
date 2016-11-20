using GooglePlayGames;
using SVGImporter;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BhanusGP : MonoBehaviour 
{
	public SVGImage signInButtonImage , signOutButtonImage , signOutImage;

	void Awake()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}

	void Start()
	{
		Social.localUser.Authenticate((bool success) => 
		{
			if(success) 
			{
				Debug.Log ("Login Success");
				signInButtonImage.enabled = false;
				signOutButtonImage.enabled = true;
				signOutImage.enabled = true;
			}

			if(!success)
			{
				Debug.Log ("Login Failed");
				signInButtonImage.enabled = true;
				signOutButtonImage.enabled = false;
				signOutImage.enabled = false;
			}
		});
	}

	public void SignIn()
	{
		Social.localUser.Authenticate((bool success) => 
		{
			if(success) 
			{
				Debug.Log ("Login Success");
				signInButtonImage.enabled = false;
				signOutButtonImage.enabled = true;
				signOutImage.enabled = true;
			}

			if(!success)
			{
				Debug.Log ("Login Failed");
				signInButtonImage.enabled = true;
				signOutButtonImage.enabled = false;
				signOutImage.enabled = false;
			}
		});
	}

	public void SignOut()
	{
		PlayGamesPlatform.Instance.SignOut(); //Or ((PlayGamesPlatform) Social.Active).SignOut();

		if(!Social.localUser.authenticated)
		{
			signInButtonImage.enabled = true;
			signOutButtonImage.enabled = false;
			signOutImage.enabled = false;
		}
	}
}
