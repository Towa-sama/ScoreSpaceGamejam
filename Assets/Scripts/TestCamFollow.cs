using UnityEngine;

public class TestCamFollow : MonoBehaviour
{
    // camera will follow this object
    [SerializeField]
    private Transform Target;
    //camera transform
    [SerializeField]
    private Transform camTransform;
    // offset between camera and target
    [SerializeField]
    private Vector3 Offset = new(0.05997086f, 5.903007f, -6.43185f);
    // change this value to get desired smoothness
    [SerializeField]
    private float SmoothTime = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
    
    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        // update rotation
        transform.LookAt(Target);
    }
}