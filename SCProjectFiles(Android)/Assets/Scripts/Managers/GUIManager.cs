using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    AudioSource m_musicSource;
	bool[] usedMissionNotifications = new bool[]{false , false , false};
	bool canPause = true , inPlayMode = false;
	float m_landRunnerTime = 30f;
	Image m_backToLandLoseMenuImage , m_backToLandWinMenuImage , m_continueButtonImage , m_pauseButtonImage , m_pauseMenuImage , m_resumeButtonImage;
	int collectedCoins = 0 , distanceTraveled = 0;
    Text m_backToLandLose , m_backToLandWin , m_highScoreTextDisplay , m_highScoreValueDisplay;

	public Animator overlayAnimator;                        //The overlay animator
	public LevelManager levelManager;                       //A link to the level manager
    public PlayerManager playerManager;                     //A link to the player manager
    public PowerupManager powerupManager;                   //A link to the powerup manager

    void Start()
    {
        BhanuElements();
    }

	IEnumerator LandRunnerRoutine()
	{
		yield return new WaitForSeconds(m_landRunnerTime);
		BackToLandWin();
	}

    public void BackToLandLose(int distance)
    {
        m_backToLandLose.enabled = true;
        m_backToLandLoseMenuImage.enabled = true;
        m_continueButtonImage.enabled = true;

        m_highScoreTextDisplay.enabled = false;
        m_highScoreValueDisplay.enabled = false;
        m_pauseButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWin()
    {
		m_backToLandWin.enabled = true;
		m_backToLandWinMenuImage.enabled = true;
		m_continueButtonImage.enabled = true;

		m_highScoreTextDisplay.enabled = false;
		m_highScoreValueDisplay.enabled = false;
		m_pauseButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void BhanuContinue()
    {
        SceneManager.LoadScene("LandRunner");
        Time.timeScale = 1;
    }

    void BhanuElements()
    {
        m_backToLandLose = GameObject.Find("BackToLandLose").GetComponent<Text>();
        m_backToLandLoseMenuImage = GameObject.Find("BackToLandLoseMenu").GetComponent<Image>();
		m_continueButtonImage = GameObject.Find("ContinueButton").GetComponent<Image>();
        m_backToLandWin = GameObject.Find("BackToLandWin").GetComponent<Text>();
        m_backToLandWinMenuImage = GameObject.Find("BackToLandWinMenu").GetComponent<Image>();
        m_highScoreTextDisplay = GameObject.Find("HighScoreTextDisplay").GetComponent<Text>();
        m_highScoreValueDisplay = GameObject.Find("HighScoreValueDisplay").GetComponent<Text>();
        m_pauseButtonImage = GameObject.Find("PauseButton").GetComponent<Image>();
        m_pauseMenuImage = GameObject.Find("PauseMenu").GetComponent<Image>();
        m_resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
        m_musicSource = GameObject.FindGameObjectWithTag("AVManager").GetComponent<AudioSource>();
    }

    public void BhanuPause()
    {
        if(m_musicSource != null)
        {
            m_musicSource.Pause();
        }

        m_pauseButtonImage.enabled = false;

        m_pauseMenuImage.enabled = true;
        m_resumeButtonImage.enabled = true;
        Time.timeScale = 0;
    }

    public void BhanuResume()
    {
        if(m_musicSource != null)
        {
            m_musicSource.Play();
        }

        m_pauseButtonImage.enabled = true;

        m_pauseMenuImage.enabled = false;
        m_resumeButtonImage.enabled = false;
        Time.timeScale = 1;
    }
		    
    public void PlayTrigger(Image arrowImage)
    {
        if(!inPlayMode)
        {
            inPlayMode = true;
            overlayAnimator.SetBool("Visible" , false);
            levelManager.StartLevel();
			StartCoroutine("LandRunnerRoutine");
        }
    }
     
    public void ShowPowerup(string name)
    {
        switch(name)
        {
//            case "ExtraSpeed":
//                SaveManager.extraSpeed += 1;
//                powerupButtons[0].SetBool("Visible", true);
//                break;
//
//            case "Shield":
//                SaveManager.shield += 1;
//                powerupButtons[1].SetBool("Visible", true);
//                break;
//
//            case "SonicBlast":
//                SaveManager.sonicWave += 1;
//                powerupButtons[2].SetBool("Visible", true);
//                break;

            case "Revive":
                SaveManager.revive += 1;
            break;
        }
    }
		
    public bool InPlayMode()
    {
        return inPlayMode;
    }
}