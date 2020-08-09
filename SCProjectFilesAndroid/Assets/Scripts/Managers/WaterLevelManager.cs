using UnityEngine;
using UnityEngine.UI;

public class WaterLevelManager : MonoBehaviour
{
    [SerializeField] GameObject _firstTimeTutorialObj;
    
    public Text m_HighScoreValueText;
    void Start()
    {
        Time.timeScale = 0;
    }

    public void FirstTimeTutorialOKButton()
    {
        _firstTimeTutorialObj.SetActive(false);
        Time.timeScale = 1;
    }
}
