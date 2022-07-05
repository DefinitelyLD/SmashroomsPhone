using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //TODO: CAMERA CALCULATE ORTHO SIZE
    private const float CAMERA_OFFSET_Y = 0;
    private const float CAMERA_OFFSET_Z = -10;
    private const float CAMERA_OFFSET_X = 20;

    private float cameraLimitX = 72;

    [SerializeField] Transform target;
    public float smoothSpeed = 0.125f;

    private void FixedUpdate() 
    {
        Vector3 desiredPostion = new Vector3(target.position.x + CAMERA_OFFSET_X, CAMERA_OFFSET_Y, CAMERA_OFFSET_Z);
        desiredPostion.x = Mathf.Clamp(desiredPostion.x, -cameraLimitX, cameraLimitX);
        Vector3 smoothedPostion = Vector3.Lerp (transform.position, desiredPostion, smoothSpeed);
        transform.position = smoothedPostion;

        transform.LookAt(transform);
    }
}
