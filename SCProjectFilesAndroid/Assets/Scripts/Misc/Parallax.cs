using UnityEngine;

public class Parallax : MonoBehaviour
{
<<<<<<< HEAD
    //TODO This is causing backgrounds to overlap. Try applying this script to children instead and all children with the same parallax offset. Also, try making EndPosition child of it's respective object
=======
    //TODO This is causing backgrounds to overlap. Try applying this script to children instead and all children with the same parallax offset
>>>>>>> be3a1d40a557d9daa5589c5138bad2862168f264
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
