using UnityEngine;

public class Parallax : MonoBehaviour
{
    //TODO This is causing backgrounds to overlap. Try applying this script to children instead and all children with the same parallax offset. Also, try making EndPosition child of it's respective object
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
