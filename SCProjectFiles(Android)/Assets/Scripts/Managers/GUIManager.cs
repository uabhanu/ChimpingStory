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
	GameManager m_gameManager;
	int collectedCoins = 0 , distanceTraveled = 0;

	public Animator overlayAnimator;                        //The overlay animator
	public LevelManager levelManager;                       //A link to the level manager
    public PlayerManager playerManager;                     //A link to the player manager
    public PowerupManager powerupManager;                   //A link to the powerup manager

	IEnumerator LandRunnerRoutine()
	{
		yield return new WaitForSeconds(m_landRunnerTime);
		m_gameManager.BackToLandWinMenu();
	}

    public void BackToLandLose(int distance)
    {
		m_gameManager.BackToLandLoseMenu();
    }
		    
    public void PlayTrigger(Image arrowImage)
    {
		m_gameManager = FindObjectOfType<GameManager>();

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