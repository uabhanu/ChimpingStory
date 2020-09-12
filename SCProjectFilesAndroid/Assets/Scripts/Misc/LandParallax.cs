using UnityEngine;

public class LandParallax : MonoBehaviour
{
    //TODO Figure out a way to convert this into just Parallax so can be used in all the levels and can take SerializeField variable Direction
    //TODO Once this is done, use Parallax for Space Level too or use it anyway even if converting into Parallax fails
    private LandPuss _landPussReference;

    [SerializeField] private float _parallaxOffset;
    
    private void Start()
    {
        _landPussReference = FindObjectOfType<LandPuss>();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        transform.Translate(Vector2.right * _landPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
    }
}
