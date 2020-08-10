using UnityEngine;

public class WaterParallax : MonoBehaviour
{
    private WaterPuss _waterPussReference;

    [SerializeField] private float _parallaxOffset;
    
    private void Start()
    {
        _waterPussReference = FindObjectOfType<WaterPuss>();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        transform.Translate(Vector2.left * _waterPussReference.GetMoveSpeed() * _parallaxOffset * Time.deltaTime , Space.World);
    }
}
