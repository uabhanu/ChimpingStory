using UnityEngine;
using UnityEngine.SceneManagement;

public class LandParallax : MonoBehaviour
{
    private int _sceneIndex;
    private LandPuss _landPussReference;

    [SerializeField] private float _parallaxOffset;
    
    private void Start()
    {
        _landPussReference = FindObjectOfType<LandPuss>();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //TODO There is no object in the Water Level Scene using this script but somehow this is being called so put this check as a temporary measure

        if(_sceneIndex == 1)
        {
            transform.Translate(Vector2.right * _landPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
        }
    }
}
