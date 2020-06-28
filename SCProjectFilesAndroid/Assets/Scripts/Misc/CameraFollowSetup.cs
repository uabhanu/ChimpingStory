using UnityEngine;
public class CameraFollowSetup : MonoBehaviour 
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform followTransform;
    [SerializeField] private float zoom;

    private void Start() 
    {
        if(followTransform == null) 
        {
                Debug.LogError("followTransform is null! Intended?");
                cameraFollow.Setup(() => Vector3.zero , () => zoom , true , true);
        } 
        else 
        {
                cameraFollow.Setup(() => followTransform.position, () => zoom , true , true);
        }
    }
}