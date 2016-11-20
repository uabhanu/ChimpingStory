using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	public GameObject backgroundSound;
	public GameObject bananaSound;
	public GameObject deathSound;
	public GameObject jumpSound;
	public GameObject muteButton;
	public GameObject unmuteButton;

	public void Mute() 
	{
		backgroundSound.SetActive(false);
		bananaSound.SetActive(false);
		deathSound.SetActive(false);
		jumpSound.SetActive(false);
		muteButton.SetActive(false);
		unmuteButton.SetActive(true);
	}

	public void Unmute() 
	{
		backgroundSound.SetActive(true);
		bananaSound.SetActive(true);
		deathSound.SetActive(true);
		jumpSound.SetActive(true);
		muteButton.SetActive(true);
		unmuteButton.SetActive(false);
	}
}
