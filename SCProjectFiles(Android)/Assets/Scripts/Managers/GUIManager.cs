using System.Collections;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    AudioSource m_musicSource;
	bool inPlayMode = false;
	GameManager m_gameManager;
    WaitForSeconds m_landRunnerRoutineDelay = new WaitForSeconds(30f);

	public Animator overlayAnimator;                        //The overlay animator
	public LevelManager levelManager;                       //A link to the level manager
    public PlayerManager playerManager;                     //A link to the player manager
    public PowerupManager powerupManager;                   //A link to the powerup manager

    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inPlayMode = true;
        overlayAnimator.SetBool("Visible" , false);
        levelManager.StartLevel();
		StartCoroutine("LandRunnerRoutine");
    }

    IEnumerator LandRunnerRoutine()
	{
		yield return m_landRunnerRoutineDelay;
		m_gameManager.BackToLandWinMenu();
	}

    public void BackToLandLose(int distance)
    {
		m_gameManager.BackToLandLoseMenu();
    }
		    	
    public bool InPlayMode()
    {
        return inPlayMode;
    }
}