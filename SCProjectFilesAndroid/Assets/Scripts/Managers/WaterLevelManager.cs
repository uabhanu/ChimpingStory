using UnityEngine;
using UnityEngine.UI;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] GameObject _inGameMenuObj , _pauseMenuObj;
    
    public Text m_HighScoreValueText;
    void Start()
    {
        
    }

    public void PauseButton()
    {
        _inGameMenuObj.SetActive(false);
        _pauseMenuObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        _inGameMenuObj.SetActive(true);
        _pauseMenuObj.SetActive(false);
        Time.timeScale = 1;
    }
}
