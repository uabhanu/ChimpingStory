using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OtherLevelManager : MonoBehaviour
{
    [SerializeField] private float _countDownTimer;
    
    [SerializeField] private GameManagerSO _gameManagerSO;
    [SerializeField] private Text _countdownDisplayText;

    private void Start()
    {
        _gameManagerSO.GetReferences();
        _countDownTimer = _gameManagerSO.GetCountDownValue();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_countDownTimer > 0)
        {
            _countDownTimer -= Time.deltaTime;
        }
    
        _countdownDisplayText.text = Mathf.Round(_countDownTimer).ToString();

        if(_countDownTimer <= 0)
        {
            SceneManager.LoadScene("LandRunner");
        }
    }
}
