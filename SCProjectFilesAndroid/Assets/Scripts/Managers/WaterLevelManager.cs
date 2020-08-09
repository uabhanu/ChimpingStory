using UnityEngine;
using UnityEngine.UI;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] GameObject _inGameUIObj , _otherUIObj;
    
    public Text m_HighScoreValueText;
    void Start()
    {
        _otherUIObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void FirstTimeTutorialOKButton()
    {
        _inGameUIObj.SetActive(true);
        _otherUIObj.SetActive(false);
        Time.timeScale = 1;
    }
}
