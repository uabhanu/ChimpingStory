using SelfiePuss.Events;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu]
public class GameManagerSO : ScriptableObject 
{
    //TODO Text Mesh Pro
    //private int _chimpionshipsCount , _currentChimpion; TODO This is for future use

    private bool _bIsUI;

    [SerializeField] private bool _isUICheckOkToDo;
    [SerializeField] private float _countdownValue;
    [SerializeField] private Sprite[] _chimpionshipBeltSprites;
    [SerializeField] private string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;

    public bool _bisUnityEditorTestingMode , _bSelfieFlashEnabled;

    public float GetCountDownValue()
    {
        return _countdownValue;
    }

    public void AdsUI()
    {
        EventsManager.InvokeEvent(SelfiePussEvent.AdsUI);
    }

    public bool UICheck()
    {
        if(_isUICheckOkToDo)
        {
            if(EventSystem.current.currentSelectedGameObject != null)
            {
                _bIsUI = true;
            }

            else if(EventSystem.current.currentSelectedGameObject == null)
            {
                _bIsUI = false;
            }
        }

        return _bIsUI;
    }
}
