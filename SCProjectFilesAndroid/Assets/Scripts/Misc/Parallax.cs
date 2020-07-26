using UnityEngine;

public class Parallax : MonoBehaviour
{
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

        transform.Translate(-Vector2.left * _landPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.Self);
    }
}
