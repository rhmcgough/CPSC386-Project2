// Obtained from https://gamedevbeginner.com/how-to-follow-the-player-with-a-camera-in-unity-with-or-without-cinemachine/
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothTime = 0.15f;
    Vector3 currentVelocity;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref currentVelocity,
            smoothTime
        );
}

}