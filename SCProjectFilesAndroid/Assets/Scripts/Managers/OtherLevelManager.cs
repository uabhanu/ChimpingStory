using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OtherLevelManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO _gameManagerSO;
    [SerializeField] private Text _countdownDisplayText;

    private void Start()
    {
        _gameManagerSO.GetOtherLevelObjects();   
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(_gameManagerSO.m_CountdownValue > 0)
        {
            _gameManagerSO.m_CountdownValue -= Time.deltaTime;
        }
    
        _countdownDisplayText.text = Mathf.Round(_gameManagerSO.m_CountdownValue).ToString();

        if(_gameManagerSO.m_CountdownValue == 0)
        {
            EventsManager.InvokeEvent(SelfiePussEvent.CountdownFinished);
        }
    }

    private void OnCountdownFinished()
    {
        SceneManager.LoadScene("LandRunner"); //TODO This is executing before the value becoming zero and upon colliding with coin of water level and meteor of space level so fixing WIP
    }

    private void RegisterEvents()
    {
        EventsManager.SubscribeToEvent(SelfiePussEvent.ScoreChanged , OnCountdownFinished);
    }

    private void UnregisterEvents()
    {
        EventsManager.UnsubscribeFromEvent(SelfiePussEvent.ScoreChanged , OnCountdownFinished);
    }
}
