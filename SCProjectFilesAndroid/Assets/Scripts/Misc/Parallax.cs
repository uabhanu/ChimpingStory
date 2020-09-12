using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset;
    [SerializeField] private string _pussTypeToLookFor;

    [SerializeField] private LandPuss _landPussReference;
    [SerializeField] private SuperPuss _superPussReference;
    [SerializeField] private WaterPuss _waterPussReference;

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        switch(_pussTypeToLookFor)
        {
            case "Land Puss":
                transform.Translate(Vector2.right * _landPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
            break;

            case "Super Puss":
                transform.Translate(Vector2.right * _superPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
            break;

            case "Water Puss":
                transform.Translate(Vector2.left * _waterPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
            break;
        }
    }
}
